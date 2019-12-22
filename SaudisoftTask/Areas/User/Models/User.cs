using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaudisoftTask.Areas.UserRole.Models;
using System.ComponentModel.DataAnnotations;

namespace SaudisoftTask.Areas.UserRole.Models
{
    public class User
    {
        public int ID { get; set; }
        public UserRole Role { get; set; }
        public int RoleId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string CheckInTime { get; set; }
        [Required]
        public string CheckOutTime { get; set; }


    }
}