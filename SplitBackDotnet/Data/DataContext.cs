using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Data;

public class DataContext : DbContext {
  //public DataContext() {
  //}

  public DataContext(DbContextOptions<DataContext> options) : base(options) {
  }

  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Group> Groups { get; set; } = null!;
  public DbSet<Expense> Expenses { get; set; } = null!;
  public DbSet<Share> Shares { get; set; } = null!;
  public DbSet<Transfer> Transfers { get; set; } = null!;
  public DbSet<Label> Labels { get; set; } = null!;
  public DbSet<Session> Sessions { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();
    modelBuilder.Entity<User>().HasMany(user => user.Groups).WithMany(group => group.Members);
    modelBuilder.Entity<Group>().HasOne(group => group.Creator).WithMany(user => user.CreatedGroups);

    modelBuilder.Entity<Expense>().HasMany(exp => exp.Spenders).WithMany(spender => spender.Expenses)
    .UsingEntity<ExpenseUser>(
     j => j.HasOne(eu => eu.Spender).WithMany(spender => spender.ExpenseUsers).HasForeignKey(eu => eu.SpenderId),
     j => j.HasOne(eu => eu.Expense).WithMany(expense => expense.ExpenseUsers).HasForeignKey(eu => eu.ExpenseId),
     j => {
       j.Property(eu => eu.SpenderAmount).HasDefaultValueSql(null);
       j.HasKey(t => new { t.ExpenseId, t.SpenderId });
     }
    );

  }

  //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
  //  optionsBuilder.UseSqlite(@"DataSource=mydatabase.db;");
  //}

  //public DbSet<User> Users => Set<User>();
}
