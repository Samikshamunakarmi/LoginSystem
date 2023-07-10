using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetLoginDataFromDB (Login login)
        {
        
            String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection com= new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select password from Login where email= @Email ",com);
                cmd.Parameters.AddWithValue("@email", login.Email);
                
                com.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string dbHashedPassword = reader.GetString(0);
                    if (VerifyPassword(login.Password, dbHashedPassword))
                    {
                        return Ok("Success");
                    }
                }
                
                    return BadRequest("Need to Join In for New Employer");
                
            }

        }
        

        [HttpPost]
        [Route("api/Login/PostJoininInfo")]
        public IHttpActionResult PostJoininInfo(Login login)
        {
            String CS= ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                 if (login.Email == null && login.Password == null)
                {
                    return BadRequest("Email and password are required.");
                }

                SqlCommand checkEmailExsits = new SqlCommand("Select Count(*) from Login where email= @email", con);
                checkEmailExsits.Parameters.AddWithValue("@email", login.Email);
                con.Open();
                int value = (int)checkEmailExsits.ExecuteScalar();
                con.Close();
                if(value>0)
                {
                    return BadRequest("Email already exists"); 
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into Login(FirstName,LastName,Email,Password) VALUES (@FirstName, @LastName,@Email, @Password)", con);
                    cmd.Parameters.AddWithValue("@FirstName", login.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", login.LastName);
                    cmd.Parameters.AddWithValue("@Email", login.Email);
                    cmd.Parameters.AddWithValue("@Password", login.Password);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        return Ok("You Are Now Join");
                    }
                    else
                    {
                        return BadRequest("Your Joining is failed.");
                    }
                }
               
            }
        }


        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            using (SHA256 sHA256Hash = SHA256.Create())
            {
                byte[] bytes = sHA256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                string Hashedinput = Convert.ToBase64String(bytes);
                return Hashedinput == hashedPassword;
            }

        }
    }
}
