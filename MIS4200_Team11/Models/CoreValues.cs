using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS4200_Team11.Models
{
    public class CoreValues
    {
        [Key]
        public int cvID { get; set; }
        [Display(Name = "Core Value")]
        public string coreValue  { get; set; }
    }
}