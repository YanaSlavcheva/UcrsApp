namespace Ucrs.Web.ViewModels.Courses
{
    using System.ComponentModel.DataAnnotations;
    using Ucrs.Common;
    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Mapping;

    public class CourseBindingModel : IMapTo<Course>
    {
        [Required]
        [MaxLength(GlobalConstants.CourseTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [Range(GlobalConstants.CoursePointsMinValue, GlobalConstants.CoursePointsMaxValue)]
        public int Points { get; set; }
    }
}
