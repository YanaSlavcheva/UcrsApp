namespace Ucrs.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Ucrs.Data.Common.Repositories;
    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Extensions;
    using Ucrs.Web.Infrastructure.Mapping;
    using Ucrs.Web.ViewModels.Courses;
    using Ucrs.Common;

    public class CoursesController : BaseController
    {
        private readonly IDeletableEntityRepository<Course> courses;

        private readonly IRepository<ApplicationUserCourse> applicationUsersForCourses;

        public CoursesController(
            IDeletableEntityRepository<Course> courses,
            IRepository<ApplicationUserCourse> applicationUsersForCourses)
        {
            this.courses = courses;
            this.applicationUsersForCourses = applicationUsersForCourses;
        }

        [HttpGet]
        public IActionResult All()
        {
            var userId = this.User.GetId();

            var courses = this.courses
                .All()
                .Include(c => c.ApplicationUsersInCourses)
                .ThenInclude(auc => auc.ApplicationUser)
                .To<CourseViewModel>(new { userId })
                .OrderByDescending(cvm => cvm.IsUserRegistered)
                .ToList();

            return this.Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CourseBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.GetFirstError());
            }

            var course = Mapper.Map<Course>(model);

            this.courses.Add(course);
            await this.courses.SaveChangesAsync();

            return this.Ok(Mapper.Map<CourseViewModel>(course));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterForCourse(int id)
        {
            var course = await this.courses.GetByIdAsync(id);

            if (course == null)
            {
                return this.NotFound();
            }

            var userId = this.User.GetId();
            var userCourseIds = this.applicationUsersForCourses
                .All()
                .Where(aufc => aufc.ApplicationUserId == userId)
                .Select(aufc => aufc.CourseId)
                .ToList();

            var userPointsFromCourses = this.courses
                .All()
                .Where(c => userCourseIds.Contains(c.Id))
                .Sum(c => c.Points);

            if (userPointsFromCourses >= GlobalConstants.MaxCoursePointsPerUser)
            {
                return this.BadRequest("Student cannot register in any more courses. The student has maximum points already.");
            }

            var isUserEnrolledInCourse = this.applicationUsersForCourses
                .All()
                .Any(aufc => aufc.ApplicationUserId == userId && aufc.CourseId == id);

            if (isUserEnrolledInCourse)
            {
                return this.BadRequest("Student is already registered in this course.");
            }

            var applicationUserForCourse = new ApplicationUserCourse()
            {
                ApplicationUserId = userId,
                CourseId = course.Id
            };

            this.applicationUsersForCourses.Add(applicationUserForCourse);
            await this.courses.SaveChangesAsync();

            return this.Ok();
        }
    }
}
