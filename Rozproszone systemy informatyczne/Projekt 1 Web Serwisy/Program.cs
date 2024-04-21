using SoapCore;
using Projekt_1_Web_Serwisy.SOAPHelloWorld;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSoapCore();
builder.Services.AddScoped<ISOAPService, SOAPService>();

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
    endpoints.UseSoapEndpoint<ISOAPService>("/Service.wsdl", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

//app.UseAuthorization();

//app.MapRazorPages();

app.Run();
