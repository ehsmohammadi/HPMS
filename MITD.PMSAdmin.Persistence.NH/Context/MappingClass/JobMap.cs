using System;
using MITD.Data.NH;
using MITD.PMSAdmin.Domain.Model.Jobs;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMSAdmin.Persistence.NH.Context.MappingClass
{
    public class JobMap : ClassMapping<Job>
    {
        public JobMap()
        {
            Table("Jobs");
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
            Property(pi => pi.DictionaryName, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(100);
                mapper.NotNullable(true);
            });
            Property(pi => pi.Name, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(256);
                mapper.NotNullable(true);
            });
            Property(pi => pi.TransferId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.NotNullable(false);
            });

            IdBag(p => p.CustomFieldTypeIdList, m =>
            {
                m.Table("Jobs_CustomFields");
                m.Key(i => i.Column("JobId"));
                m.Access(Accessor.Field);
                m.Id(i =>
                {
                    i.Column("Id");
                    i.Generator(Generators.Identity);
                });
            },
            x => x.Component(m =>
            {
                m.Access(Accessor.Field);
                m.Property(i => i.Id, ma =>
                {
                    ma.Access(Accessor.Field);
                    ma.Column("CustomFieldTypeId");
                });
            }));

        }
    }
}
