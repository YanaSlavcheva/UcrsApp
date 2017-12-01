namespace Ucrs.Web.ViewModels.CourseItems
{
    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Mapping;

    public class CourseItemViewModel : IMapFrom<CourseItem>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDone { get; set; }
    }
}
