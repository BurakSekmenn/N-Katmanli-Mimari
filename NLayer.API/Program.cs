using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Repository.Context;
using NLayer.Service.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ValidateFilterAttribute());
}).AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});//ModelStateInvalidFilter devre dýþý býrakýldý. Bizim yukarýdaki filtre kullanmamýzýn sebebi bu








// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();



builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));


// Biz Burada bu verileri repository ve service modulüne taþýdýk


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"), options =>
    {
        options.MigrationsAssembly("NLayer.Repository");
    });
});


builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory()
);
builder.Host.ConfigureContainer<ContainerBuilder>(configureDelegate =>
    configureDelegate.RegisterModule(new RepoServiceModul()));



// middleware kýsmý
var app = builder.Build();

app.Use((context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:7046");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
    return next();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCustomException();






app.UseAuthorization();

app.MapControllers();

app.Run();
