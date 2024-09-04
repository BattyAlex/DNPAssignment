namespace Entities;

public class User
{
   public int ID{get; set;} 
   public string Name{get; set;}
   public string Password{get; set;}

   public User(int id, string name, string password)
   {
      ID = id;
      Name = name;
      Password = password;
   }
}