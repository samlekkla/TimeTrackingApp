using MongoDB.Bson;

namespace TimeTrackingApp.Models
{
    public class Employee
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Birthyear {  get; set; }  
    }
}
