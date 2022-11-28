using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Data;
using SplitBackDotnet.Endpoints;
using SplitBackDotnet.Services;
using SplitBackDotnet.Profiles;
using System.Text.Json.Serialization;
using SplitBackDotnet.Services.EmailService;
using SplitBackDotnet.Models;
using SplitBackDotnet.Services.JwtFactory;
using SplitBackDotnet.Services.CommandRepo;
using System;

var builder = WebApplication.CreateBuilder(args);

 builder.Services.Configure<JsonOptions>(options => {
   options.SerializerOptions.AllowTrailingCommas = true;
   options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Prevent reference loop
 });


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddTransient<IJwtFactory, JwtFactory>();

builder.Services.AddDbContext<DataContext>(options =>
  options.UseSqlite(@"DataSource=mydatabase.db;")
 );

builder.Services.AddScoped<ICommandRepo, CommandRepo>();

builder.Services.AddCors(options => {
  options.AddPolicy("AllowAllPolicy",
  builder => {
    builder
      .WithOrigins(new[] { "http://localhost:3000" })
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials();
  });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<UserProfile>());
builder.Services.AddMyAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddMySwagger();
var app = builder.Build();

if(app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapGet("/", () => "Welcome to SplitBackMinimal Api").WithName("SplitBackMinimal Api");
app.MapUserEndpoints();
app.MapExpenseEndpoints();
app.MapAuthenticationEndpoints();

app.UseCors("AllowAllPolicy");

app.Run();

public partial class Program { }
