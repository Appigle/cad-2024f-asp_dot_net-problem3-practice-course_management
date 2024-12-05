using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lei_Chen_Prob_Asst_3.Data;
using PA3.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcCourseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MvcCourseContext") ?? throw new InvalidOperationException("Connection string 'MvcCourseContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<StudentService>();
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
    pattern: "{controller=Course}/{action=Index}/{id?}");

app.Run();
