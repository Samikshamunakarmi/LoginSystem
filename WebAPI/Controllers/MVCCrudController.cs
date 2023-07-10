using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace WebAPI.Controllers
{
    public class MVCCrudController : Controller
    {
        // GET: MVCCrud
        public ActionResult Index()
        {
            IEnumerable<MvcDatabaseConnectivity> persons = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/PersonalInfo");

            // Send a GET request to retrieve the list of persons from the Web API
            HttpResponseMessage response = client.GetAsync("PersonalInfo").Result;

            if (response.IsSuccessStatusCode)
            {
                // Read the response content and parse it into a collection of MvcDatabaseConnectivity objects
                persons = response.Content.ReadAsAsync<IEnumerable<MvcDatabaseConnectivity>>().Result;
            }
            else
            {
                // Handle the error response accordingly
                // For example, you can log the error or display an error message to the user
                // You can also choose to return a different view in case of an error
                ModelState.AddModelError(string.Empty, "Failed to retrieve persons. Please try again later.");
            }


            return View(persons);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MvcDatabaseConnectivity mvcDatabase)
        {
           

         

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/PersonalInfo");

            HttpResponseMessage insertRecord = client.PostAsJsonAsync("PersonalInfo", mvcDatabase).Result;
            if(insertRecord.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        public ActionResult Details(int id)
        {
            MvcDatabaseConnectivity person = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/");

            // Send a GET request to retrieve the list of persons from the Web API
            HttpResponseMessage response = client.GetAsync("PersonalInfo/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                // Read the response content and parse it into a collection of MvcDatabaseConnectivity objects
                person = response.Content.ReadAsAsync<MvcDatabaseConnectivity>().Result;
            }
            else
            {
                // Handle the error response accordingly
                // For example, you can log the error or display an error message to the user
                // You can also choose to return a different view in case of an error
                ModelState.AddModelError(string.Empty, "Failed to retrieve persons. Please try again later.");
            }


            return View(person);
        }

        public ActionResult Edit(int id)
        {
            MvcDatabaseConnectivity person = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/");

            // Send a GET request to retrieve the list of persons from the Web API
            HttpResponseMessage response = client.GetAsync("PersonalInfo/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                // Read the response content and parse it into a collection of MvcDatabaseConnectivity objects
                person = response.Content.ReadAsAsync<MvcDatabaseConnectivity>().Result;
            }
            else
            {
                // Handle the error response accordingly
                // For example, you can log the error or display an error message to the user
                // You can also choose to return a different view in case of an error
                ModelState.AddModelError(string.Empty, "Failed to retrieve persons. Please try again later.");
            }


            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(int id, MvcDatabaseConnectivity mvcDatabase)
        {
          
            HttpClient client = new HttpClient();
            

            // Send a GET request to retrieve the list of persons from the Web API
            client.BaseAddress = new Uri("https://localhost:44381/api/");

            HttpResponseMessage insertRecord = client.PutAsJsonAsync($"PersonalInfo/{id}", mvcDatabase).Result;
            if (insertRecord.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Employee record not updated.";
            }

            return View(mvcDatabase);
        }

      
        public ActionResult Delete(int id)
        {
           
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/");

            // Send a GET request to retrieve the list of persons from the Web API
            HttpResponseMessage response = client.DeleteAsync("PersonalInfo/" + id.ToString()).Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content and parse it into a collection of MvcDatabaseConnectivity objects
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorMessage = response.Content.ReadAsStringAsync().Result;
                    
                    ViewBag.ErrorMessage = "Failed to delete the person. Error Message: " + errorMessage;

                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
            }

          
          


            return RedirectToAction("Index");
        }

        

        
    }
}