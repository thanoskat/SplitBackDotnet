
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Data;
using SplitBackDotnet.Endpoints.Authentication;
using SplitBackDotnet.Endpoints;
using SplitBackDotnet.Services;
using System.Text.Json.Serialization;
using MongoDB.Driver;
using SplitBackDotnet.Models;
using MongoDB.Bson.Serialization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
  options.SerializerOptions.AllowTrailingCommas = true;
  options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Prevent reference loop
});

// builder.Services.AddDbContext<DataContext>(options =>
//   options.UseSqlite(@"DataSource=mydatabase.db;")
//  );

builder.Services.Configure<AlphaSplitDatabaseSettings>(builder.Configuration.GetSection("AlphaSplitDatabase"));

// BsonSerializer.RegisterDiscriminatorConvention(typeof(IAction), new ActionDiscriminatorConvention());

// var client = new MongoClient("mongodb+srv://dbSplitUser:1423qrwe@cluster0.mqehg.mongodb.net/db1?retryWrites=true&w=majority");
// var db = client.GetDatabase("db1");
// var collection  = db.GetCollection<PersonModel>("people");
// var person = new PersonModel{FirstName="christos", LastName="kechagias"};

// await collection.InsertOneAsync(person);


builder.Services.AddScoped<IRepo, MongoRepo>();
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAllPolicy",
  builder =>
  {
    builder
      .WithOrigins(new[] { "http://localhost:3000" })
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials();
  });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMyAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddMySwagger();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapUserEndpoints();
app.MapExpenseEndpoints();
app.MapTransferEndpoints();
app.MapGroupEndpoints();
app.MapAuthenticationEndpoints();

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();
app.MapGet("/", (IOptions<AlphaSplitDatabaseSettings> appSettings) => 
{
  return appSettings;
});
app.Run();

record UserDto(string Username, string Password);
