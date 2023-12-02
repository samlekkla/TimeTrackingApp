using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using TimeTrackingApp.Models;

namespace TimeTrackingApp.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Employee>("employees");

            List<Employee> employees = collection.Find(e => true).ToList(); 

            return View(employees);
        }
        
        public IActionResult Create() //Skapa listan
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee) //Hämta listan och spara i databasen
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Employee>("employees");
            collection.InsertOne(employee);

            return Redirect("/Employees");
        }

        public IActionResult Show(string Id) //Visa enskilda employee
        {
            ObjectId employeeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Employee>("employees");

            Employee employee = collection.Find(e => e.Id == employeeId).FirstOrDefault(); //Hämta enskilda book

            return View(employee);

        }

        public IActionResult Edit(string Id) 
        {
            ObjectId employeeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Employee>("employees");

            Employee employee = collection.Find(e => e.Id == employeeId).FirstOrDefault(); //Hämta enskilda book

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(string Id, Employee employee)
        {
            ObjectId employeeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Employee>("employees");

            employee.Id = employeeId; 
            collection.ReplaceOne(e => e.Id == employeeId, employee); //Ta emot 2 parametrar:

            return Redirect("/Employees");
        }

        [HttpPost]
        public IActionResult Delete(string Id)
        {
            ObjectId employeeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Employee>("employees");

            collection.DeleteOne(e => e.Id == employeeId);

            return Redirect("/Employees");

        }



    }
}
