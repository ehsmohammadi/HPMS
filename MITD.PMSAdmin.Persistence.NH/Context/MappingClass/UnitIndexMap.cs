using MITD.Data.NH;
using MITD.PMSAdmin.Domain.Model.UnitIndices;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMSAdmin.Persistence.NH.Context.MappingClass
{
    public class AbstarctUnitIndexMap : ClassMapping<AbstractUnitIndex>
    {
        public AbstarctUnitIndexMap()
        {
            Table("AbstractUnitIndices");
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
            Property(pi => pi.TransferId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.NotNullable(false);
            });
        }
    }

    public class UnitIndexCategoryMap : JoinedSubclassMapping<UnitIndexCategory>
    {
        public UnitIndexCategoryMap()
        {
            Table("UnitIndexCategories");
            Key(m => m.Column("Id"));
            ManyToOne<UnitIndexCategory>("Parent", m =>
            {
                m.Access(Accessor.Field);
                m.Column("ParentId");
                //m.ForeignKey("Id");
                m.Lazy(LazyRelation.NoLazy);
            });

        }
    }

    public class UnitIndexMap : JoinedSubclassMapping<UnitIndex>
    {
        public UnitIndexMap()
        {
            Table("UnitIndices");
            Key(m => m.Column("Id"));
            ManyToOne<UnitIndexCategory>("Category", m =>
            {
                m.Access(Accessor.Field);
                m.Column("CategoryId");
                m.Lazy(LazyRelation.Proxy);
            });
            IdBag(p => p.CustomFieldTypeIdList, m =>
            {
                m.Table("UnitIndices_CustomFields");
                m.Key(i => i.Column("UnitIndexId"));
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
