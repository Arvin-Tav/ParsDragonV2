namespace Learning.Domain.DTO.CourseEpisode
{
    public class ShowCourseEpisodeDTO
    {
        public bool NotSuccesEpisodes { get; set; }
        public bool NotSucces { get; set; }
        public Learning.Domain.Models.Course.CourseEpisode Episode { get; set; }
        public string FilePath { get; set; }

    }
}
