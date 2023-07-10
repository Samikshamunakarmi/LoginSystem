using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PersonalInfoController : ApiController
    {
        //get/ api/controllername
        [HttpGet]
        public IHttpActionResult GetPerson()
        {
            List<MvcDatabaseConnectivity> PersonList = new List<MvcDatabaseConnectivity>();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Persons", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var person = new MvcDatabaseConnectivity();

                    person.PersonID = Convert.ToInt32(rdr["PersonID"]);
                    person.LastName = rdr["LastName"].ToString();
                    person.FirstName = rdr["FirstName"].ToString();
                    person.Address = rdr["Address"].ToString();
                    person.EmailAddress = rdr["EmailAddress"].ToString();
                    person.PhoneNumber = rdr["PhoneNumber"].ToString();
                    person.PostalCode = Convert.ToInt32(rdr["PostalCode"]);

                    PersonList.Add(person);
                }
            }
            return Ok(PersonList);
        }


        //post/ api/controllername
        [HttpPost]
        public IHttpActionResult PostPerson(MvcDatabaseConnectivity mvcDatabaseConnectivity)
        {
           
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Persons (LastName, FirstName, Address, EmailAddress, PhoneNumber, PostalCode) VALUES (@LastName, @FirstName, @Address, @EmailAddress, @PhoneNumber, @PostalCode)", con);
                cmd.Parameters.AddWithValue("@LastName", mvcDatabaseConnectivity.LastName);
                cmd.Parameters.AddWithValue("@FirstName", mvcDatabaseConnectivity.FirstName);
                cmd.Parameters.AddWithValue("@Address", mvcDatabaseConnectivity.Address);
                cmd.Parameters.AddWithValue("@EmailAddress", mvcDatabaseConnectivity.EmailAddress);
                cmd.Parameters.AddWithValue("@PhoneNumber", mvcDatabaseConnectivity.PhoneNumber);
                cmd.Parameters.AddWithValue("@PostalCode", mvcDatabaseConnectivity.PostalCode);


                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    return Ok("Person added successfully");
                }
                else
                {
                    return BadRequest("Failed to add person");
                }

            }

        }


        //put/ api/controllername
        [HttpPut]
        [Route("api/PersonalInfo/{id}")]
        public IHttpActionResult PutPerson(int id, MvcDatabaseConnectivity mvcDatabaseConnectivity)
        {

            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                string query = "UPDATE Persons SET LastName = @LastName, FirstName = @FirstName, Address = @Address, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber, PostalCode = @PostalCode where PersonID = @PersonID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", id);
                command.Parameters.AddWithValue("@LastName", mvcDatabaseConnectivity.LastName);
                command.Parameters.AddWithValue("@FirstName", mvcDatabaseConnectivity.FirstName);
                command.Parameters.AddWithValue("@Address", mvcDatabaseConnectivity.Address);
                command.Parameters.AddWithValue("@EmailAddress", mvcDatabaseConnectivity.EmailAddress);
                command.Parameters.AddWithValue("@PhoneNumber", mvcDatabaseConnectivity.PhoneNumber);
                command.Parameters.AddWithValue("@PostalCode", mvcDatabaseConnectivity.PostalCode);


                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                

                if (rowsAffected > 0)
                {
                    return Ok("Person updated successfully");
                }
                else
                {
                    return BadRequest("Failed to update person");
                }

            }

        }

        [HttpDelete]
        [Route("api/PersonalInfo/{id}")] //sets the route template for the Delete action method
        public IHttpActionResult Delete(int id)
        {

            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                string query = "Delete from Persons  WHERE PersonID = @PersonID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", id);


                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();


                if (rowsAffected > 0)
                {

                    return Ok("Person delete successfully");
                }
                else
                {
                    return BadRequest("Failed to delete person");
                }

            }

        }


        [HttpGet]
        [Route("api/PersonalInfo/{id}")]

        public IHttpActionResult DisplayPerson(int id)
        {
             string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))

            {
                string query = "SELECT * FROM Persons where personID= @PersonID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID",  id);
                connection.Open();
                 SqlDataReader rdr = command.ExecuteReader();    

                if (rdr.Read())
                {
                    var person = new MvcDatabaseConnectivity();

                    person.PersonID = Convert.ToInt32(rdr["PersonID"]);
                    person.LastName = rdr["LastName"].ToString();
                    person.FirstName = rdr["FirstName"].ToString();
                    person.Address = rdr["Address"].ToString();
                    person.EmailAddress = rdr["EmailAddress"].ToString();
                    person.PhoneNumber = rdr["PhoneNumber"].ToString();
                    person.PostalCode = Convert.ToInt32(rdr["PostalCode"]);

                    return Ok(person);
                }
                else { return BadRequest(); }
            }
           
        }
        

    }
}
