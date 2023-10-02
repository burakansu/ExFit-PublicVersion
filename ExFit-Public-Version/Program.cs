using System.Text.Json.Serialization;
using Data.Entities.ExFit;
using ExFit.Custom;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
     .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddSessionStateTempDataProvider();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPageMetaData, PageMetaData>();

builder.Services.AddScoped<User>();
builder.Services.AddScoped<Company>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LogIn}/{action=SignIn}/{id?}");
app.Run();