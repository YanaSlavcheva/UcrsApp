namespace Ucrs.Services.Data.Courses
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Ucrs.Data.Common.Repositories;
    using Ucrs.Data.Models;

    public class CoursesDataService : ICoursesDataService
    {
        private readonly IDeletableEntityRepository<Course> courses;

        public CoursesDataService(IDeletableEntityRepository<Course> courses)
        {
            this.courses = courses;
        }

        public async Task Add(Course course)
        {
            this.courses.Add(course);
            await this.courses.SaveChangesAsync();
        }

        public IQueryable<Course> GetAll() => this.courses.All();

        public async Task<Course> GetByIdAsync(int id) => await this.courses.GetByIdAsync(id);

        public IQueryable<int> GetAllCourseIdsByUser(string userId) =>
            this.courses
                .All()
                .Where(c => c.ApplicationUsersInCourses.Any(auc => auc.ApplicationUserId == userId))
                .Select(c => c.Id);
    }
}
