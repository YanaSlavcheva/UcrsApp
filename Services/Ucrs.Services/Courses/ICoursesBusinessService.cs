namespace Ucrs.Services.Courses
{
    using System.Threading.Tasks;

    public interface ICoursesBusinessService
    {
        Task RegisterUser(string userId, int courseId);

        bool IsUserEnrolled(string userId, int courseId);
    }
}
