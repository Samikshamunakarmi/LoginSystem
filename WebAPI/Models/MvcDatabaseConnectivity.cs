using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class MvcDatabaseConnectivity
    {
        public int PersonID { get; set; }
        [Required(ErrorMessage = "The LastName should not be blank.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = " The LastName should  be alphabet")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The FirstName  should not be blank.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage ="The FirstName should be alphabet.")]
        public string FirstName { get; set; }
   
        public string Address { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format.")]
        public string EmailAddress { get; set; }

        [RegularExpression (@"^\d{10}$", ErrorMessage ="The value must be digits and length should be 10.")]
        public string PhoneNumber { get; set; }
        public int PostalCode { get; set; }

    }
}