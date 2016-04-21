using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterprisesNew.Models
{
    public class CompetenceRating
    {
        public CompetenceRating()
        {
            this.DateCreated = DateTime.Now;
        }

        public int ID { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public int? EmployeeID { get; set; }

        [Display(Name = "Competence")]
        public int? CompetenceID { get; set; }

        [Range(0,10)]
        public int? Grade { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Competence Competence { get; set; }
    }
}