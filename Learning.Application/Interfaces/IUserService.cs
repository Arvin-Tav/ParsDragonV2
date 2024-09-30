using Learning.Domain.DTO.Account;
using Learning.Domain.DTO.PublicEnum;
using Learning.Domain.DTO.UserPanel;
using Learning.Domain.Models.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IUserService
    {
        #region User
        Task<RegisterResult> RegisterUser(RegisterDTO register);
        Task<ActivateMobileResult> ActiveAccountByMobile(ActivateMobileDTO activeCode);
        Task<LoginResult> LoginUser(LoginDTO login);
        Task<User> GetUserByMobileOrEmail(string userEmailOrMobile);
        Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordDTO forgot);
        #region admin 
        Task<CreateUserByAdminResut> CreateUserByAdmin(CreateUserByAdminDTO createUser,IFormFile imageUser, List<int> selectedRoles);
        Task<EditUserByAdminResut> EditUserByAdmin(EditUserByAdminDTO editUser,IFormFile imageUser, List<int> selectedRoles);
        Task<EditUserByAdminDTO> GeUserForEditByAdmin(int userId);
        Task<FilterUserDTO> FilterUsers(FilterUserDTO filter);
        #endregion

        #region user panel
        Task<InformationUserDTO> GetUserInformation(int userId);
        Task<UserPanelResult> ChangeAvatar(int userId, IFormFile imageAvatar);
        Task<EditProfileDTO> GetUserInfoForEdit(int userId);
        Task<UserPanelResult> EditForUserInfo(EditProfileDTO editProfile, int userId);
        Task<UserPanelResult> ChangeUserPassword(ChangePasswordDTO changePassword, int userId);
        Task<SideBarUserPanelDTO> GetSideBarUserPanelData(int userId);

        //InformationUserViewModel GetUserInformation(int userId);
        //void EditProfile(string userName, EditProfileViewModel profile);
        //bool CompareOldPassword(string userName, string OldPassword);
        #endregion




        Task<bool> IsExistUserName(string userName);
        Task<bool> IsExistMobile(string mobile);
        Task<bool> IsExistEditUserName(int userId, string userName);
        Task<bool> IsExistEditMobile(int userId, string mobile);
        Task<bool> IsExistEmail(string email);
        Task<bool> IsExistEditEmail(int userId, string email);
        Task<int> AddUser(User user);
        Task<bool> ActiveAccount(string activeCode);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByMobile(string mobile);
        Task<User> GetUserByUserName(string userName);
        Task<User> GetUserByActiveCode(string activeCode);
        Task UpdateUser(User user);
        Task<User> GetUserById(int userId);
        Task<DeleteResult> DeleteUser(int userId);
        Task<int> GetStudentCount();
        Task<ActiveResult> IsActive(int userId);
        #endregion
    }
}
