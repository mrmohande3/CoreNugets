using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace HashSharpCore.DataLayer.Models
{
    public enum Gender
    {
        Mail,
        Femail
    }
    public class User : IdentityUser<int>
    {
        public Gender Gender { get; set; }
        [NotMapped]
        public string Password { get; set; }
    }
}
