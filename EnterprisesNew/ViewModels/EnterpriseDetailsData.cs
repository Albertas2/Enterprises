using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EnterprisesNew.Models;

namespace EnterprisesNew.ViewModels
{
    public class EnterpriseDetailsData
    {
        public IEnumerable<Enterprise> Enterprises { get; set; }
        public IEnumerable<CompetenceRating> CompetenceRatings { get; set; }
    }
}