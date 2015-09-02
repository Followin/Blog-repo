using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WEB.Models
{
    public class UserViewModel
    {
        public Int32 Id { get; set; }

        [Required]
        public String Nickname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public Genders Gender { get; set; }


    }

    public enum Genders
    {
        Male,
        Female
    }

    public class LoginViewModel
    {
        [Required]
        public String Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        
        public Boolean Persistent { get; set; }
    }
}