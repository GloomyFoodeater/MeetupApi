using ApplicationCore.Interfaces.Services;
using ApplicationCore.MapperProfiles;
using ApplicationCore.Services;
using static Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
