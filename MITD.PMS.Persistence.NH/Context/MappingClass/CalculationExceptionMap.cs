using MITD.Data.NH;
using MITD.PMS.Domain.Model.CalculationExceptions;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class EmployeeCalculationExceptionMap : ClassMapping<EmployeeCalculationException>
    {
        public EmployeeCalculationExceptionMap()
        {
            Table("Calculations_Exceptions");
            ComponentAsId(p => p.Id, m =>
            {
                m.Access(Accessor.Field);
                m.Property(pi => pi.Id, mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Column("Id");
                });
            });

            Version("rowVersion", m =>
            {
                m.Column("RowVersion");
                m.Generated(VersionGeneration.Always);
                m.Type<BinaryTimestamp>();
            });
            Component(p => p.CalculationId, m =>
            {
                m.Access(Accessor.Field);
                m.Property(pm => pm.Id, pm =>
                {
                    pm.Access(Accessor.Field);
                    pm.Column("CalculationId");
                });
            });
            Component(p => p.EmployeeId, m =>
            {
                m.Access(Accessor.Field);
                m.Property("dbId", pm =>
                {
                    pm.Lazy(false);
                    pm.Column("EmployeeId");
                });
                m.Component(i => i.PeriodId,
                       mm =>
                       {
                           mm.Access(Accessor.Field);
                           mm.Property(p => p.Id, pm =>
                           {
                               pm.Access(Accessor.Field);
                               pm.Column("PeriodId");
                           });
                       });
                m.Property(pm => pm.EmployeeNo, pm =>
                {
                    pm.Access(Accessor.Field);
                    pm.Column("EmployeeNo");
                });
            });
            Property(p => p.CalculationPathNo, m =>
            {
                m.Access(Accessor.Field);
                m.Column("CalculationPathNo");
            });

            Property(p => p.Message, m =>
            {
                m.Access(Accessor.Field);
                m.Column("Message");
            });


        }
    }
}
