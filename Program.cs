using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data;
using SchoolManagmentSystem.Models;
using SchoolManagmentSystem.Services;
using SchoolManagmentSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Add DbContext
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContex>(opt => opt.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

//Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContex>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.UseHttpsRedirection();

app.UseRouting();



app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

await SeedData(app);

app.Run();

// Seed method at the bottom of Program.cs
static async Task SeedData(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContex>();

    // Create roles
    string[] roles = { "Admin", "Student" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // ── ADMIN ──────────────────────────────────────────
    var adminEmail = "admin@school.com";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FullName = "Administrator"
        };
        await userManager.CreateAsync(admin, "Admin@123456");
        await userManager.AddToRoleAsync(admin, "Admin");
    }

    // ── STUDENT 1 ──────────────────────────────────────
    var student1Email = "student1@school.com";
    if (await userManager.FindByEmailAsync(student1Email) == null)
    {
        var user1 = new ApplicationUser
        {
            UserName = student1Email,
            Email = student1Email,
            FullName = "John Smith"
        };
        await userManager.CreateAsync(user1, "Student@123456");
        await userManager.AddToRoleAsync(user1, "Student");

        context.Students.Add(new Student
        {
            FullName = "John Smith",
            Email = student1Email,
            DateOfBirth = new DateTime(2005, 3, 15),
            ApplicationUserId = user1.Id
        });
    }

    // ── STUDENT 2 ──────────────────────────────────────
    var student2Email = "student2@school.com";
    if (await userManager.FindByEmailAsync(student2Email) == null)
    {
        var user2 = new ApplicationUser
        {
            UserName = student2Email,
            Email = student2Email,
            FullName = "Maria Johnson"
        };
        await userManager.CreateAsync(user2, "Student@123456");
        await userManager.AddToRoleAsync(user2, "Student");

        context.Students.Add(new Student
        {
            FullName = "Maria Johnson",
            Email = student2Email,
            DateOfBirth = new DateTime(2006, 7, 22),
            ApplicationUserId = user2.Id
        });
    }

    await context.SaveChangesAsync();
}
