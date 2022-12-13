using API.Filters;
using Microsoft.AspNetCore.Mvc;
using Service.Mapping;
using API.Middlewares;
using API.Modules;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Extension;
using Service.Extensions;
using Service.Validations;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Autofac;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
);

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserAddDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddScoped(typeof(AuthenticationFilterAttribute));
builder.Services.AddScoped(typeof(UserCheckFilterAttribute));
builder.Services.AddAutoMapper(typeof(UserProfile));


var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseMySql(builder.Configuration["DB_CONNECT"], new MySqlServerVersion(new Version(10, 4, 14)), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

    });
});


builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

builder.Services.InstallJwt(builder.Configuration);
builder.Services.InstallSwagger();


var app = builder.Build();

app.UseCors();
app.UseStaticFiles();

app.ConfigureSwagger();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

//app.UseWhen(httpContext => (httpContext.Request.Path.StartsWithSegments("/Users")), subApp => subApp.UseAuthMiddleware());

app.UseWhen(httpContext => (httpContext.Request.Path.Equals("/Auth/Register")  || httpContext.Request.Path.StartsWithSegments("/Users/Image") ||
(httpContext.Request.Method.Equals("POST") && httpContext.Request.Path.Equals("/Users"))), subApp => subApp.UseImageMiddleware());

app.UserCustomException();

app.MapControllers();

app.Run();