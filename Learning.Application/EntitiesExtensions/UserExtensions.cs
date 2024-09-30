using Learning.Application.Utils;
using Learning.Domain.DTO.Account;
using Learning.Domain.DTO.Master;
using Learning.Domain.DTO.Permission;
using Learning.Domain.DTO.UserPanel;
using Learning.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.EntitiesExtensions
{
    public static class UserExtensions
    {
        public static string GetUserShowName(this User user)
        {
            if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
            {
                return $"{user.FirstName} {user.LastName}";
            }

            return user.Mobile;
        }

        public static string GetUserImageAddress(this User user)
        {
            return user.UserAvatar == "DefultAvatar.jpg"
                ? PathExtensions.DefaultAvatar
                : PathExtensions.UserAvatarOrgin + user.UserAvatar;
        }

        public static string GetUserImageThumbAddress(this User user)
        {
            return user.UserAvatar == "DefultAvatar.jpg"
                ? PathExtensions.DefaultAvatar
                : PathExtensions.UserAvatarThumb + user.UserAvatar;
        }
        public static string GetUserImageAddress(this EditUserByAdminDTO user)
        {
            return user.AvatarName == "DefultAvatar.jpg"
               ? PathExtensions.DefaultAvatar
               : PathExtensions.UserAvatarOrgin + user.AvatarName;
        }
        public static string GetUserImageThumbAddress(this EditUserByAdminDTO user)
        {
            return user.AvatarName == "DefultAvatar.jpg"
                ? PathExtensions.DefaultAvatar
                : PathExtensions.UserAvatarThumb + user.AvatarName;
        }
        public static string GetMasterImageAddress(this InfoMasterDTO user)
        {
            return user.Image == "DefultAvatar.jpg"
               ? PathExtensions.DefaultAvatar
               : PathExtensions.UserAvatarOrgin + user.Image;
        }
        public static string GetMasterImageThumbAddress(this InfoMasterDTO user)
        {
            return user.Image == "DefultAvatar.jpg"
                ? PathExtensions.DefaultAvatar
                : PathExtensions.UserAvatarThumb + user.Image;
        }
        public static string GetMasterImageAddress(this MastersDTO user)
        {
            return user.Image == "DefultAvatar.jpg"
               ? PathExtensions.DefaultAvatar
               : PathExtensions.UserAvatarOrgin + user.Image;
        }
        public static string GetMasterImageThumbAddress(this MastersDTO user)
        {
            return user.Image == "DefultAvatar.jpg"
                ? PathExtensions.DefaultAvatar
                : PathExtensions.UserAvatarThumb + user.Image;
        }
        public static string GetMasterImageAddress(this SideBarUserPanelDTO user)
        {
            return user.ImageName == "DefultAvatar.jpg"
               ? PathExtensions.DefaultAvatar
               : PathExtensions.UserAvatarOrgin + user.ImageName;
        }
    }
}
