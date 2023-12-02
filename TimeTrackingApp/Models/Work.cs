using MongoDB.Bson;

namespace TimeTrackingApp.Models
{
    public class Work
    {
        public ObjectId Id { get; set; }
        public string WorkDescription { get; set; }
        public string WorkDate {  get; set; }
    }
}
