using LoginRazorBlog;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthStorage(builder.Configuration.GetConnectionString("DefaultDatabase"));


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   // app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    
  //  app.UseExceptionHandler("/Error2/Error2");
    app.UseExceptionHandler(new ExceptionHandlerOptions
    {
        ExceptionHandlingPath = "/Error2/Error2"
    });

    //app.UseWhen(
    //    context => context.Request.Path.StartsWithSegments("/Administration", StringComparison.OrdinalIgnoreCase)),
    //    appBuilder => appBuilder.UseExceptionHandler("/Administration/Error"));

    //app.UseWhen(
    //    context => context.Request.Path.StartsWithSegments("/Production", StringComparison.OrdinalIgnoreCase)),
    //    appBuilder => appBuilder.UseExceptionHandler("/Production/Error"));

    //// the catch-all is a bit tricky and is needs to be updated if you add a new area.
    //app.UseWhen(
    //    context => !context.Request.Path.StartsWithSegments("/Administration", StringComparison.OrdinalIgnoreCase)) &&
    //               !context.Request.Path.StartsWithSegments("/Production", StringComparison.OrdinalIgnoreCase)),
    //    appBuilder => appBuilder.UseExceptionHandler("/Error"));

}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
