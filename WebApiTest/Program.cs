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



app.MapControllers();

app.Run();