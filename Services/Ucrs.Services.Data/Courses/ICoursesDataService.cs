namespace Ucrs.Services.Data.Courses
{
    using System.Linq;
    using System.Threading.Tasks;

    using Ucrs.Data.Models;

    public interface ICoursesDataService
    {
        Task Add(Course course);

        Task Update(Course course);

        IQueryable<Course> GetAll();

        Task<Course> GetByIdAsync(int id);

        IQueryable<int> GetAllCourseIdsByUser(string userId);
    }
}
