using Expense_Tracker.DAL;
using Expense_Tracker.DAL.Repositories.Implementations;
using Expense_Tracker.DAL.Repositories.Interfaces;
using Expense_Tracker.Models;
using Expense_Tracker.Services.Implementations;
using Expense_Tracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseConnection")));

//Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Services
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<IDashboardService, DashboardService>();
builder.Services.AddTransient<ILogInOrSignUpService, LogInOrSignUpService>();

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VFhhQlJBfVldX3xLflF1VWBTfFp6dFFWESFaRnZdQV1mSX1TdUFhXHxXeHFW");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
