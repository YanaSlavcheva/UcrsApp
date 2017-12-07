namespace Ucrs.Services.Courses
{
    using System.Linq;

    using Ucrs.Data.Models;

    public interface ICoursesDataService
    {
        IQueryable<Course> GetAll();
    }
}
