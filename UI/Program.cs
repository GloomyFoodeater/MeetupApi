using ApplicationCore.Interfaces.Services;
using ApplicationCore.MapperProfiles;
using ApplicationCore.Services;
using Microsoft.OpenApi.Models;
using static Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    var scheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme"

    };
    var requirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    };

    options.AddSecurityDefinition("Bearer", scheme);
    options.AddSecurityRequirement(requirement);
});

// Add ApplicationCore services
builder.Services.AddAutoMapper(typeof(MeetupProfile));
builder.Services.AddTransient<MeetupService>();

// Add Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

// Fill database
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    await seeder.SeedAsync();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
