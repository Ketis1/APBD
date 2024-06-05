using apbd10.Contexts;
using apbd10.Exceptions;
using apbd10.ResponseModels;
using apbd10.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/accounts/{accountId:int}", async (int id, IAccountService service) =>
{
    try
    {
        return Results.Ok(await service.GetAccountByIdAsync(id));
    }
    catch (NotFoundException e)
    {
        return Results.NotFound(e.Message);
    }
});

app.MapPost("/api/products", async (AddProductRequestModel newproduct, IProductService service) =>
{
    if (newproduct==null)
    {
        return Results.BadRequest("Invalid product data");
    }

    try
    {
        await service.AddProductAsync(newproduct);
        return Results.Ok();

    }
    catch (Exception e)
    {
        return Results.BadRequest("some exception");
    }
});

app.Run();

