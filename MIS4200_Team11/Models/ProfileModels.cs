using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Summary description for Class1
/// </summary>
public class ProfileModels
{

    public ProfileModels()
    {

    }
      

        [Display(Name = "Employee ID")]
        [Required]
        public Guid ID { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string lastName { get; set; }
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string email { get; set; }
        [Display(Name = "Business Unit")]
        [Required]
        public string busUnit { get; set; }
        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime hireDate { get; set; }
        [Display(Name = "Title")]
        [Required]
        public string title { get; set; }
        public string fullName
        {
            get
            {
            return lastName + ", " + firstName;
            } 
        }
   
}


