using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EnterprisesNew.Models;
namespace EnterprisesNew.DAL
{
    public class EnterpriseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EnterpriseContext>
    {
        protected override void Seed(EnterpriseContext context)
        {

            var enterprises = new List<Enterprise>
            {
                new Enterprise {Name="Present Connection", Description="Olandijos Bendrove isikurusi 2011 m.", DateCreated=DateTime.Now },
                new Enterprise {Name="Senukai", Description="Prekybos tinklas", DateCreated=DateTime.Now  }

            };
            enterprises.ForEach(e => context.Enterprises.Add(e));
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee {Name="Petras",Surname="Petraitis",Email="petras@mail.com",PhoneNr="86211111111", RegistrationDate=DateTime.Now, EnterpriseID=1 },
                new Employee {Name="Jonas",Surname="Jonaitis",Email="jonas@mail.com",PhoneNr="86211111112", RegistrationDate=DateTime.Now, EnterpriseID=1 },
                new Employee {Name="Vardenis",Surname="Pavardenis",Email="vardas@mail.com",PhoneNr="86211111113", RegistrationDate=DateTime.Now, EnterpriseID=1 },
                new Employee {Name="Ona",Surname="Onaite",Email="ona@mail.com",PhoneNr="86211111114", RegistrationDate=DateTime.Now, EnterpriseID=2 },
                new Employee {Name="Lukas",Surname="Lukaitis",Email="lukas@mail.com",PhoneNr="86211111115", RegistrationDate=DateTime.Now, EnterpriseID=2 }
            };
            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();

            var competences = new List<Competence>
            {
                new Competence {Name="Junior Programmer",Description="Jaunesnysis programuotojas", EnterpriseID = 1 },
                new Competence {Name="Senior Programmer",Description="Patyręs programuotojas", EnterpriseID = 1 },
                new Competence {Name="Public Relation",Description="Višujų ryšių specialistas", EnterpriseID = 1 },
                new Competence {Name="Pardvėjas",Description="Parduoda prekes", EnterpriseID = 2 },
                new Competence {Name="Vadybininkas",Description="Pardavimu vadybininkas", EnterpriseID = 2 }
            };
            competences.ForEach(e => context.Competences.Add(e));
            context.SaveChanges();

            var competencesRatings = new List<CompetenceRating>
            {
                new CompetenceRating {EmployeeID=1, CompetenceID=1, Grade=7, DateCreated=DateTime.Now },
                new CompetenceRating {EmployeeID=2, CompetenceID=2, Grade=8, DateCreated=DateTime.Now },
                new CompetenceRating {EmployeeID=5, CompetenceID=4, Grade=10, DateCreated=DateTime.Now },
                new CompetenceRating {EmployeeID=3, CompetenceID=1, Grade=null, DateCreated=DateTime.Now },
                new CompetenceRating {EmployeeID=4, CompetenceID=5, Grade=null, DateCreated=DateTime.Now },

            };
            competencesRatings.ForEach(e => context.CompetencesRatings.Add(e));
            context.SaveChanges();

        }
    }
}