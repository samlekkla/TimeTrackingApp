using MongoDB.Bson;

namespace TimeTrackingApp.Models
{
    public class Work
    {
        public ObjectId Id { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public string WorkDate {  get; set; }
    }
}
