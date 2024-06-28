using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using SmartsearchApi.Data;
using SmartsearchApi.Mappings;
using SmartsearchApi.Repositories.AbstractRepository;
using SmartsearchApi.Repositories.Projects;
using SmartsearchApi.Repositories.Publications;
using SmartsearchApi.Repositories.Researchers;
using SmartsearchApi.Repositories.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthorization();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // pour les dépendances circulaires
    // TODO: créer des DTOs spécifiques pour les entités avec des dépendances circulaires
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Repositories
builder.Services.AddScoped(typeof(IAbstractRepository<>), typeof(AbstractRepository<>));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IPublicationRepository, PublicationRepository>();
builder.Services.AddScoped<IResearcherRepository, ResearcherRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMapper
builder.Services.AddAutoMapper(
    typeof(ProjectMappingProfile),
    typeof(ResearcherMappingProfile),
    typeof(PublicationMappingProfile));

// Cors
// var allowedOrigins = "_allowedOrigins";
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(
        policy =>
        {
            //policy.WithOrigins("http://localhost:5173")
            //  .WithMethods("GET", "POST", "PUT", "DELETE")
            //.AllowAnyHeader();

            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        })
);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "SmartsearchApi", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();
// app.UseAuthentication();
app.UseCors();

app.MapGroup("/identity").MapIdentityApi<IdentityUser>();
app.MapControllers();

app.Run();