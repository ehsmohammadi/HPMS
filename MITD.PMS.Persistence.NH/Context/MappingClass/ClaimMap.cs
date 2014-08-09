using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain.Model.Claims;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using MITD.Data.NH;

namespace MITD.PMS.Persistence.NH.Context.MappingClass
{
    public class ClaimMap : ClassMapping<Claim>
    {
        public ClaimMap()
        {
            Table("Periods_Claims");
            ComponentAsId(p=>p.Id ,m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(p=> p.Id,mp =>
                        {
                            mp.Access(Accessor.Field);
                            mp.Column("Id");
                        });
                });
            Component(p => p.PeriodId,m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(p=>p.Id ,pm =>
                        {
                            pm.Access(Accessor.Field);
                            pm.NotNullable(true);
                            pm.Column("PeriodId");
                        });
                });
            Version("rowVersion", m =>
            {
                m.Column("RowVersion");
                m.Generated(VersionGeneration.Always);
                m.Type<BinaryTimestamp>();
            });
            Property(p => p.EmployeeNo, m =>
            {
                m.Length(255);
                m.Access(Accessor.Field);
                m.NotNullable(true);
            });
            Property(p => p.Title, m =>
            {
                m.Length(128);
                m.Access(Accessor.Field);
                m.NotNullable(false);
            });
            Property(p => p.ClaimDate, m =>
            {
                m.Access(Accessor.Field);
                m.NotNullable(true);
            });
            Property(p => p.ResponseDate, m =>
            {
                m.Access(Accessor.Field);
                m.NotNullable(false);
            });

           
            Property(pi => pi.State, mapper =>
            {
                mapper.Type<EnumerationTypeConverter<ClaimState>>();
                mapper.Column("ClaimStateId");
                mapper.Access(Accessor.Field);
                mapper.NotNullable(true);
            });

            Property(pi => pi.ClaimTypeId, mapper =>
            {
                mapper.Type<EnumerationTypeConverter<ClaimTypeEnum>>();
                mapper.Column("ClaimTypeId");
                mapper.Access(Accessor.Field);
                mapper.NotNullable(true);
            });

            Property(p => p.Request, m =>
            {
                m.Length(512);
                m.Access(Accessor.Field);
                m.NotNullable(false);
            });
            Property(p => p.Response, m =>
            {
                m.Length(512);
                m.Access(Accessor.Field);
                m.NotNullable(false);
            });




        }
    }
}
