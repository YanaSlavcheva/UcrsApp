namespace Ucrs.Web.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    using Ucrs.Services.Courses;
    using Ucrs.Services.Data.Courses;
    using Ucrs.Services.Messaging;

    public static class IServiceResultExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISmsSender, NullMessageSender>();
            services.AddTransient<ICoursesDataService, CoursesDataService>();
            services.AddTransient<ICoursesBusinessService, CoursesBusinessService>();
        }
    }
}
