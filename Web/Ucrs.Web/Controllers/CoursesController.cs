namespace Ucrs.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Ucrs.Data.Common.Repositories;
    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Extensions;
    using Ucrs.Web.Infrastructure.Mapping;
    using Ucrs.Web.ViewModels.Courses;

    using AutoMapper;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;

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
            var userId = this.User.GetId();
            var courses = this.repository.All().Where(t => t.AuthorId == userId).To<CourseViewModel>().ToList();

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
            course.AuthorId = this.User.GetId();

            this.repository.Add(course);
            await this.repository.SaveChangesAsync();

            return this.Ok(Mapper.Map<CourseViewModel>(course));
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsDone(int id)
        {
            var course = await this.repository.GetByIdAsync(id);

            if (course == null)
            {
                return this.NotFound();
            }

            if (course.AuthorId != this.User.GetId())
            {
                return this.Forbid(JwtBearerDefaults.AuthenticationScheme);
            }

            if (!course.IsDone)
            {
                course.IsDone = true;

                this.repository.Update(course);
                await this.repository.SaveChangesAsync();
            }

            return this.Ok();
        }
    }
}
