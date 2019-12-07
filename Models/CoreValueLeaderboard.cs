using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? Stewardship { get; set; }
        public int? Culture { get; set; }
        [Display(Name = "Delivery Excellence")]
        public int? Delivery_Excellence { get; set; }
        public int? Innovation { get; set; }
        [Display(Name = "Greater Good")]
        public int? Greater_Good { get; set; }
        [Display(Name = "Integrity and Openness")]
        public int? Integrity_And_Openness { get; set; }
        public int? Balance { get; set; }
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