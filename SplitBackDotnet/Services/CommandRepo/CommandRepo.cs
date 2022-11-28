using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Data;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Services.CommandRepo;

public class CommandRepo : ICommandRepo {
  private readonly DbContext _context;

  public CommandRepo(DataContext context) {
    _context = context;
  }
  public async Task SaveChanges() {
    await _context.SaveChangesAsync();
  }
}
