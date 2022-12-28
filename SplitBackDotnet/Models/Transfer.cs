﻿namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;


public class Transfer
{
  public ObjectId Id { get; set; }
  [MaxLength(80)]
  public string? Description { get; set; }
  public decimal Amount { get; set; }
  public string IsoCode { get; set; } = null!;
  public ObjectId SenderId { get; set; }
  public ObjectId ReceiverId { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public ICollection<TransferPastSnapShot>? History { get; set; } = new List<TransferPastSnapShot>();
  public bool IsDeleted { get; set; } = false;
}