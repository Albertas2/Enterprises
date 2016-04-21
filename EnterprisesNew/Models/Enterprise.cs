using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterprisesNew.Models
{
    public class Enterprise
    {
        public Enterprise()
        {
            this.DateCreated = DateTime.Now;
        }

        public int ID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Enterprise name must be 50 characters or less")]
        [Display(Name = "Enterprise Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Enterprise description must be 200 characters or less")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Competence> Competences { get; set; }

    }
}