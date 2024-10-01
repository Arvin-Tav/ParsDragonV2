using Learning.Domain.Interfaces;
using Learning.Domain.Models.Config;
using Learning.Domain.Models.Contact;
using Learning.Domain.Models.Course;
using Learning.Domain.Models.Order;
using Learning.Domain.Models.Permissions;
using Learning.Domain.Models.Question;
using Learning.Domain.Models.Wallet;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class ConfigRepository : IConfigRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public ConfigRepository(LearningContext context)
        {
            _context = context;
        }

        #endregion

        #region config

        public async Task<Config> GetConfigKey(string key)
        {
            #region re
            //            List<string> values = new List<string>()
            //           {
            //               "Address",
            //"AkseSafeyeAsly",
            //"AksTamasBama",
            //"Consulting",
            //"DarBareyeMa",
            //"Email",
            //"GavaninKharid",
            //"HamkaryBama",
            //"Inista",
            //"RahnemyaKharid",
            //"Telegram",
            //"TellHome",
            //"TellMobile",
            //"TextHome1",
            //"TextHome2",
            //"Whatsapp"
            //           };
            //            foreach (var item in values)
            //            {
            //                await _context.Configs.AddAsync(new Config()
            //                {
            //                    CreateDate = DateTime.Now,
            //                    IsDelete = false,
            //                    Key = item,
            //                    UpdateDate = DateTime.Now,
            //                    Value = "",
            //                });
            //            }


            //            List<Permission> permissions = new List<Permission>();

            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "پنل مدیریت",
            //                PermissionName = "Admin",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "کاربران",
            //                PermissionName = "Users",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "نقش ها",
            //                PermissionName = "Roles",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "استاد",
            //                PermissionName = "Master",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "بنرها",
            //                PermissionName = "Banners",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "مقاله ها",
            //                PermissionName = "Blogs",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "نظرها",
            //                PermissionName = "Comment",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });



            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "دورها",
            //                PermissionName = "Courses",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "دسته بندی دور ها",
            //                PermissionName = "CourseGroups",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "تخفیف ها",
            //                PermissionName = "Discounts",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });



            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "فاکتورها",
            //                PermissionName = "Orders",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "سوالات و پاسخ ها\r\n",
            //                PermissionName = "Question",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "واریز و برداشت ها\r\n",
            //                PermissionName = "Walletv",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "موارد ارسالی در تماس با ما",
            //                PermissionName = "Contact",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "تنظیمات سایت",
            //                PermissionName = "Config",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "دسترسی به دوره ها",
            //                PermissionName = "AccessToCourses",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            permissions.Add(new Permission()
            //            {
            //                PermissionTitle = "دسترسی به کلاس ها",
            //                PermissionName = "AccessToOnlineClass",
            //                CreateDate = DateTime.Now,
            //                IsDelete = false,
            //                UpdateDate = DateTime.Now,

            //            });
            //            await _context.Permission.AddRangeAsync(permissions);


            //await _context.Users.AddAsync(new Domain.Models.Account.User()
            //{
            //    UpdateDate = DateTime.UtcNow,
            //    CreateDate = DateTime.UtcNow,
            //    LastName = "admin",
            //    ActiveCodeEmail = "admin",
            //    AboutMe = "admin",
            //    ActiveCodeMobile = "admin",
            //    Email = "admin@gmail.com",
            //    Mobile = "admin",
            //    Password = "f6e0a1e2ac41945a9aa7ff8a8aaa0cebc12a3bcc981a929ad5cf810a090e11ae",
            //    FirstName = "admin",
            //    IsEmailActive=true,
            //    IsActive=true,
            //    UserName = "admin",
            //});

            //await _context.SaveChangesAsync();
            #endregion
            return await _context.Configs.AsQueryable().FirstOrDefaultAsync(i => i.Key == key);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateConfig(Config config)
        {
            _context.Configs.Update(config);
        }
        #endregion



    }
}
