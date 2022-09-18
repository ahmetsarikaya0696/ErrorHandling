using ErrorHandling.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new CustomExceptionFilterAttribute() { ErrorPage = "Error1"});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// 1.Yol
//app.UseStatusCodePages("/text/plain","Hata bulundu. Durum kodu {0}");

// 2.Yol
app.UseStatusCodePages(async context =>
{
    context.HttpContext.Response.ContentType = "text/html";
    var statusCode = context.HttpContext.Response.StatusCode;
    await context.HttpContext.Response.WriteAsync($"<h1>Hata bulundu. Durum kodu {statusCode}</h1>");
});

// 3.Yol
//app.UseStatusCodePages()

//------------------------------------------------------------------------------------------------------

// 1.Yol
//app.UseExceptionHandler("/Home/Error");

// 2.Yol
//app.UseExceptionHandler(context =>
//{
//    context.Run(async page =>
//    {
//        page.Response.StatusCode = 500;
//        page.Response.ContentType = "text/html";
//        await page.Response.WriteAsync($"<h1>Hata bulundu. Hata kodu : {page.Response.StatusCode}</h1>");
//    });
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
