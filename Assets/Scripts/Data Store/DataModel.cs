using Realms;
using MongoDB.Bson;

public class DataModel : RealmObject
{
    [PrimaryKey]
    [MapTo("id")]
      public ObjectId Id { get; set; } = ObjectId.GenerateNewId(); 
    [MapTo("name")]
    [Required]
   public string Name { get; set; }
   [MapTo("description")]
    [Required]
   public string Description { get; set; }

   public DataModel(){

   }

   public DataModel (string Name, string Description){
    this.Id=ObjectId.GenerateNewId();
    this.Name = Name;
    this.Description = Description;
   }
}
