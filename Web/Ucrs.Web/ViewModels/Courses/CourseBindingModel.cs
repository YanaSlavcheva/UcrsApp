namespace Ucrs.Web.ViewModels.Courses
{
    using System.ComponentModel.DataAnnotations;

    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Mapping;

    public class CourseBindingModel : IMapTo<Course>
    {
        [Required]
        public string Title { get; set; }
    }
}
