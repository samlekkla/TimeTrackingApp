using MongoDB.Bson;

namespace TimeTrackingApp.Models
{
    public class WorksIndexViewModel
    {
        public ObjectId Id { get; set; }
        public string EmployeeName { get; set; }    
        public string WorkDescription { get; set; }
        public string WorkDate {  get; set; }
    }
}
