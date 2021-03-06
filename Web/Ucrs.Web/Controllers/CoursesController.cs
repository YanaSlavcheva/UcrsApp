﻿namespace Ucrs.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Ucrs.Common;
    using Ucrs.Data.Models;
    using Ucrs.Services.Courses;
    using Ucrs.Services.Data.Courses;
    using Ucrs.Web.Infrastructure.Extensions;
    using Ucrs.Web.Infrastructure.Mapping;
    using Ucrs.Web.ViewModels.Courses;

    public class CoursesController : BaseController
    {
        private readonly ICoursesDataService coursesData;

        private readonly ICoursesBusinessService coursesBusiness;

        public CoursesController(
            ICoursesDataService coursesData,
            ICoursesBusinessService coursesBusiness)
        {
            this.coursesData = coursesData;
            this.coursesBusiness = coursesBusiness;
        }

        [HttpGet]
        public IActionResult All()
        {
            var userId = this.User.GetId();

            var courses = this.coursesData
                .GetAll()
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

            await this.coursesData.Add(course);

            return this.Ok(Mapper.Map<CourseViewModel>(course));
        }

        [HttpGet]
        public IActionResult GetUpdate(int id)
        {
            var courseForUpdate = this.coursesData
                .GetAll()
                .Where(c => c.Id == id)
                .To<CourseBindingModel>()
                .FirstOrDefault();

            return this.Ok(courseForUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]CourseBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.GetFirstError());
            }

            var course = await this.coursesData.GetByIdAsync(model.Id);

            Mapper.Map(model, course);

            await this.coursesData.Update(course);

            return this.Ok(Mapper.Map<CourseViewModel>(course));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]CourseBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.GetFirstError());
            }

            var course = await this.coursesData.GetByIdAsync(model.Id);

            Mapper.Map(model, course);

            await this.coursesData.Delete(course);

            return this.Ok(Mapper.Map<CourseViewModel>(course));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterForCourse(int id)
        {
            var course = await this.coursesData.GetByIdAsync(id);

            if (course == null)
            {
                return this.NotFound();
            }

            var userId = this.User.GetId();
            var userCourseIds = this.coursesData.GetAllCourseIdsByUser(userId).ToList();

            var userPointsFromCourses = this.coursesData
                .GetAll()
                .Where(c => userCourseIds.Contains(c.Id))
                .Sum(c => c.Points);

            if ((userPointsFromCourses + course.Points) > GlobalConstants.MaxCoursePointsPerUser)
            {
                return this.BadRequest("Student cannot register in any more courses because of the points restriction.");
            }

            var isUserEnrolledInCourse = this.coursesBusiness.IsUserEnrolled(userId, course.Id);

            if (isUserEnrolledInCourse)
            {
                return this.BadRequest("Student is already registered in this course.");
            }

            await this.coursesBusiness.RegisterUser(userId, course.Id);

            return this.Ok();
        }
    }
}
