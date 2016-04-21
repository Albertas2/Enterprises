using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterprisesNew.Models
{
    public class Employee
    {

        public Employee()
        {
            this.RegistrationDate = DateTime.Now;
        }

        public int ID { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Employee name must be 25 characters or less")]
        [Display(Name ="Employee Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Employee surname must be 25 characters or less")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "Employee email must be 50 characters or less")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(25, ErrorMessage = "Employee phone number must be 25 characters or less")]
        [Display(Name = "Phone Number")]
        public string PhoneNr { get; set; }

        [Required]
        [Display(Name = "Registration Date")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [Display(Name = "Enterprise")]
        public int? EnterpriseID { get; set; }

        public virtual ICollection<CompetenceRating> CompotencesRatings { get; set; }
        public virtual Enterprise Enterprise { get; set; }
    }
}