using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Login
    {

        public int loginID { get; set; }
        [Required(ErrorMessage = "The First name should not be blank.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = " The First name should be alphabet")]
        public string FirstName { get; set; }

       [Required(ErrorMessage = "The LastName should not be blank.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = " The Last name should  be alphabet")]
        public string LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        
        //[RegularExpression(@"^(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]+$", ErrorMessage = "The password must contain at least one special character.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]+$", ErrorMessage = "The password must contain at least one special character.")]
        public string Password { get; set; }

    }
}