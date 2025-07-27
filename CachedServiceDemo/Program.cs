using CachedServiceDemo.Abstract;
using CachedServiceDemo.Context;
using CachedServiceDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<CachedServiceDemoDBContext>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.Decorate<IEmployeeService, CachedEmployeeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
