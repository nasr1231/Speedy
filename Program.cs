using Microsoft.AspNetCore.Identity;
using Speedy.Core.Mapping;
using Speedy.Data;
using System.Reflection;
using Speedy.Seeds;
using Microsoft.EntityFrameworkCore;
using Speedy.Services.User;

namespace Speedy;

public class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

        //builder.Services.AddScoped<UserManager<AppUser>>();

        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddControllersWithViews();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;
            options.User.RequireUniqueEmail = true;            
            options.Lockout.MaxFailedAccessAttempts = 3;
        });

        var app = builder.Build();
       
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        await DefaultRoles.SeedRolesAsync(roleManager);
        await DefaultUsers.SeedAdminUser(userManager);

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
