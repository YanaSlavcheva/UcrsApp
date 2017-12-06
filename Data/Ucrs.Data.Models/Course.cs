namespace Ucrs.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Ucrs.Common;
    using Ucrs.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.ApplicationUsersInCourses = new HashSet<ApplicationUserCourse>();
        }

        [Required]
        [MaxLength(GlobalConstants.CourseTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [Range(GlobalConstants.CoursePointsMinValue, GlobalConstants.CoursePointsMaxValue)]
        public int Points { get; set; }

        public virtual ICollection<ApplicationUserCourse> ApplicationUsersInCourses { get; set; }
    }
}
