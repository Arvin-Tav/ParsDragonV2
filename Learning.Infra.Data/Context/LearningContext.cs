using Learning.Domain.Models.Account;
using Learning.Domain.Models.Banner;
using Learning.Domain.Models.Blog;
using Learning.Domain.Models.Config;
using Learning.Domain.Models.Contact;
using Learning.Domain.Models.Course;
using Learning.Domain.Models.OnlineClass;
using Learning.Domain.Models.Order;
using Learning.Domain.Models.Permissions;
using Learning.Domain.Models.Question;
using Learning.Domain.Models.Ticket;
using Learning.Domain.Models.Visit;
using Learning.Domain.Models.Wallet;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Learning.Infra.Data.Context
{
    public class LearningContext : DbContext
    {
        public LearningContext(DbContextOptions<LearningContext> options) : base(options)
        {

        }
        #region Contact
        public DbSet<Contact> Contacts { get; set; }
        #endregion
        #region Banner
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        #endregion

        #region OnlineClass
        public DbSet<OnlineClass> OnlineClasses { get; set; }
        public DbSet<OnlineClassContact> OnlineClassContacts { get; set; }
        public DbSet<OnlineClassGoal> OnlineClassGoals { get; set; }
        public DbSet<OnlineClassHeading> OnlineClassHeadings { get; set; }
        public DbSet<OnlineClassOfDay> OnlineClassOfDays { get; set; }
        public DbSet<OnlineClassPrerequisite> OnlineClassPrerequisites { get; set; }
        public DbSet<UserOnlineClass> UserOnlineClasses { get; set; }
        public DbSet<OnlineClassEpisode> OnlineClassEpisodes { get; set; }
        #endregion
        #region Config
        public DbSet<Config> Configs { get; set; }
        #endregion

        #region Blog
        public DbSet<Blog> Blogs { get; set; }
        #endregion
        #region User
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }
        #endregion

        #region Wallet
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        #endregion

        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion

        #region Course
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }
        public DbSet<CourseDiscount> CourseDiscounts { get; set; }
        #endregion
        #region Question
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        #endregion
        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        #endregion
        #region Visit
        public DbSet<Visit> Visits { get; set; }

        #endregion
        #region Ticket
        public DbSet<Ticket> Tickets { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
               .SelectMany(t => t.GetForeignKeys())
               .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            modelBuilder.Entity<User>()
                .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Course>()
               .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<CourseComment>()
               .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Answer>()
              .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Question>()
              .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Blog>()
             .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Contact>()
            .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Banner>()
           .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Discount>()
         .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<CourseEpisode>()
        .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Slider>()
      .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<OnlineClass>()
    .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Order>()
    .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<OrderDetail>()
    .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Ticket>()
               .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<OnlineClassEpisode>()
               .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<CourseDiscount>()
               .HasQueryFilter(p => !p.IsDelete);




            modelBuilder.Entity<Course>()
               .HasOne<CourseGroup>(f => f.CourseGroup)
               .WithMany(g => g.Courses)
               .HasForeignKey(f => f.GroupId);

            modelBuilder.Entity<Course>()
                .HasOne<CourseGroup>(f => f.Groupe)
                .WithMany(g => g.SubGroup)
                .HasForeignKey(f => f.SubGroup);


            modelBuilder.Entity<OnlineClass>()
              .HasOne<CourseGroup>(f => f.OnlineClassGroup)
              .WithMany(g => g.OnlineClassGroup)
              .HasForeignKey(f => f.GroupId);

            modelBuilder.Entity<OnlineClass>()
                .HasOne<CourseGroup>(f => f.OnlineClassSubGroup)
                .WithMany(g => g.OnlineClassSubGroup)
                .HasForeignKey(f => f.SubGroup);


            base.OnModelCreating(modelBuilder);
        }
    }
}
