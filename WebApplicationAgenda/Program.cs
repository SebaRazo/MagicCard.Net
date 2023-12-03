using Microsoft.EntityFrameworkCore;
using WebApplicationAgenda.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Context
builder.Services.AddDbContext<AgendaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));  //conexion a la base de datos
});

#region DependencyInjections
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IContactRepository, ContactRepository>();
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
