using Learning.Application.EntitiesExtensions;
using Learning.Application.Interfaces;
using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.OnlineClass;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Course;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Mvc.ViewComponents
{
    #region banner
    public class BannerViewComponent : ViewComponent
    {
        private readonly IBannerService _bannerService;
        private readonly ISliderService _sliderService;
        public BannerViewComponent(IBannerService bannerService, ISliderService sliderService)
        {
            _bannerService = bannerService;
            _sliderService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.ListSlider = await _sliderService.GetSliders();
            return await Task.FromResult((IViewComponentResult)View("Banner", await _bannerService.GetBanners()));
        }
    }
    #endregion
    #region blog
    public class BlogViewComponent : ViewComponent
    {
        private IBlogService _blogService;

        public BlogViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("Blog", await _blogService.GetShowBlogsInSlider(6)));
        }
    }
    #endregion
    #region main header
    public class MainHeaderViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public MainHeaderViewComponent(IPermissionService permissionService, IUserService userService)
        {
            _permissionService = permissionService;
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                User selectedUser = await _userService.GetUserById(User.GetUserId());
                if (selectedUser != null)
                {
                    ViewBag.User = selectedUser;
                    if (await _permissionService.CheckPermission("Admin", selectedUser.UserId))
                    {
                        ViewBag.IsAdmin = true;
                    }
                }
            }
            return await Task.FromResult((IViewComponentResult)View("MainHeader"));
        }
    }
    #endregion

    #region course group
    public class CourseGroupComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        public CourseGroupComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("CourseGroup", await _courseService.GetAllGroup()));
        }
    }
    #endregion


    #region  course group mobile
    public class CourseGroupMobileComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        public CourseGroupMobileComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("CourseGroupMobile", await _courseService.GetAllGroup()));
        }
    }
    #endregion


    #region lates course
    public class LatesCourseViewComponent : ViewComponent
    {
        private ICourseService _courseService;

        public LatesCourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LatesCourse", await _courseService.GetLatesCourse()));
        }
    }
    #endregion


    #region slider Text Home
    public class SliderTextHomeViewComponent : ViewComponent
    {
        private IConfigService _configService;

        public SliderTextHomeViewComponent(IConfigService configService)
        {
            _configService = configService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("SliderTextHome", await _configService.GetSliderHomeText()));
        }

    }
    #endregion

    #region suggested course
    public class SuggestedCourseViewComponent : ViewComponent
    {
        private ICourseService _courseService;

        public SuggestedCourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("SuggestedCourse", await _courseService.GetSuggestedCourse()));
        }
    }
    #endregion

    #region course group show blog
    public class CourseGroupShowBlogViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        public CourseGroupShowBlogViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("CourseGroupShowBlog", await _courseService.GetAllGroup()));
        }
    }
    #endregion

    #region course information
    public class CourseInformationViewComponent : ViewComponent
    {
        private ICourseService _courseService;

        public CourseInformationViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("CourseInformation", await _courseService.GetCourseInformaation()));
        }
    }
    #endregion

    #region last questions
    public class LastQuestionsViewComponent : ViewComponent
    {
        private readonly IForumService _forumService;


        public LastQuestionsViewComponent(IForumService forumService)
        {
            _forumService = forumService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LastQuestions", await _forumService.GetLastQuestions(6)));
        }
    }
    #endregion


    #region lates classes
    public class LatesClassesViewComponent : ViewComponent
    {
        private IOnlineClassService _onlineClassService;

        public LatesClassesViewComponent(IOnlineClassService onlineClassService)
        {
            _onlineClassService = onlineClassService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LatesClasses", await _onlineClassService.FilterOnlineClass(new FilterOnlineClassDTO() { TakeEntity = 6 }))); ;
        }
    }
    #endregion

    #region popular course
    public class PopularCourseViewComponent : ViewComponent
    {
        private ICourseService _courseService;

        public PopularCourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("PopularCourse", await _courseService.GetPopularCourse(8)));
        }
    }
    #endregion

    #region course to ShowCourseListItem
    public class ShowCourseListItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<Course> course)
        {
            List<ShowCourseListItemDTO> list = new List<ShowCourseListItemDTO>();
            foreach (var item in course)
            {
                list.Add(new ShowCourseListItemDTO()
                {
                    CourseId = item.CourseId,
                    ImageName = item.CourseImageName,
                    Price = item.CoursePrice,
                    Title = item.CourseTitle,
                    TeacherName = item.User.UserName,
                    TotalTime = new TimeSpan(item.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
                });
            }
            return await Task.FromResult((IViewComponentResult)View("ShowCourseListItem", list));
        }
    }
    #endregion
  
}
