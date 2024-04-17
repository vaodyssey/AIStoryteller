using AIStoryteller.Components;
using AIStoryteller.Configs;
using AIStoryteller_Repository.Migrations;
using AIStoryteller_Repository.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDistributedMemoryCache();    
builder.Services.AddRepositories();
builder.Services.AddDatabase();
builder.Services.AddServices();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AIStorytellerDbContext>();
await context.Database.MigrateAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseSession();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapHub<ProgressHub>("/progressHub");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
