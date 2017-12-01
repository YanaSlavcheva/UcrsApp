namespace Ucrs.Web.ViewModels.CourseItems
{
    using System.ComponentModel.DataAnnotations;

    using Ucrs.Data.Models;
    using Ucrs.Web.Infrastructure.Mapping;

    public class CourseItemBindingModel : IMapTo<CourseItem>
    {
        [Required]
        public string Title { get; set; }
    }
}
