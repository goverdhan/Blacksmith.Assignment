using Blacksmith.Assignment.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blacksmith.Assignment.Models
{
    public class User
    {
        [Required]
        [CustomValidator("First Name", 5, 10, ValidationType.Length, ValidationType.Alphabetic)]
        public string  FirstName { get; set; }

        [Required]
        [CustomValidator("Last Name", 5, 10, ValidationType.Length, ValidationType.Alphabetic)]
        public string LastName { get; set; }

        [Required]
        [CustomValidator("Address", 10, 100, ValidationType.Length, ValidationType.AlphaNumeric)]
        public string Address { get; set; }

        [Required]
        [CustomValidator("Age", ValidationType.Number)]
        public string Age { get; set; }

       
        [CustomValidator("Parent Name", 5, 10, "Age",18, ValidationType.Length, ValidationType.Alphabetic)]
        public string  ParentName { get; set; }

        [Required]
        [CustomValidator("Email", ValidationType.Email)]
        public string Email { get; set; }

        [Required]
        [CustomValidator("Webiste", ValidationType.Webiste)]
        public string Website { get; set; }

    }
}
