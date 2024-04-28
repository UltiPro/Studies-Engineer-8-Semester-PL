#pragma warning disable ASP0014

using SoapCore;
using Microsoft.EntityFrameworkCore;
using Projekt_1_Web_Serwisy.Database;
using Projekt_1_Web_Serwisy.SOAPMotor;

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
}
app.UseStaticFiles();*/

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IMotorService>("/Motor.wsdl", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

//app.UseAuthorization();

//app.MapRazorPages();

app.Run();
