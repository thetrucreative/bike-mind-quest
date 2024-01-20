using bike_mind_quest.Controllers.GBFSDataController;
using bike_mind_quest.Controllers.QuizController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using bike_mind_quest.Models.GameStateServiceModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<GeneralBikeshareFeedSpecificationDataController>();
builder.Services.AddSingleton<CareemQuizController>();
builder.Services.AddSingleton<DonkeyRepublicQuizController>();
builder.Services.AddSingleton<LyftQuizController>();
builder.Services.AddSingleton<GameStateService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Serve static files
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSession(); // Add this line to enable session middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AuthenticateUser}/{action=SignUp}/{id?}");

app.MapGet("/bike-mind-quest", async context =>
{
    // Retrieve the IWebHostEnvironment from the service provider
    var env = app.Services.GetRequiredService<IWebHostEnvironment>();

    var filePath = Path.Combine(env.WebRootPath, "BikeMindQuest.html");
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync(filePath);
});

app.MapFallback(context =>
{
    context.Response.Redirect("/bike-mind-quest");
    return Task.CompletedTask;
});

app.Run();
