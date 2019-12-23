using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaudisoftTask.Areas.UserRole.Models;
using System.ComponentModel.DataAnnotations;

namespace SaudisoftTask.Areas.UserRole.Models
{
    public class LogedinUser
    {
        public int ID { get; set; }
        public User user { get; set; }
        public int userId { get; set; }
        public string Day { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        [Display (Name ="Comment")]
        public string CheckInComment { get; set; }
        [Display(Name = "Comment")]
        public string CheckOutComment { get; set; }


    }
}