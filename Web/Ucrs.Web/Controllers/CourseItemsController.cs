namespace Ucrs.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Ucrs.Data.Common.Repositories;
    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Extensions;
    using Ucrs.Web.Infrastructure.Mapping;
    using Ucrs.Web.ViewModels.CourseItems;

    using AutoMapper;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;

    public class CourseItemsController : BaseController
    {
        private readonly IDeletableEntityRepository<CourseItem> repository;

        public CourseItemsController(IDeletableEntityRepository<CourseItem> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult All()
        {
            var userId = this.User.GetId();
            var courseItems = this.repository.All().Where(t => t.AuthorId == userId).To<CourseItemViewModel>().ToList();

            return this.Ok(courseItems);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CourseItemBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.GetFirstError());
            }

            var courseItem = Mapper.Map<CourseItem>(model);
            courseItem.AuthorId = this.User.GetId();

            this.repository.Add(courseItem);
            await this.repository.SaveChangesAsync();

            return this.Ok(Mapper.Map<CourseItemViewModel>(courseItem));
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsDone(int id)
        {
            var courseItem = await this.repository.GetByIdAsync(id);

            if (courseItem == null)
            {
                return this.NotFound();
            }

            if (courseItem.AuthorId != this.User.GetId())
            {
                return this.Forbid(JwtBearerDefaults.AuthenticationScheme);
            }

            if (!courseItem.IsDone)
            {
                courseItem.IsDone = true;

                this.repository.Update(courseItem);
                await this.repository.SaveChangesAsync();
            }

            return this.Ok();
        }
    }
}
