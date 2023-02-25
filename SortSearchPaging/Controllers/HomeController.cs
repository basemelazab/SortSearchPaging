using PagedList;
using PagedList.Mvc;
using SortSearchPaging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SortSearchPaging.Controllers
{
    public class HomeController : Controller
    {
        //create a StudentsDbContext object  
        StudentsDbContext db = new StudentsDbContext();
        //public ActionResult Index()
        //{
        //    //the StudentsDbContext object has got a property Students which will give us all the student records back. Convert it into a list  

        //    List<Student> StudentList = db.Students.ToList();
        //    //pass the StudentList list object to the view.  
        //    return View(StudentList);
        //}
        //the first parameter is the option that we choose and the second parameter will use the textbox value  


        //public ActionResult Index(string option, string search)
        //{

        //    //if a user choose the radio button option as Subject  
        //    if (option == "Subjects")
        //    {
        //        //Index action method will return a view with a student records based on what a user specify the value in textbox  
        //        return View(db.Students.Where(x => x.Subjects == search || search == null).ToList());
        //    }
        //    else if (option == "Gender")
        //    {
        //        return View(db.Students.Where(x => x.Gender == search || search == null).ToList());
        //    }
        //    else
        //    {
        //        return View(db.Students.Where(x => x.Name.StartsWith(search) || search == null).ToList());
        //    }
        //}

        //public ActionResult Index(string option, string search, int? pageNumber)
        //{
        //    if (option == "Subjects")
        //    {
        //        return View(db.Students.Where(x => x.Subjects == search || search == null).ToList().ToPagedList(pageNumber ?? 1, 3));
        //    }
        //    else if (option == "Gender")
        //    {
        //        return View(db.Students.Where(x => x.Gender == search || search == null).ToList().ToPagedList(pageNumber ?? 1, 3));
        //    }
        //    else
        //    {
        //        return View(db.Students.Where(x => x.Name.StartsWith(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 3));
        //    }
        //}

        public ActionResult Index(string option, string search, int? pageNumber, string sort)
        {

            //if the sort parameter is null or empty then we are initializing the value as descending name  
            ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "descending name" : "";
            //if the sort value is gender then we are initializing the value as descending gender  
            ViewBag.SortByGender = sort == "Gender" ? "descending gender" : "Gender";

            //here we are converting the db.Students to AsQueryable so that we can invoke all the extension methods on variable records.  
            var records = db.Students.AsQueryable();

            if (option == "Subjects")
            {
                records = records.Where(x => x.Subjects == search || search == null);
            }
            else if (option == "Gender")
            {
                records = records.Where(x => x.Gender == search || search == null);
            }
            else
            {
                records = records.Where(x => x.Name.StartsWith(search) || search == null);
            }

            switch (sort)
            {

                case "descending name":
                    records = records.OrderByDescending(x => x.Name);
                    break;

                case "descending gender":
                    records = records.OrderByDescending(x => x.Gender);
                    break;

                case "Gender":
                    records = records.OrderBy(x => x.Gender);
                    break;

                default:
                    records = records.OrderBy(x => x.Name);
                    break;

            }
            return View(records.ToPagedList(pageNumber ?? 1, 3));
        }
    }
}
