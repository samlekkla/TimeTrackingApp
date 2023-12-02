using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using TimeTrackingApp.Models;

namespace TimeTrackingApp.Controllers
{
    public class WorksController : Controller
    {
        public IActionResult Index()
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Work>("works");

            List<Work> works = collection.Find(w => true).ToList();

            return View(works);
        }

        public IActionResult Create() //Skapa listan
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Employee>("employees");

            List<Employee> employees = collection.Find(e => true).ToList();
            return View(employees);
        }

        [HttpPost]
        public IActionResult Create(Work Work) //Hämta listan och spara i databasen
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Work>("works");
            collection.InsertOne(Work);

            return Redirect("/Works");
        }

        public IActionResult Show(string Id) //Visa enskilda employee
        {
            ObjectId workId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Work>("works");

            Work work = collection.Find(w => w.Id == workId).FirstOrDefault(); //Hämta enskilda book

            return View(work);

        }

        public IActionResult Edit(string Id)
        {
            ObjectId workId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Work>("works");

            Work work = collection.Find(w => w.Id == workId).FirstOrDefault(); //Hämta enskilda book

            return View(work);
        }

        [HttpPost]
        public IActionResult Edit(string Id, Work work)
        {
            ObjectId workId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Work>("works");

            work.Id = workId;
            collection.ReplaceOne(w => w.Id == workId, work); //Ta emot 2 parametrar:

            return Redirect("/Works");
        }

        [HttpPost]
        public IActionResult Delete(string Id)
        {
            ObjectId workId = new ObjectId();
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var collection = database.GetCollection<Work>("works");

            collection.DeleteOne(w => w.Id == workId);

            return Redirect("/Works");

        }

    }
}
