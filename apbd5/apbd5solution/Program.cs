using apbd5solution.Endpoints;
using apbd5solution.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.RegisterValidators();
//builder.Services.AddScoped<IAnimalsRepository, AnimalsRepository>();
//builder.Services.AddScoped<IAnimalsService, AnimalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterAnimalsEndpoints();
app.MapControllers();

app.Run();