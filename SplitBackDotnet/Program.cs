using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SplitBackDotnet.Data;
using SplitBackDotnet.Endpoints;
using SplitBackDotnet.Models;
using SplitBackDotnet.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options => {
  options.SerializerOptions.AllowTrailingCommas = true;
  options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Prevent reference loop
});

builder.Services.AddDbContext<DataContext>(options =>
  options.UseSqlite(@"DataSource=mydatabase.db;")
 );

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
app.UseHttpsRedirection();
app.MapGet("/", () => "Welcome to SplitBackMinimal Api").WithName("SplitBackMinimal Api");
app.MapUserEndpoints();
app.MapExpenseEndpoints();
app.MapAuthenticationEndpoints();

app.UseCors("AllowAllPolicy");

//User jajakis = new User() {
//  Nickname = "jajakis",
//  Email = "jajakis@email.com"
//};
//context.Add(jajakis);

var nanakis = new User()
{
  Nickname = "nanakis",
  Email = "nanakis@email.com"
};

//Group jajaGroup = new Group() {
//  Title = "jaja2 group",
//  Creator = context.Users.FirstOrDefault(user => user.Nickname == "jajakiss") is User user ? user : nanakis,
//};
//context.Add(jajaGroup);

//context.SaveChanges();
app.MapPost("/accounts/login", [AllowAnonymous] (UserDto user) => {
  if (user.Username == "thanoskat" && user.Password == "12345")
  {
    var secureKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

    var issuer = builder.Configuration["Jwt:Issuer"];
    var audience = builder.Configuration["Jwt:Audience"];
    var securityKey = new SymmetricSecurityKey(secureKey);
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

    var jwtTokenHandler = new JwtSecurityTokenHandler();

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[] {
        new Claim("Id", "1"),
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Email, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      }),
      Expires = DateTime.Now.AddMinutes(5),
      Audience = audience,
      Issuer = issuer,
      SigningCredentials = credentials
    };

    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
    var jwtToken = jwtTokenHandler.WriteToken(token);
    return Results.Ok(jwtToken);
  }
  return Results.Unauthorized();
});


app.Run();

record UserDto(string Username, string Password);
