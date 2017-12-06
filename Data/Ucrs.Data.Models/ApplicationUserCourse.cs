namespace Ucrs.Data.Models
{
    public class ApplicationUserCourse
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
