
using Learning.Application.Convertors;
using Learning.Application.Interfaces;
using Learning.Application.Services;
using Learning.Domain.Interfaces;
using Learning.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Learning.Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Application Layer
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEpisodeService, EpisodeService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IConfigService, ConfigService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IOnlineClassService, OnlineClassService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IViewRenderService, RenderViewToString>();
            services.AddScoped<ISmsService, SmsService>();
            //services.AddScoped<IUserPanelService, UserPanelService>();





            //Infra Data Layer
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<IConfigRepository, ConfigRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IOnlineClassRepository, OnlineClassRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();


        }
    }
}
