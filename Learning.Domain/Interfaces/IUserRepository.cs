using Learning.Domain.Models;
using Learning.Domain.Models.Account;
using Learning.Domain.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Domain.DTO.UserPanel;

namespace Learning.Domain.Interfaces
{
    public interface IUserRepository
    {
        #region User
        Task<User> GetUserByMobile(string mobile);
        Task<User> GetUserForLogin(string userEmailOrMobile);




        Task<FilterUserDTO> FilterUsers(FilterUserDTO filter);
        Task<bool> IsExistUserName(string userName);
        Task<bool> IsExistMobile(string mobile);
        Task<bool> IsExistEditUserName(int userId, string userName);
        Task<bool> IsExistEditMobile(int userId, string mobile);
        Task<bool> IsExistEmail(string email);
        Task<bool> IsExistEditEmail(int userId, string email);
        Task<int> AddUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUserName(string userName);
        Task<User> GetUserByActiveCode(string activeCode);
        void UpdateUser(User user);
        Task<User> GetUserById(int userId);
        Task<int> GetStudentCount();
        Task<List<int>> GetStudentUserId();

        Task Save();
        #endregion





    }
}
