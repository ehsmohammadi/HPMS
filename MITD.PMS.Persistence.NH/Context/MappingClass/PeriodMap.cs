using MITD.Data.NH;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class PeriodMap : ClassMapping<Period>
    {
        public PeriodMap()
        {
            Table("Periods");
            ComponentAsId(p => p.Id, m =>
            {
                m.Access(Accessor.Field);
                m.Property(pi => pi.Id, mapper =>
                {
                    mapper.Access(Accessor.Field);
                });
            });

            Version("rowVersion", m =>
            {
                m.Column("RowVersion");
                m.Generated(VersionGeneration.Always);
                m.Type<BinaryTimestamp>();
            });
            Property(p => p.Name, m =>
            {
                m.Length(256);
                m.NotNullable(true);
                m.Access(Accessor.Field);
            });
            Property(p => p.StartDate, m =>
            {
                m.Access(Accessor.Field);
            });
            Property(p => p.EndDate, m =>
            {
                m.Access(Accessor.Field);
            });

            Property(p => p.Active, m =>
            {
                m.Access(Accessor.Field);
            });

            Property(c => c.State, m =>
            {
                m.Access(Accessor.Field);
                m.Column("State");
                m.NotNullable(true);
                m.Type<EnumerationTypeConverter<PeriodState>>();
            });
            
        }
    }
}
