using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Contexts;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WebApiTest.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureUserManager();
builder.Services.ConfigureGenericDal();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
          .AddCookie(options =>
          {
              options.Cookie.Name = "ShopListApp";
              options.LoginPath = "/Session/Login"; // Giriþ sayfasýnýn yolunu belirtin
              options.AccessDeniedPath = "/Session/Login"; // Yetkisiz eriþimde yönlendirilecek yol
          });
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
//{
//    options.Cookie.Name = "ShopListApp";
//    options.LoginPath = "/Users/login";//login yapma sayfasý
//    options.AccessDeniedPath = "/Users/login";//yetkisizse buraya atýyor
//});

// Autofac container oluþturma


//containerBuilder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
//containerBuilder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();


// var containerBuilder = new ContainerBuilder();
//containerBuilder.Populate(builder.Services); // Microsoft DI'dan Autofac'a geçiþ

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer(); 
builder.Logging.ClearProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder.WithOrigins("http://localhost:7297").AllowAnyHeader());

app.UseAuthentication(); // UseAuthentication metodu düzenlendi
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    // Endpoint yapýlandýrmalarýnýzý burada tanýmlayýn
//    endpoints.MapControllers();
//});
//app.UseRouting();//chat gpt ekledi

app.MapControllers();

app.Run();