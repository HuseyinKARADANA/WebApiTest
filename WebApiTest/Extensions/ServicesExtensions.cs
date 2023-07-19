using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repository;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;

namespace WebApiTest.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlCon"));
            });
        }


        public static void ConfigureUserManager(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
        }
        public static void ConfigureGenericDal(this IServiceCollection services)
        {
            services.AddScoped<IUserDal, EfUserDal>();
        }
    }
}
