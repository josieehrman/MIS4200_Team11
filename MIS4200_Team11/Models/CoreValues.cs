using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIS4200_Team11.Models
{
    public class CoreValues
    {
        [Key]
        public int cvID { get; set; }
        [Display(Name = "Core value recognized")]
        public CoreValue award { get; set; }
        [Display(Name = "ID of Person giving the recognition")]
        public Guid recognizor { get; set; }
        [Display(Name = "ID of Person receiving the recognition")]
        public Guid recognized { get; set; }
        [Display(Name = "Date recognition given")]
        public DateTime recognizationDate { get; set; }
        public enum CoreValue
        {
            Excellence = 1,
            Integrity = 2,
            Stewardship = 3,
            Innovate = 4,
            Balance = 5,
            Culture = 6,
            Passion = 7

        }
        public string descriptionOfRecognition { get; set; }
        [ForeignKey("recognizor")]
        public ProfileModels personGivingRecognition { get; set; }
        [ForeignKey("recognized")]
        public ProfileModels personGettingRecognition { get; set; }
    }
}