using Learning.Domain.Models;
using Learning.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Extentions
{
    public static class UserExtentions
    {
        public static string GetUserName(this User user)
        {
            if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
            {
                return $"{user.FirstName} {user.LastName}";
            }
            return user.UserName;
        }
    }
}
