
using System;
using MITD.Data.NH;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.UnitIndices;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class AbstractUnitIndexMap : ClassMapping<AbstractUnitIndex>
    {
        public AbstractUnitIndexMap()
        {

            Table("Periods_AbstractUnitIndices");
            ComponentAsId(p => p.Id, m =>
            {
                m.Access(Accessor.Field);
                m.Property(pi => pi.Id, mapper =>
                {
                    mapper.Access(Accessor.Field);
                });
            });
            Component(p => p.PeriodId, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(pi => pi.Id, mapper =>
                    {
                        mapper.Access(Accessor.Field);
                        mapper.Column("PeriodId");
                        mapper.NotNullable(true);
                    });
                });
            Version("rowVersion", m =>
            {
                m.Column("RowVersion");
                m.Generated(VersionGeneration.Always);
                m.Type<BinaryTimestamp>();
            });

        }

    }

    public class UnitIndexGroupMap : JoinedSubclassMapping<UnitIndexGroup>
    {
        public UnitIndexGroupMap()
        {
            Table("Periods_UnitIndexGroups");
            Key(m => m.Column("Id"));
            ManyToOne<UnitIndexGroup>(j=>j.Parent, m =>
            {
                m.Access(Accessor.Field);
                m.Column("ParentId");
                m.Lazy(LazyRelation.NoLazy);
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


    public class UnitIndexMap : JoinedSubclassMapping<UnitIndex>
    {
        public UnitIndexMap()
        {
            Table("Periods_UnitIndices");
            Key(m => m.Column("Id"));
            ManyToOne<UnitIndexGroup>(j=>j.Group, m =>
            {
                m.Access(Accessor.Field);
                m.Column("GroupId");
                m.Lazy(LazyRelation.Proxy);
            });
            Property(p => p.IsInquireable, m =>
                {
                    m.Access(Accessor.Field);
                    m.Column("IsInquireable");

                });
            Property(p => p.CalculationOrder, m =>
            {
                m.Access(Accessor.Field);
                m.Column("CalculationOrder");

            }); 
            Property(p => p.CalculationLevel, m =>
            {
                m.Access(Accessor.Field);
                m.Column("CalculationLevel");

            });
          
            ManyToOne(j=>j.SharedUnitIndex, m =>
            {

                m.Access(Accessor.Field);
                m.Column("UnitIndexId");
                m.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(j => j.ReferenceIndex, m =>
            {
                m.Access(Accessor.Field);
                m.Column("RefUnitIndexId");
            });

            Map<SharedUnitIndexCustomFieldId, string>("customFieldValues", m =>
                {
                    m.Table("Periods_UnitIndices_CustomFields");
                    m.Access(Accessor.Field);
                    m.Key(k => k.Column("PeriodUnitIndexId"));
                  

                }, map => map.Component(m => 
                    m.Property(p => p.Id, pm =>
                    {
                        pm.Column("CustomFieldTypeId");
                        pm.Access(Accessor.Field);

                    })), map => map.Element(e => e.Column("CustomFieldValue"))
                );


      
        }
    }



    public class SharedUnitIndexCustomFieldMap : ClassMapping<SharedUnitIndexCustomField>
    {
        public SharedUnitIndexCustomFieldMap()
        {

            Table("CustomFieldTypes");
            ComponentAsId(p => p.Id, m =>
            {
                m.Access(Accessor.Field);
                m.Property(p => p.Id, map => map.Access(Accessor.Field));
            });
            Property(p => p.Name, m => m.Access(Accessor.Field));
            Property(p => p.DictionaryName, m => m.Access(Accessor.Field));
            Property(p => p.MinValue, m => m.Access(Accessor.Field));
            Property(p => p.MaxValue, m => m.Access(Accessor.Field));
        }
    }

    public class SharedUnitIndexMap : ClassMapping<SharedUnitIndex>
    {
        public SharedUnitIndexMap()
        {

            Table("UnitIndices");
            ComponentAsId(p => p.Id, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(p => p.Id, map => map.Access(Accessor.Field));
                });

            Join("AbstractUnitIndices", mj =>
                {
                    mj.Table("AbstractUnitIndices");
                    mj.Key(k => k.Column("Id"));
                    //mj.Fetch(FetchKind.Select);
                    mj.Property(p => p.Name, m => m.Access(Accessor.Field));
                    mj.Property(p => p.DictionaryName, m => m.Access(Accessor.Field));

                });

        }
    }
}
