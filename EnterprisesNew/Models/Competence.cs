using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterprisesNew.Models
{
    public class Competence
    {   
        public int ID { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Competence name must be 25 characters or less")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Competence name must be 200 characters or less")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Enterprise")]
        public int? EnterpriseID { get; set; }

        public virtual ICollection<CompetenceRating> CompotencesRatings { get; set; }
        public virtual Enterprise Enterprise { get; set; }
    }
}