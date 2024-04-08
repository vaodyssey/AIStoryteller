using AIStoryteller_Repository.Services;
using AIStoryteller_Repository.Services.Implementation;

namespace AIStoryteller.Configs
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            //services.AddDbContext<DummyDbContext>(options => options.UseSqlServer(GetConnectionString()));
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<IPdfService, PdfService>();
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
