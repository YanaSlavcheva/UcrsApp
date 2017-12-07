namespace Ucrs.Services.Courses
{
    using System.Linq;
    using System.Threading.Tasks;
    using Ucrs.Data.Common.Repositories;
    using Ucrs.Data.Models;

    public class CoursesBusinessService : ICoursesBusinessService
    {
        private readonly IRepository<ApplicationUserCourse> applicationUsersForCourses;

        public CoursesBusinessService(
            IRepository<ApplicationUserCourse> applicationUsersForCourses)
        {
            this.applicationUsersForCourses = applicationUsersForCourses;
        }

        public async Task RegisterUser(string userId, int courseId)
        {
            var applicationUserForCourse = new ApplicationUserCourse()
            {
                ApplicationUserId = userId,
                CourseId = courseId
            };

            this.applicationUsersForCourses.Add(applicationUserForCourse);
            await this.applicationUsersForCourses.SaveChangesAsync();
        }

        public bool IsUserEnrolled(string userId, int courseId) =>
            this.applicationUsersForCourses
                .All()
                .Any(aufc => aufc.ApplicationUserId == userId && aufc.CourseId == courseId);
    }
}
