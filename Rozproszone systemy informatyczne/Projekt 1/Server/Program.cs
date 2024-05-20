#pragma warning disable ASP0014

using SoapCore;
using Microsoft.EntityFrameworkCore;
using Projekt_1_Web_Serwisy.Database;
using Projekt_1_Web_Serwisy.SOAPMotor;
using Projekt_1_Web_Serwisy.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
}, ServiceLifetime.Transient);

builder.Services.AddSoapCore();
builder.Services.AddScoped<IMotorService, MotorService>();


// Add services to the container.
// builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}*/

//app.UseHttpsRedirection();
// app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IMotorService>("/Motor.wsdl", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

app.UseSoapExceptionMiddleware();

// app.UseAuthorization();

// app.MapRazorPages();

app.Run();
