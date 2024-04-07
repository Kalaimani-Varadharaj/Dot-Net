using FraudDetectionRepositoryPatternProject.BO;
using FraudDetectionRepositoryPatternProject.Models;
using FraudDetectionRepositoryPatternProject.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);


//Application service
builder.Services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();
builder.Services.AddScoped<TransactionHistoryBo>();

builder.Services.AddScoped<IFraudulentIncidentDetailRepository, FraudulentIncidentDetailRepository>();
builder.Services.AddScoped<FraudulentIncidentDetailBo>();

builder.Services.AddScoped<IUserRegisterRepository, UserRegisterRepository>();
builder.Services.AddScoped<UserRegisterBo>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FraudRiskManagementContext>(options =>
{
    options.UseSqlServer(connectionString);
});


// Add services to the container.
builder.Services.AddControllersWithViews();

//CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

//Inject IWebHost Environment
var env = app.Services.GetRequiredService<IWebHostEnvironment>();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

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
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserRegister}/{action=Login}/{id?}");

// Add a new route for TransactionHistoryController
app.MapControllerRoute(
    name: "transactionHistory",
    pattern: "TransactionHistory/{action=Create}/{id?}",
    defaults: new { controller = "TransactionHistory" });




app.Run();
