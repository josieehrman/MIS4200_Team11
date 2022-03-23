using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS4200_Team11.Models
{
    public class Recognize
    {
        [Key]
        public int recognizeID { get; set; }
        public int cvID { get; set; }
        public int ID { get; set; }
        public string Reason { get; set; }
        public virtual CoreValues coreValue { get; set; }
        public virtual ProfileModels profile { get; set; }
    }
}