using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace HashSharpCore.DataLayer.Models
{
    public class Role : IdentityRole<int>
    {
        [Required]
        public string Description { get; set; }
    }
}
