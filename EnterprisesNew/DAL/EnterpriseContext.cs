using EnterprisesNew.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EnterprisesNew.DAL
{
    public class EnterpriseContext : DbContext
    {
        public EnterpriseContext() : base("EnterpriseContext")
        {

        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<CompetenceRating> CompetencesRatings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}