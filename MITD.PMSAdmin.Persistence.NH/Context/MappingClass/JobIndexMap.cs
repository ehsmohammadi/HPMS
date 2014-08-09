using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Data.NH;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMSAdmin.Persistence.NH.Context.MappingClass
{
    public class AbstarctJobIndexMap : ClassMapping<AbstractJobIndex>
    {
        public AbstarctJobIndexMap()
        {
            Table("AbstractJobIndices");
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
                mapper.Length(256);
                mapper.NotNullable(true);
            });
            Property(pi => pi.Name, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(512);
                mapper.NotNullable(true);
            });
        }
    }

    public class JobIndexCategoryMap : JoinedSubclassMapping<JobIndexCategory>
    {
        public JobIndexCategoryMap()
        {
            Table("JobIndexCategories");
            Key(m => m.Column("Id"));
            ManyToOne<JobIndexCategory>("Parent", m =>
            {
                m.Access(Accessor.Field);
                m.Column("ParentId");
                //m.ForeignKey("Id");
                m.Lazy(LazyRelation.NoLazy);
            });

        }
    }

    public class JobIndexMap : JoinedSubclassMapping<JobIndex>
    {
        public JobIndexMap()
        {
            Table("JobIndices");
            Key(m => m.Column("Id"));
            ManyToOne<JobIndexCategory>("Category", m =>
            {
                m.Access(Accessor.Field);
                m.Column("CategoryId");
                m.Lazy(LazyRelation.Proxy);
            });
            IdBag(p => p.CustomFieldTypeIdList, m =>
            {
                m.Table("JobIndices_CustomFields");
                m.Key(i => i.Column("JobIndexId"));
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
