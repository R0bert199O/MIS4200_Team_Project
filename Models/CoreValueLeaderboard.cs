using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MIS4200_Team_Project.Models
{
    public class CoreValueLeaderboard
    {
        [Key] // the data annotation is necessary because there is a field called, ID,
              // in the table and it is not the PK for the record
        public int leaderboardID { get; set; }

        //ID of person being recognized
        public Guid ID { get; set; }
        [ForeignKey(name: "ID")]
        public virtual UserDetails UserDetails { get; set; }
        [Range(0, 50, ErrorMessage = "Enter number between 0 and 50")]
        public int? Stewardship { get; set; }
        [Range(0, 50, ErrorMessage = "Enter number between 0 and 50")]
        public int? Culture { get; set; }
        [Range(0, 50, ErrorMessage = "Enter number between 0 and 50")]
        [Display(Name = "Delivery Excellence")]
        public int? Delivery_Excellence { get; set; }
        [Range(0, 50, ErrorMessage = "Enter number between 0 and 50")]
        public int? Innovation { get; set; }
        [Range(0, 50, ErrorMessage = "Enter number between 0 and 50")]
        [Display(Name = "Greater Good")]
        public int? Greater_Good { get; set; }
        [Range(0, 50, ErrorMessage = "Enter number between 0 and 50")]
        [Display(Name = "Integrity and Openness")]
        public int? Integrity_And_Openness { get; set; }
        [Range(0, 50, ErrorMessage = "Enter number between 0 and 50")]
        public int? Balance { get; set; }
        [Range(0, 50, ErrorMessage = "Enter number between 0 and 50")]
        [Display(Name = "Total Points")]
        public int? TotalPoints
        {
            get
            {
                return Stewardship + Culture + Delivery_Excellence + Innovation + Greater_Good + Integrity_And_Openness + Balance;
            }
        }

    }
}