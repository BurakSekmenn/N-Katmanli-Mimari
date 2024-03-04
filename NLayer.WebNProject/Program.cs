using Autofac.Extensions.DependencyInjection;
using Autofac;
using NLayer.NLayer.WebNProject.Modules;
using Microsoft.EntityFrameworkCore;
using NLayer.Repository.Context;
using NLayer.Service.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"), options =>
    {
        options.MigrationsAssembly("NLayer.Repository");
    });
});


builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory()
);
builder.Host.ConfigureContainer<ContainerBuilder>(configureDelegate =>
    configureDelegate.RegisterModule(new RepoServiceModul()));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
