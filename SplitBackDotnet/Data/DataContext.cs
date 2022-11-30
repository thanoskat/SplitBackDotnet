using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Data;

public class DataContext : DbContext
{
  //public DataContext() {
  //}

  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {
  }

  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Group> Groups { get; set; } = null!;
  public DbSet<Expense> Expenses { get; set; } = null!;
  public DbSet<ExpenseSpender> ExpenseSpenders { get; set; } = null!;
  public DbSet<ExpenseParticipant> ExpenseParticipants { get; set; } = null!;
  public DbSet<Transfer> Transfers { get; set; } = null!;
  public DbSet<Label> Labels { get; set; } = null!;
  public DbSet<Session> Sessions { get; set; } = null!;
  public DbSet<PendingTransaction> PendingTransactions { get; set; } = null!;
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();
    modelBuilder.Entity<User>().HasMany(user => user.Groups).WithMany(group => group.Members);
    modelBuilder.Entity<Group>().HasOne(group => group.Creator).WithMany(user => user.CreatedGroups);

    //Start of ExpenseSpender
    //https://www.entityframeworktutorial.net/efcore/configure-many-to-many-relationship-in-ef-core.aspx
    modelBuilder.Entity<ExpenseSpender>().HasKey(eu => new { eu.ExpenseId, eu.SpenderId });
    modelBuilder.Entity<ExpenseSpender>()
    .HasOne<Expense>(eu => eu.Expense)
    .WithMany(e => e.ExpenseSpenders)
    .HasForeignKey(eu => eu.ExpenseId);

    modelBuilder.Entity<ExpenseSpender>()
    .HasOne<User>(eu => eu.Spender)
    .WithMany(e => e.ExpenseSpenders)
    .HasForeignKey(eu => eu.SpenderId);
    //End of ExpenseUser

    //Start of Share
    modelBuilder.Entity<ExpenseParticipant>().HasKey(s => new { s.ExpenseId, s.ParticipantId });
    modelBuilder.Entity<ExpenseParticipant>()
    .HasOne<Expense>(s => s.Expense)
    .WithMany(e => e.ExpenseParticipants)
    .HasForeignKey(s => s.ExpenseId);

    modelBuilder.Entity<ExpenseParticipant>()
    .HasOne<User>(s => s.Participant)
    .WithMany(p => p.ExpenseParticipants)
    .HasForeignKey(s => s.ParticipantId);
    //End of Share

    modelBuilder.Entity<Group>()
    .HasMany<PendingTransaction>(g => g.PendingTransactions)
    .WithOne(pt => pt.Group)
    .HasForeignKey(pt => pt.CurrentGroupId)
    .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Group>()
    .HasMany<Expense>(g => g.Expenses)
    .WithOne(e => e.Group)
    .HasForeignKey(e => e.GroupId);

  }
}
