namespace Ucrs.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using Ucrs.Data.Common.Repositories;
    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Extensions;
    using Ucrs.Web.Infrastructure.Mapping;
    using Ucrs.Web.ViewModels.Courses;
    using Microsoft.EntityFrameworkCore;

    public class CoursesController : BaseController
    {
        private readonly IDeletableEntityRepository<Course> repository;

        public CoursesController(IDeletableEntityRepository<Course> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult All()
        {
            var courses = this.repository
                .All()
                .Include(c => c.ApplicationUsersInCourses)
                .ThenInclude(auc => auc.ApplicationUser)
                //.To<CourseViewModel>()
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

            this.repository.Add(course);
            await this.repository.SaveChangesAsync();

            return this.Ok(Mapper.Map<CourseViewModel>(course));
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsDone(int id)
        {
            ////var course = await this.repository.GetByIdAsync(id);

            ////if (course == null)
            ////{
            ////    return this.NotFound();
            ////}

            ////if (course.AuthorId != this.User.GetId())
            ////{
            ////    return this.Forbid(JwtBearerDefaults.AuthenticationScheme);
            ////}

            ////if (!course.IsDone)
            ////{
            ////    course.IsDone = true;

            ////    this.repository.Update(course);
            ////    await this.repository.SaveChangesAsync();
            ////}

            return this.Ok();
        }
    }
}
