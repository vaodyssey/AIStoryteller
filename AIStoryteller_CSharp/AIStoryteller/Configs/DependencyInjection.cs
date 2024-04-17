using AIStoryteller_Repository.Migrations;
using AIStoryteller_Repository.Repositories;
using AIStoryteller_Repository.Repositories.Implementation;
using AIStoryteller_Repository.Services;
using AIStoryteller_Repository.Services.Implementation;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace AIStoryteller.Configs
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AIStorytellerDbContext>(options => 
                options.UseSqlServer(GetConnectionString()), 
                ServiceLifetime.Transient);
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStorytellerService, StorytellerService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IPageService, PageService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSignalR();
            return services;
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:DefaultConnection"];

            return strConn;
        }
    }

}
