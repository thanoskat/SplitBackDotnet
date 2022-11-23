namespace SplitBackDotnet.Data;
  public interface ISpender
  {
    public int Id { get; set; }
    public decimal Balance { get; set; }
  }