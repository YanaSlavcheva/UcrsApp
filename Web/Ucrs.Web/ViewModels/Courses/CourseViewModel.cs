namespace Ucrs.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Mapping;

    public class CourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Points { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
