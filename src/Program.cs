using BarbershopApi.Data;
using BarbershopApi.Middleware;
using BarbershopApi.Repositories;
using BarbershopApi.Repositories.Interfaces;
using BarbershopApi.Services;
using BarbershopApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  Persistence --- (é possível trocar UseSqlite por UseNpgsql/UseSqlServer pra mudar o provedor) ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//  Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
builder.Services.AddScoped<IPrestationOfServiceRepository, PrestationOfServiceRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

//  Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProfessionalService, ProfessionalService>();
builder.Services.AddScoped<IPrestationOfServiceService, PrestationOfServiceService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
