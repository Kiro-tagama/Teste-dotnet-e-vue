using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger"))
.WithMetadata(new ApiExplorerSettingsAttribute { IgnoreApi = true });

/*
#region  Categories

app.MapGet("/categories", () =>{})
.WithName("categories")
.WithOpenApi();

app.MapPost("/categories", () =>{})
.WithName("categories")
.WithOpenApi();

app.MapPut("/categories/{id}", () =>{})
.WithName("categories")
.WithOpenApi();

app.MapDelete("/categories/{id}", () =>{})
.WithName("categories")
.WithOpenApi();

#endregion

#region Products

app.MapGet("/products", () =>{})
.WithName("products")
.WithOpenApi();

app.MapPost("/products", () =>{})
.WithName("products")
.WithOpenApi();

app.MapPut("/products/{id}", () =>{})
.WithName("products")
.WithOpenApi();

app.MapDelete("/products/{id}", () =>{})
.WithName("products")
.WithOpenApi();

#endregion
*/
app.Run();