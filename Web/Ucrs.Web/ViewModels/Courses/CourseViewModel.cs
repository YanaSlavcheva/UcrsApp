namespace Ucrs.Web.ViewModels.Courses
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Mapping;

    public class CourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Points { get; set; }

        public ICollection<string> UserNames { get; set; }

        public string UserNamesList => this.UserNames.Any() ? string.Join(',', this.UserNames) : string.Empty;

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                .ForMember(
                    m => m.UserNames,
                    opt => opt.MapFrom(e =>
                        e.ApplicationUsersInCourses
                            .Select(auic => auic.ApplicationUser.UserName)));
        }
    }
}
