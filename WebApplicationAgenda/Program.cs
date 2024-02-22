using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using WebApplicationAgenda.Data;
using WebApplicationAgenda.Data.Repository.Implementations;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Models.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("WebApplicationAgendaBearerAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Ac� pegar el token generado al loguearse."
    });
    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "WebApplicationAgendaBearerAuth" } 
                }, new List<string>() }
    });
    

});

    // Add Context
    builder.Services.AddDbContext<AgendaContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));  
    });


    builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticaci�n que tenemos que elegir despu�s en PostMan para pasarle el token
        .AddJwtBearer(options => //Ac� definimos la configuraci�n de la autenticaci�n. le decimos qu� cosas queremos comprobar. La fecha de expiraci�n se valida por defecto.
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
            };
            /////////////////////////////////aca para agregar el rol de usuario para poder ver luego de agarrarlo del front /////////////
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    // Agregar el rol como claim adicional al principal del usuario
                    var identity = context.Principal.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        var roleClaim = context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                        if (roleClaim != null)
                        {
                            // Obtener el rol del token y agregarlo como claim adicional
                            var role = roleClaim.Value;
                            identity.AddClaim(new Claim(ClaimTypes.Role, role));
                        }
                    }

                    return Task.CompletedTask;
                }
            };
            //////////////////////////////////////////////////
        }
    );

//Cors para que el navegador pueda acceder a los endpoints
builder.Services.AddCors(options =>
      {
          options.AddPolicy(
              name: "AllowOrigin",
              builder =>
              {
                  builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
              });
      });

 
 


var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new CardProfile());
        cfg.AddProfile(new SaleProfile());
        cfg.AddProfile(new UserProfile());
        
    });
    var mapper = config.CreateMapper();


    #region DependencyInjections
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ICardRepository, CardRepository>();
    builder.Services.AddScoped<ISaleRepository, SaleRepository>();
    builder.Services.AddScoped<IMapper, Mapper>();
    #endregion



    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowOrigin");


    app.UseHttpsRedirection();
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();


