using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SplitBackDotnet.Data;

namespace SplitBackDotnet.Tests.Integration.Mocks;

public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class {

  protected override IHost CreateHost(IHostBuilder builder) {
    builder.ConfigureServices(services => {
      var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));

      if(descriptor != null) {
        services.Remove(descriptor);
      }

      services.AddDbContext<DataContext>(options => {
        options.UseInMemoryDatabase($"InMemoryMockDb");
      });
      //services.AddDbContext<DataContext>(options => {
      //  var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      //  options.UseSqlite($"Data Source={Path.Join(path, "SplitBackDotnet_Tests_Integration.db")}");
      //});
    });

    return base.CreateHost(builder);
  }
}