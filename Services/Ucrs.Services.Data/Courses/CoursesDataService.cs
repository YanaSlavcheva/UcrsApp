namespace Ucrs.Services.Data.Courses
{
    using System.Linq;
    using System.Threading.Tasks;

    using Ucrs.Data.Common.Repositories;
    using Ucrs.Data.Models;

    public class CoursesDataService : ICoursesDataService
    {
        private readonly IDeletableEntityRepository<Course> courses;

        private readonly IRepository<ApplicationUserCourse> applicationUsersForCourses;

        public CoursesDataService(
            IDeletableEntityRepository<Course> courses,
            IRepository<ApplicationUserCourse> applicationUsersForCourses)
        {
            this.courses = courses;
            this.applicationUsersForCourses = applicationUsersForCourses;
        }

        public async Task Add(Course course)
        {
            this.courses.Add(course);
            await this.courses.SaveChangesAsync();
        }

        public IQueryable<Course> GetAll() => this.courses.All();

        public async Task<Course> GetByIdAsync(int id) => await this.courses.GetByIdAsync(id);

        public IQueryable<int> GetAllCourseIdsByUser(string userId) =>
            this.applicationUsersForCourses
                .All()
                .Where(aufc => aufc.ApplicationUserId == userId)
                .Select(aufc => aufc.CourseId);
    }
}
