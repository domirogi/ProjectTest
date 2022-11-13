using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Common;
using Project.DAL.Data;
using Project.DAL.Data.Repoaitoty;
using Project.DAL.Data.Repository;
using Project.Model.Common;
using Project.Repository.Common.IRepository;
using Project.Repository.Repository;
using Project.Service.Common.Interface;
using Project.Service.Service;

namespace Project.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
            => services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<VehicleDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("ProjectTestDb")));

        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMakeService, MakeService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IMakeRepository, MakeRepository>();





        }
    }
}
