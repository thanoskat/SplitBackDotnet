using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Data;
using SplitBackDotnet.Endpoints;
using SplitBackDotnet.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Prevent reference loop
});

builder.Services.AddDbContext<DataContext>(options =>
  options.UseSqlite(@"DataSource=mydatabase.db;")
 );

// builder.Services.AddSingleton<IDocumentStore>(new DocumentStore {
//     Urls = new[] { "http://localhost:8080" },
//     Database = "splita"
// });

// builder.Services.AddScoped<IDocumentSession>(provider => {
//     var store = provider.GetService<IDocumentStore>();
//     store.Initialize();
//     return store.OpenSession();
// });

builder.Services.AddScoped<IRepo, Repo>();
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
app.MapTransferEndpoints();
app.MapGroupEndpoints();
app.MapAuthenticationEndpoints();

app.UseCors("AllowAllPolicy");

app.Run();

record UserDto(string Username, string Password);
