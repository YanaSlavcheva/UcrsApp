namespace Ucrs.Tests
{
    using System;
    using Ucrs.Data.Common.Repositories;
    using Ucrs.Data.Models;
    using Ucrs.Data.Repositories;
    using Ucrs.Services.Data.Courses;
    using Xunit;

    public class CoursesDataServiceTests
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        private readonly CoursesDataService coursesDataService;

        public CoursesDataServiceTests()
        {
            this.coursesRepository = new InMemoryDeletableEntityRepository<Course, int>();
            this.coursesDataService = new CoursesDataService(this.coursesRepository);
        }

        [Fact]
        public void GetByIdAsync_ShouldReturnCourseIfExistingIdIsGiven()
        {
            var fakeId = 1;
            var fakeCourse = new Course
            {
                Id = fakeId
            };

            this.coursesRepository.Add(fakeCourse);

            var searchResult = this.coursesDataService.GetByIdAsync(fakeId).GetAwaiter().GetResult();

            Assert.NotNull(searchResult);
            Assert.Equal(fakeId, searchResult.Id);
        }

        [Fact]
        public void GetByIdAsync_ShouldReturnNullIfNonExistingIdIsGiven()
        {
            var fakeId = 1;
            var fakeCourse = new Course
            {
                Id = fakeId
            };

            this.coursesRepository.Add(fakeCourse);

            var searchResult = this.coursesDataService.GetByIdAsync(2).GetAwaiter().GetResult();

            Assert.Null(searchResult);
        }
    }
}
