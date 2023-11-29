using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TimeTrackingApp.Models;

namespace TimeTrackingApp.Controllers
{
    public class WorksController : Controller
    {
        public IActionResult Index()
        {
            List<WorksIndexViewModel> viewModel = new List<WorksIndexViewModel>();

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("timetracking_app");
            var worksCollection = database.GetCollection<Work>("works");
            var employeesCollection = database.GetCollection<Employee>("employees");

            List<Work> works = worksCollection.Find(w => true).ToList();

            foreach (Work work in works)
            {
                ObjectId employeeId = new ObjectId(work.EmployeeId);
                Employee employee = employeesCollection.Find(e => e.Id == employeeId).FirstOrDefault();

                WorksIndexViewModel model = new WorksIndexViewModel();
                model.EmployeeName = employee.Name;
                model.WorkDescription = work.Description;
                model.WorkDate = work.WorkDate;
                viewModel.Add(model);
            }

            return View(viewModel);
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

        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("timetracking_app");
            var worksCollection = database.GetCollection<Work>("works");
            var employeesCollection = database.GetCollection<Employee>("employees");

            ObjectId workId = new ObjectId(id);
            Work work = worksCollection.Find(w => w.Id == workId).FirstOrDefault();

            if (work == null)
            {
                return NotFound();
            }

            ObjectId employeeId = new ObjectId(work.EmployeeId);
            Employee employee = employeesCollection.Find(e => e.Id == employeeId).FirstOrDefault();

            WorksIndexViewModel model = new WorksIndexViewModel
            {
                Id = work.Id,
                EmployeeName = employee.Name,
                WorkDescription = work.Description,
                WorkDate = work.WorkDate
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(WorksIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                MongoClient dbClient = new MongoClient();
                var database = dbClient.GetDatabase("timetracking_app");
                var worksCollection = database.GetCollection<Work>("works");

                ObjectId workId = model.Id;
                Work existingWork = worksCollection.Find(w => w.Id == workId).FirstOrDefault();

                if (existingWork == null)
                {
                    return NotFound();
                }

                existingWork.Description = model.WorkDescription;
                existingWork.WorkDate = model.WorkDate;

                worksCollection.ReplaceOne(w => w.Id == workId, existingWork);

                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
