using Linq_CRUD_MVC.Models;
using Linq_CRUD_MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linq_CRUD_MVC.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _repository;

        //Initialize object of database Connection using controller Constructor
        public HomeController() : this(new UserRepository())
        {
        }

        //this will call everytime, Initialize all the method of Interface and store in the object for using in the Action method
        public HomeController(IUserRepository repository)
        {
            _repository = repository;
        }

        //List: Display All the Records
        public ActionResult Index()
        {
            var users = _repository.GetUsers();
            return View(users);
        }

        //Details: Get Detail of perticular record
        public ActionResult Details(int id)
        {
            UserModel model = _repository.GetUserById(id);
            return View(model);
        }

        //Insert: Insert the Record
        [HttpGet]
        public ActionResult Create()
        {
            return View(new UserModel());
        }
        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.InsertUser(user);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(user);
        }

        //Edit: Edit the Record
        public ActionResult Edit(int id)
        {
            UserModel model = _repository.GetUserById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateUser(user);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(user);
        }

        //Delete: Delete the Record
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            UserModel user = _repository.GetUserById(id);
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                UserModel user = _repository.GetUserById(id);
                _repository.DeleteUser(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                new System.Web.Routing.RouteValueDictionary { { "id", id }, { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}