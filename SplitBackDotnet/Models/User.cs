﻿using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{

  [BsonRepresentation(BsonType.ObjectId)]
  public ObjectId Id { get; set; }

  [MaxLength(200)]
  [Required]
  public string Nickname { get; set; } = String.Empty;

  [MaxLength(200)]
  [Required]
  public string Email { get; set; } = String.Empty;

  public ObjectId[] Groups { get; set; } = Array.Empty<ObjectId>();

  public DateTime Date { get; set; } = DateTime.UtcNow;

}
