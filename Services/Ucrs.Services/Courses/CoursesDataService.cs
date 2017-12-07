namespace Ucrs.Services.Courses
{
    using System.Linq;

    using Ucrs.Data.Common.Repositories;
    using Ucrs.Data.Models;

    public class CoursesDataService : ICoursesDataService
    {
        private readonly IDeletableEntityRepository<Course> courses;

        public CoursesDataService(IDeletableEntityRepository<Course> courses)
        {
            this.courses = courses;
        }

        public IQueryable<Course> GetAll() => this.courses.All();
    }
}
