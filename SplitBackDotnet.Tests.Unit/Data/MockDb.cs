using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Data;

namespace UnitTests.Helpers;

public class MockDb : IDbContextFactory<DataContext>
{
  public DataContext CreateDbContext()
  {
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"InMemoryMockDb-{Guid.NewGuid()}")
        .Options;

    return new DataContext(options);
  }
}