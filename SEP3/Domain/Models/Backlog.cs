namespace Domain.Models;

public class Backlog
{
   public string? name { get; set; }
  
   public User? ProductOwner { get; private set; }
   public bool IsCompleted { get; set; }

   public Backlog(string? name, User? productOwner)
   {
      this.name = name;
      ProductOwner = productOwner;
   }
}