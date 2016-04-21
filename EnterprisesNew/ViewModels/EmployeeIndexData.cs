using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EnterprisesNew.Models;

namespace EnterprisesNew.ViewModels
{
    public class EmployeeIndexData
    {
        public IEnumerable<Employee> Employees;
        public IEnumerable<CompetenceRating> CompetenceRating;
    }
}