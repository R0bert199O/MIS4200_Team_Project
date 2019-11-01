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
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use only letters A - Z")]
        public string firstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use only letters A - Z")]
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
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{3})-([0-9]{3})-([0-9]{4})$",
                   ErrorMessage = "Please enter phone number in format xxx-xxx-xxxx.")]
        //[DisplayFormat(DataFormatString = "\\d{3}-\\d{3}-\\d{4}", ApplyFormatInEditMode = true)]

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
            Consultant = 0,
            [Display(Name = "Senior Consultant")]
            Senior_Consultant = 1,
            Manager = 2,
            Architect = 3,
            [Display(Name = "Senior Manager")]
            Senior_Manager = 4,
            [Display(Name = "Senior Architect")]
            Senior_Architect = 5,
            [Display(Name = "Principal Architect")]
            Principal_Architect = 6,
            Partner = 7,
        }
        public enum operatingGroup
        {
            [Display(Name = "Modern Software Delivery")]
            Modern_Software_Delivery = 0,
            [Display(Name = "Data & Analytics")]
            Data_and_Analytics = 1,
            [Display(Name = "Mobile App Development")]
            Mobile_App_Development = 2,
            [Display(Name = "Enterprise Collaboration")]
            Enterprise_Collaboration = 3,
            [Display(Name = "Technology Solution Services")]
            Technology_Solution_Services = 4,
            [Display(Name = "Enterprise Applications & Solutions")]
            Enterprise_Applications_and_Solutions = 5,
            [Display(Name = "Marketing Operations & CRM")]
            Marketing_Operations_and_CRM = 6,
            [Display(Name = "Talent Management")]
            Talent_Management = 7,
            Digital = 8,
            [Display(Name = "Customer Experience & Design")]
            Customer_Experience_and_Design = 9,
            [Display(Name = "Microsoft Partnership")]
            Microsoft_Partnership = 10,
            [Display(Name = "People & Change")]
            People_and_Change = 11,
            [Display(Name = "Operational & Process Excellence")]
            Operational_and_Process_Excellence = 12,
            Insurance = 13,
            Healthcare = 14,
            [Display(Name = "Energy & Utilities")]
            Energy_and_Utilities = 15,
            [Display(Name = "Financial Services")]
            Financial_Services = 16,
        }
        public enum location
        {
            Columbus = 0,
            Seattle = 1,
            India = 2,
            Cincinnati = 3,
            Cleveland = 4,
            [Display(Name = "St. Louis")]
            St_Louis = 5,
            Indianapolis = 6,
            Chicago = 7,
            Charlotte = 8,
            Boston = 9,
            Louisville = 10,
            Miami = 11,
            Tampa = 12,
        }

    }
}