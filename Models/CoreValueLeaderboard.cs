using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS4200_Team_Project.Models
{
    public class CoreValueLeaderboard
    {
        [Key]
        public int leaderboardID { get; set; }
        public int Stewardship { get; set; }
        public int Culture { get; set; }
        [Display(Name = "Delivery Excellence")]
        public int Delivery_Excellence { get; set; }
        public int Innovation { get; set; }
        [Display(Name = "Greater Good")]
        public int Greater_Good { get; set; }
        [Display(Name = "Integrity and Openness")]
        public int Integrity_And_Openness { get; set; }
        public int Balance { get; set; }

        public Guid ID { get; set; }
        public virtual UserDetails UserDetails { get; set; }

    }
}