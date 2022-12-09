
public interface ITransaction
{
  public int TransactionId { get; set; }

  public string Description { get; set; }
  
  public string IsoCode { get; set; }
 
  public DateTime CreatedAt {get;set;}
}