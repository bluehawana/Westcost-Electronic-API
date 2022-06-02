using api.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//https://wckeyvc.vault.azure.net/secrets/WestCostConnectionString/9cc5c7017c9f46b39500623b48694833

//Get our Azure KeyVault secret......

var secretUri = builder.Configuration.GetSection("KeyVaultSecret:SqlConnection").Value;

var keyVaultToken = new AzureServiceTockenProvider().KeyVaultTokenCallback;

var KeyVaultClient = new KeyValutClient(new KeyVaultClient.AuthenticationCallback(keyVaultToken));




builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
