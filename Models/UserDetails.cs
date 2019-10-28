using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS4200_Team_Project.Models
{
    public class UserDetails
    {
        [Required]
        public Guid ID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime birthDate { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Centric Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Primary Phone")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }
        [Required]
        [Display(Name = "Job Title")]
        public jobTitle JobTitle { get; set; }
        [Required]
        [Display(Name = "Operating Group")]
        public operatingGroup operatingGroups { get; set; }
        [Required]
        [Display(Name = "Location")]
        public location locations { get; set; }

        public enum jobTitle
        {
            Consultant = 1,
            [Display(Name = "Senior Consultant")]
            Senior_Consultant = 2,
            Manager = 3,
            Architect = 4,
            [Display(Name = "Senior Manager")]
            Senior_Manager = 5,
            [Display(Name = "Senior Architect")]
            Senior_Architect = 6,
            [Display(Name = "Principal Architect")]
            Principal_Architect = 7,
            Partner = 8,
        }
        public enum operatingGroup
        {
            [Display(Name = "Modern Software Delivery")]
            Modern_Software_Delivery = 1,
            [Display(Name = "Data & Analytics")]
            Data_and_Analytics = 2,
            [Display(Name = "Mobile App Development")]
            Mobile_App_Development = 3,
            [Display(Name = "Enterprise Collaboration")]
            Enterprise_Collaboration = 4,
            [Display(Name = "Technology Solution Services")]
            Technology_Solution_Services = 5,
            [Display(Name = "Enterprise Applications & Solutions")]
            Enterprise_Applications_and_Solutions = 6,
            [Display(Name = "Marketing Operations & CRM")]
            Marketing_Operations_and_CRM = 7,
            [Display(Name = "Talent Management")]
            Talent_Management = 8,
            Digital = 9,
            [Display(Name = "Customer Experience & Design")]
            Customer_Experience_and_Design = 10,
            [Display(Name = "Microsoft Partnership")]
            Microsoft_Partnership = 11,
            [Display(Name = "People & Change")]
            People_and_Change = 12,
            [Display(Name = "Operational & Process Excellence")]
            Operational_and_Process_Excellence = 13,
            Insurance = 14,
            Healthcare = 15,
            [Display(Name = "Energy & Utilities")]
            Energy_and_Utilities = 16,
            [Display(Name = "Financial Services")]
            Financial_Services = 17,
        }
        public enum location
        {
            Columbus = 1,
            Seattle = 2,
            India = 3,
            Cincinnati = 4,
            Cleveland = 5,
            [Display(Name = "St. Louis")]
            St_Louis = 6,
            Indianapolis = 7,
            Chicago = 8,
            Charlotte = 9,
            Boston = 10,
            Louisville = 11,
            Miami = 12,
            Tampa = 13,
        }

    }
}