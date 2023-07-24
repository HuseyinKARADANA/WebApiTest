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
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlCon"));
            });
        }

        //User Dependency Injection
        public static void ConfigureUserManager(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
        }
        public static void ConfigureGenericDal(this IServiceCollection services)
        {
            services.AddScoped<IUserDal, EfUserDal>();
        }

        //Address Dependency Injection

        public static void ConfigureAddressManager(this IServiceCollection services)
        {
            services.AddScoped<IAddressService, AddressManager>();
        }

        public static void ConfigureAddressDal(this IServiceCollection services)
        {
            services.AddScoped<IAddressDal, EfAddressDal>();
        }

        //Category Dependency Injection

        public static void ConfigureCategoryManager(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryManager>();
        }

        public static void ConfigureCategoryDal(this IServiceCollection services)
        {
            services.AddScoped<ICategoryDal, EfCategoryDal>();
        }

        //SubCategory Dependency Injection

        public static void ConfigureSubCategoryManager(this IServiceCollection services)
        {
            services.AddScoped<ISubCategoryService, SubCategoryManager>();
        }

        public static void ConfigureSubCategoryDal(this IServiceCollection services)
        {
            services.AddScoped<ISubCategoryDal, EfSubCategoryDal>();
        }

        //CategoryDetail Dependency Injection

        public static void ConfigureCategoryDetailManager(this IServiceCollection services)
        {
            services.AddScoped<ICategoryDetailService, CategoryDetailManager>();
        }

        public static void ConfigureCategoryDetailDal(this IServiceCollection services)
        {
            services.AddScoped<ICategoryDetailDal, EfCategoryDetailDal>();
        }
    }
}
