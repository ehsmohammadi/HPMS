using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using MITD.PMS.Domain.Model.Calculations;

namespace MITD.PMS.Persistence.EF
{
    public class PMSContext : DbContext
    {
        public PMSContext()
            : base("PMSContext")
        {
            Database.SetInitializer<PMSContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new CalculationIdConfiguration());
            modelBuilder.Configurations.Add(new CalculationConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Calculation> Calculations { get; set; }

        //class CalculationIdConfiguration : ComplexTypeConfiguration<CalculationId>
        //{
        //    public CalculationIdConfiguration()
        //        : base()
        //    {

        //        Property(c => c.Id).HasColumnType("bigint").IsRequired();
        //    }
        //}

        class CalculationConfiguration : EntityTypeConfiguration<Calculation>
        {
            public CalculationConfiguration()
                : base()
            {
                ToTable("Calculations");
                Property(p=>p.CalculationId.Id).HasColumnType("bigint").IsRequired();
                Property(p => p.Name).HasMaxLength(150).IsRequired();
                Ignore(c => c.PolicyId);
                Ignore(c => c.Calculator);
                Ignore(c => c.EmployeeCount);
                Ignore(c => c.EmployeeIdList);

                Ignore(c => c.EndRunTime);
                Ignore(c => c.PeriodId);
                Ignore(c => c.StartRunTime);

            }
        }

    }
}
