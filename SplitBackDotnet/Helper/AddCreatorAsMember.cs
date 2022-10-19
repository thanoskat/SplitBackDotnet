using SplitBackDotnet.Models;

namespace SplitBackDotnet.Helper {
  public static class AddCreatorAsMember {
     public static void AddCreatorToMembers(User Creator, Group Group){
      ICollection<User> collection = new List<User>();
      collection.Add(Creator);
      Group.Members=collection;
      Group.Creator=Creator;
     }
    }
  }
