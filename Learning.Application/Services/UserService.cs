using Learning.Application.Convertors;
using Learning.Application.Extentions;
using Learning.Application.Generator;
using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Application.Senders;
using Learning.Application.Utils;
using Learning.Domain.DTO.Account;
using Learning.Domain.DTO.PublicEnum;
using Learning.Domain.DTO.UserPanel;
using Learning.Domain.DTO.Wallet;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Wallet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class UserService : IUserService
    {
        #region constructor
        private readonly IUserRepository _userRepository;
        private readonly ISmsService _smsService;
        private readonly IPermissionService _permissionService;
        private readonly IWalletRepository _walletRepository;
        private readonly IViewRenderService _viewRender;

        public UserService(IUserRepository userRepository, ISmsService smsService, IPermissionService permissionService, IWalletRepository walletRepository, IViewRenderService viewRender)
        {
            _userRepository = userRepository;
            _smsService = smsService;
            _permissionService = permissionService;
            _walletRepository = walletRepository;
            _viewRender = viewRender;
        }
        #endregion



        public async Task<RegisterResult> RegisterUser(RegisterDTO register)
        {
            if (await _userRepository.IsExistEmail(register.Email.FixedEmail())) return RegisterResult.EmailExists;
            if (await _userRepository.IsExistMobile(register.Mobile.FixedEmail())) return RegisterResult.MobileExists;
            if (await _userRepository.IsExistUserName(register.UserName.FixedEmail())) return RegisterResult.UsernameExists;

            #region create user
            User addUser = new User();
            addUser.ActiveCodeEmail = NameGenerator.GenerateUniqCode();
            addUser.ActiveCodeMobile = NameGenerator.GenerateUniqCodeNumber();
            addUser.Email = register.Email.FixedEmail();
            addUser.UserName = register.UserName.FixedEmail();
            addUser.Mobile = register.Mobile.FixedEmail();
            addUser.Password = register.Password.EncodePasswordSha256();
            addUser.UserAvatar = "DefultAvatar.jpg";

            await _userRepository.AddUser(addUser);
            await _userRepository.Save();
            #endregion

            #region send activation email
            //string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            //SendEmail.Send(user.Email, "فعال سازی", body);
            //return await Task.FromResult(View("SuccessRegister"));
            #endregion

            #region send activeion mobile
            bool sendMessage = await _smsService.SendVerificationSms(addUser.Mobile, addUser.ActiveCodeMobile);
            if (!sendMessage) return RegisterResult.Error;
            #endregion
            return RegisterResult.Success;
        }
        public async Task<ActivateMobileResult> ActiveAccountByMobile(ActivateMobileDTO activeCode)
        {
            var selectedUser = await _userRepository.GetUserByMobile(activeCode.Mobile);
            if (selectedUser == null) return ActivateMobileResult.NotFound;
            if (selectedUser.ActiveCodeMobile != activeCode.MobileActiveCode) return ActivateMobileResult.NotValideCode;
            selectedUser.IsActive = true;
            selectedUser.ActiveCodeMobile = NameGenerator.GenerateUniqCodeNumber();
            await _userRepository.Save();
            return ActivateMobileResult.Success;
        }
        public async Task<LoginResult> LoginUser(LoginDTO login)
        {
            var user = await _userRepository.GetUserForLogin(login.EmailOrMobile.FixedEmail());
            if (user == null) return LoginResult.NotFound;
            if (!user.IsActive) return LoginResult.NotActive;
            if (user.IsBlocked) return LoginResult.IsBlocked;
            string hashPasword = login.Password.EncodePasswordSha256();
            if (user.Password != hashPasword) return LoginResult.NotFound;

            return LoginResult.Success;
        }


        public async Task<User> GetUserByMobileOrEmail(string userEmailOrMobile)
        {
            return await _userRepository.GetUserForLogin(userEmailOrMobile.FixedEmail());
        }

        public async Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordDTO forgot)
        {
            var user = await _userRepository.GetUserByMobile(forgot.Mobile.FixedEmail());
            if (user == null) return ForgotPasswordResult.NotFound;
            if (user.IsBlocked) return ForgotPasswordResult.IsBlocked;

            #region send password for user
            var newPawssword = NameGenerator.GenerateUniqCodeNumber();
            user.Password = newPawssword.EncodePasswordSha256();
            user.IsActive = true;
            _userRepository.UpdateUser(user);
            await _userRepository.Save();
            bool sendMessage = await _smsService.SendUserPasswordSms(user.Mobile, newPawssword);
            if (!sendMessage) return ForgotPasswordResult.Error;
            #endregion


            return ForgotPasswordResult.Success;
        }





        #region admin 
        public async Task<CreateUserByAdminResut> CreateUserByAdmin(CreateUserByAdminDTO createUser, IFormFile imageUser, List<int> selectedRoles)
        {
            if (await _userRepository.IsExistEmail(createUser.Email.FixedEmail())) return CreateUserByAdminResut.EmailExists;
            if (await _userRepository.IsExistMobile(createUser.Mobile.FixedEmail())) return CreateUserByAdminResut.MobileExists;
            if (await _userRepository.IsExistUserName(createUser.UserName.FixedEmail())) return CreateUserByAdminResut.UsernameExists;
            #region create user
            User addUser = new User();
            if (imageUser != null)
            {
                addUser.UserAvatar = TextFixer.GenerateUniqCodeString(createUser.UserName) + Path.GetExtension(imageUser.FileName);
                bool result = imageUser
                   .AddImageToServer(addUser.UserAvatar, PathExtensions.UserAvatarOrginServer, null, null, PathExtensions.UserAvatarThumbServer);
                if (!result) return CreateUserByAdminResut.ErrorImage;
            }
            else
            {
                addUser.UserAvatar = "DefultAvatar.jpg";
            }
            addUser.ActiveCodeEmail = NameGenerator.GenerateUniqCode();
            addUser.ActiveCodeMobile = NameGenerator.GenerateUniqCodeNumber();
            addUser.FirstName = createUser.FirstName;
            addUser.LastName = createUser.LastName;
            addUser.Email = createUser.Email.FixedEmail();
            addUser.UserName = createUser.UserName.FixedEmail();
            addUser.Mobile = createUser.Mobile.FixedEmail();
            addUser.Password = createUser.Password.EncodePasswordSha256();
            addUser.IsActive = true;
            addUser.IsEmailActive = true;
            await _userRepository.AddUser(addUser);
            await _userRepository.Save();
            #endregion
            if (selectedRoles != null)
            {
                await _permissionService.AddUserRole(selectedRoles, addUser.UserId);
            }
            if (createUser.Wallet > 0)
            {
                await _walletRepository.AddWallet(new Wallet()
                {
                    Amount = (int)createUser.Wallet,
                    UserId = addUser.UserId,
                    Description = "شارژ حساب توسط مدیرست",
                    IsPay = true,
                    TypeId = 1,
                    Ip = "::1",
                });
                await _walletRepository.Save();
                User adminInfo = await _userRepository.GetUserById(createUser.AdminId);
                ChangeWalletByAdminDTO charge = new ChangeWalletByAdminDTO()
                {
                    UserName = addUser.UserName,
                    Amount = (int)createUser.Wallet,
                    Mobile = addUser.Mobile,
                    AdminMobile = adminInfo.Mobile,
                    AdminName = adminInfo.UserName,
                    IsCharge = true,
                };
                #region Send  Email
                string body = _viewRender.RenderToStringAsync("_SendEmailToAdminByChangeWallet", charge);
                SendEmail.Send("mehdisahandi@gmail.com", "تغیرات کیف پول کاربر توسط مدیریت", body);
                #endregion
            }
            return CreateUserByAdminResut.Success;
        }

        public async Task<EditUserByAdminResut> EditUserByAdmin(EditUserByAdminDTO editUser, IFormFile imageUser, List<int> selectedRoles)
        {
            if (await _userRepository.IsExistEditEmail(editUser.UserId, editUser.Email.FixedEmail())) return EditUserByAdminResut.EmailExists;
            if (await _userRepository.IsExistEditMobile(editUser.UserId, editUser.Mobile.FixedEmail())) return EditUserByAdminResut.MobileExists;
            if (await _userRepository.IsExistEditUserName(editUser.UserId, editUser.UserName.FixedEmail())) return EditUserByAdminResut.UsernameExists;
            #region create user
            User updateUser = await _userRepository.GetUserById(editUser.UserId);
            if (updateUser == null) return EditUserByAdminResut.NotFound;
            if (imageUser != null)
            {
                string userAvatar = TextFixer.GenerateUniqCodeString(editUser.UserName) + Path.GetExtension(imageUser.FileName);
                bool result = imageUser
                   .AddImageToServer(userAvatar, PathExtensions.UserAvatarOrginServer, null, null, PathExtensions.UserAvatarThumbServer, updateUser.UserAvatar);
                if (!result) return EditUserByAdminResut.ErrorImage;
                updateUser.UserAvatar = userAvatar;
            }
            updateUser.Email = editUser.Email.FixedEmail();
            updateUser.UserName = editUser.UserName.FixedEmail();
            updateUser.Mobile = editUser.Mobile.FixedEmail();
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                updateUser.Password = editUser.Password.EncodePasswordSha256();
            }
            updateUser.IsActive = true;
            updateUser.IsEmailActive = true;
            updateUser.FirstName = editUser.FirstName;
            updateUser.LastName = editUser.LastName;
            updateUser.UpdateDate = DateTime.Now;
            _userRepository.UpdateUser(updateUser);
            await _userRepository.Save();
            #endregion
            if (selectedRoles != null)
            {
                await _permissionService.DeleteUserRole(editUser.UserId);
                await _permissionService.AddUserRole(selectedRoles, updateUser.UserId);
            }
            int wallet = await _walletRepository.BalanceUserWallet(editUser.UserId);
            if (editUser.Wallet != wallet)
            {
                int amountCharch = 0;
                Wallet addWallet = new Wallet();
                addWallet.UserId = editUser.UserId;
                addWallet.Ip = "::1";
                addWallet.IsPay = true;
                User adminInfo = await _userRepository.GetUserById(editUser.AdminId);
                ChangeWalletByAdminDTO charge = new ChangeWalletByAdminDTO()
                {
                    UserName = updateUser.UserName,
                    Mobile = updateUser.Mobile,
                    AdminMobile = adminInfo.Mobile,
                    AdminName = adminInfo.UserName,
                };
                if (editUser.Wallet > wallet)
                {
                    charge.IsCharge = true;
                    amountCharch = (int)editUser.Wallet - wallet;
                    addWallet.TypeId = 1;
                    addWallet.Description = "شارژ حساب توسط مدیرست";
                    addWallet.Amount = amountCharch;
                }
                else if (editUser.Wallet < wallet)
                {
                    charge.IsCharge = false;
                    amountCharch = wallet - (int)editUser.Wallet;
                    addWallet.TypeId = 2;
                    addWallet.Description = "کسر از حساب توسط مدیرست";
                    addWallet.Amount = amountCharch;
                }
                charge.Amount = amountCharch;
                await _walletRepository.AddWallet(addWallet);
                #region Send  Email
                string body = _viewRender.RenderToStringAsync("_SendEmailToAdminByChangeWallet", charge);
                SendEmail.Send("mehdisahandi@gmail.com", "تغیرات کیف پول کاربر توسط مدیریت", body);
                #endregion
            }
            await _walletRepository.Save();
            return EditUserByAdminResut.Success;
        }

        public async Task<EditUserByAdminDTO> GeUserForEditByAdmin(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return null;

            return new EditUserByAdminDTO()
            {
                UserId = userId,
                UserName = user.UserName,
                LastName = user.LastName,
                FirstName = user.FirstName,
                AboutMe = user.AboutMe,
                Email = user.Email,
                Mobile = user.Mobile,
                AvatarName = user.UserAvatar,
                UserRoles = await _permissionService.GetRoleIdByUserId(userId),
                Wallet = await _walletRepository.BalanceUserWallet(userId),
            };
        }
        public async Task<FilterUserDTO> FilterUsers(FilterUserDTO filter)
        {
            return await _userRepository.FilterUsers(filter);
        }

        #endregion



        #region user panel
        public async Task<InformationUserDTO> GetUserInformation(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return null;
            InformationUserDTO information = new InformationUserDTO();
            information.UserName = user.UserName;
            information.FirstName = user.FirstName;
            information.LastName = user.LastName;
            information.Email = user.Email;
            information.CreateDate = user.CreateDate;
            information.Mobile = user.Mobile;
            information.Wallet = await _walletRepository.BalanceUserWallet(userId);
            return information;
        }
        public async Task<UserPanelResult> ChangeAvatar(int userId, IFormFile imageAvatar)
        {
            var user = await _userRepository.GetUserById(userId);
            return UserPanelResult.NotFound;
            if (imageAvatar != null)
            {
                string userAvatar = TextFixer.GenerateUniqCodeString(user.UserName) + Path.GetExtension(imageAvatar.FileName);
                bool result = imageAvatar
                   .AddImageToServer(userAvatar, PathExtensions.UserAvatarOrginServer, null, null, PathExtensions.UserAvatarThumbServer, user.UserAvatar);
                if (!result) return UserPanelResult.Error;
                user.UserAvatar = userAvatar;
            }
            _userRepository.UpdateUser(user);
            await _userRepository.Save();
            return UserPanelResult.Success;
        }
        public async Task<EditProfileDTO> GetUserInfoForEdit(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return null;
            return new EditProfileDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AboutMe = user.AboutMe,
            };
        }
        public async Task<UserPanelResult> EditForUserInfo(EditProfileDTO editProfile, int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return UserPanelResult.NotFound;
            user.FirstName = editProfile.FirstName;
            user.LastName = editProfile.LastName;
            user.AboutMe = editProfile.AboutMe;
            _userRepository.UpdateUser(user);
            await _userRepository.Save();
            return UserPanelResult.Success;
        }
        public async Task<UserPanelResult> ChangeUserPassword(ChangePasswordDTO changePassword, int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return UserPanelResult.NotFound;
            var oldPassword = changePassword.OldPassword.EncodePasswordSha256();
            if (user.Password != oldPassword) return UserPanelResult.Error;
            user.Password = changePassword.Password.EncodePasswordSha256();
            _userRepository.UpdateUser(user);
            await _userRepository.Save();
            return UserPanelResult.Success;
        }
        public async Task<SideBarUserPanelDTO> GetSideBarUserPanelData(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return null;
            SideBarUserPanelDTO selected = new SideBarUserPanelDTO();
            selected.ImageName = user.UserAvatar;
            selected.CreateDate = user.CreateDate;
            selected.UserName = user.UserName;
            return selected;
        }

        #endregion





        public async Task<bool> ActiveAccount(string activeCode)
        {
            var selectedUser = await _userRepository.GetUserByActiveCode(activeCode);
            if (selectedUser == null) return false;
            selectedUser.IsActive = true;
            selectedUser.ActiveCodeEmail = NameGenerator.GenerateUniqCode();
            await _userRepository.Save();
            return true;
        }



        public async Task<int> AddUser(User user)
        {
            await _userRepository.AddUser(user);
            return user.UserId;
        }

        public async Task<DeleteResult> DeleteUser(int userId)
        {
            var selectedUser = await _userRepository.GetUserById(userId);
            if (selectedUser == null) return DeleteResult.NotFound;
            selectedUser.IsDelete = true;
            await _userRepository.Save();
            return DeleteResult.Success;
        }


        public async Task<int> GetStudentCount()
        {
            return await _userRepository.GetStudentCount();
        }

        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _userRepository.GetUserByActiveCode(activeCode);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetUserByMobile(mobile);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _userRepository.GetUserByUserName(userName);
        }

        public async Task<ActiveResult> IsActive(int userId)
        {
            var selectedUser = await _userRepository.GetUserById(userId);
            if (selectedUser == null) return ActiveResult.NotFound;
            selectedUser.IsActive = true;
            await _userRepository.Save();
            return ActiveResult.Success;
        }



        public async Task<bool> IsExistEditEmail(int userId, string email)
        {
            return await _userRepository.IsExistEditEmail(userId, email);
        }

        public async Task<bool> IsExistEditMobile(int userId, string mobile)
        {
            return await _userRepository.IsExistEditMobile(userId, mobile);
        }

        public async Task<bool> IsExistEditUserName(int userId, string userName)
        {
            return await _userRepository.IsExistEditUserName(userId, userName);
        }

        public async Task<bool> IsExistEmail(string email)
        {
            return await _userRepository.IsExistEmail(email);
        }

        public async Task<bool> IsExistMobile(string mobile)
        {
            return await _userRepository.IsExistMobile(mobile);
        }

        public async Task<bool> IsExistUserName(string userName)
        {
            return await _userRepository.IsExistUserName(userName);
        }



        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }


    }
}
