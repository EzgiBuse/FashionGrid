using FashionGrid.UI.Services.IServices;
using FashionGrid.UI.Services.Services;
using FashionGrid.UI.Utilities;
using FashionGrid.UI.Validators.DtoValidators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

// Add FluentValidation
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddHttpClient<IUserService,UserService>();


builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.ExpireTimeSpan = TimeSpan.FromHours(24);
    opt.LoginPath = "/Login/Login";
    opt.AccessDeniedPath = "/Auth/AccessDenied";
});

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
