using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LoginMVCController : Controller
    {
        public ActionResult Login()
        {
            string email = Session["email"] as string;
            if(email == null)
            {
                return View();
            }

            return RedirectToAction("Index", "MVCCrud");
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/Login");
            if (login.Email == null)
            {
                  
                return View("Login");
            }

            HttpResponseMessage response = client.PostAsJsonAsync("Login/GetLoginDataFromDB", login).Result;

            if (response.IsSuccessStatusCode)
            {

                Session["Email"] = login.Email;

                return RedirectToAction("Index", "MVCCrud");
                
                
            }
            else
            {
                ViewBag.Message = "New employee needs to join first or Password may be mistaken.";
                return View("Login");
            }
            

        }
        public ActionResult JoinIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JoinIn(Login login)
        {
            string hashPassword =  HashPassword(login.Password);
            login.Password= hashPassword;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44381/api/Login");

            string PostURL = Url.Action("PostloginInfo", "Login", null, Request.Url.Scheme);
            HttpResponseMessage insertLoginData = client.PostAsJsonAsync("Login/PostJoininInfo", login).Result;

            if (insertLoginData.IsSuccessStatusCode)
            {
                ViewBag.Message = "Log In Now";
                return RedirectToAction("Login");
            }
            else
            {
                if (insertLoginData.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ViewBag.ErrorMessage = "Failed to join: Email already exists.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to join: An error occurred.";
                }
            }

            return View("JoinIn");

        }

        public ActionResult LogOUT()
        {
            Session.Clear();
            return View("Login");
            

        }
        public string HashPassword(string password)
        {
            using (SHA256 sHA256Hash = SHA256.Create())
            {
                byte[] bytes = sHA256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }

        }
       
    }
}