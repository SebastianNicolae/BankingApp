using BankingApp;
using BankingApp.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<BankingAppDbContext>(options =>
//options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("BankingAppDb")));

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

var app = builder
    .ConfigureConfigurationServices()
    .ConfigureServices()
    .ConfigureApiVersioningServices()
    .ConfigureDataBaseServices()
    .ConfigureSwaggerServices()
    .Build();


app.SeedDataBase()
   .ConfigurePipeline();
app.Run(); 
