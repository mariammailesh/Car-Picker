using Car_Picker_API.Interfaces;
using Car_Picker_API.Services;
using Car_Picker_API.Services.Car_Picker_API.Servicess;
using CarPicker_API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB Context configuration injection
builder.Services.AddDbContext<CarPickerDbContext>(options =>
options.UseSqlServer("Data Source=DESKTOP-FB86LSD\\SQLSERVER;Initial Catalog=Sayarti_API_Db;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));
// injected Classes and Interfaces configuration
//builder.Services.AddScoped<ILookupInterface, LookupAppService>();//<the injected interface, the class that implenents the injected interface> //and thats how we configure the dependency injection for the interface and the class that implements it
builder.Services.AddScoped<IUserAuthenticationInterface, AuthenticationAppServices>();
builder.Services.AddScoped<ILookupInterface, LookupAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/uploads" // host/uploads/filename.extansions
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
