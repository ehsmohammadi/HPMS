
using System;
using MITD.Data.NH;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Periods;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class AbstractJobIndexMap : ClassMapping<AbstractJobIndex>
    {
        public AbstractJobIndexMap()
        {

            Table("Periods_AbstractJobIndices");
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

    public class JobIndexGroupMap : JoinedSubclassMapping<JobIndexGroup>
    {
        public JobIndexGroupMap()
        {
            Table("Periods_JobIndexGroups");
            Key(m => m.Column("Id"));
            ManyToOne<JobIndexGroup>(j=>j.Parent, m =>
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


    public class JobIndexMap : JoinedSubclassMapping<JobIndex>
    {
        public JobIndexMap()
        {
            Table("Periods_JobIndices");
            Key(m => m.Column("Id"));
            ManyToOne<JobIndexGroup>(j=>j.Group, m =>
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
            //Component(p => p.SharedJobIndexId, m =>
            //{
            //    m.Access(Accessor.Field);
            //    m.Property(pi => pi.Id, mapper =>
            //    {
            //        mapper.Access(Accessor.Field);
            //        mapper.Column("JobIndexId");
            //        mapper.NotNullable(true);
            //    });
            //});

            ManyToOne(j=>j.SharedJobIndex, m =>
            {

                m.Access(Accessor.Field);
                m.Column("JobIndexId");
                m.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(j => j.ReferenceIndex, m =>
            {
                m.Access(Accessor.Field);
                m.Column("RefJobIndexId");
            });

            Map<SharedJobIndexCustomFieldId, string>("customFieldValues", m =>
                {
                    m.Table("Periods_JobIndices_CustomFields");
                    m.Access(Accessor.Field);
                    m.Key(k => k.Column("PeriodJobIndexId"));
                    //m.Cascade(Cascade.All);
                    //m.Inverse(false);

                }, map => map.Component(m => 
                    m.Property(p => p.Id, pm =>
                    {
                        pm.Column("CustomFieldTypeId");
                        pm.Access(Accessor.Field);

                    })), map => map.Element(e => e.Column("CustomFieldValue"))
                );


            //Bag(x => x.CustomFieldValues, collectionMapping =>
            //{
            //    collectionMapping.Access(Accessor.Field);
            //    collectionMapping.Table("Periods_JobIndices_CustomFields");
            //    collectionMapping.Cascade(Cascade.None);
            //    collectionMapping.Inverse(false);
            //    collectionMapping.Key(k => k.Column("JobIndexId"));


            //}, map => map.ManyToMany(p =>
            //{
            //    p.Column("CustomFieldTypeId");
            //}));

            //Map<DateTime, RuleTrail>("rulesTrail", m =>
            //{
            //    m.Key(k => k.Column("RuleId"));
            //    m.Cascade(Cascade.All);
            //    m.Inverse(true);
            //}, m => m.Element(e => e.Column("EffectiveDate")),
            //    m => m.OneToMany());

            // IdBag(p => p.CustomFieldTypeIdList, m =>
            // {
            //     m.Table("JobIndices_CustomFields");
            //     m.Key(i => i.Column("JobIndexId"));
            //     m.Access(Accessor.Field);
            //     m.Id(i =>
            //     {
            //         i.Column("Id");
            //         i.Generator(Generators.Identity);
            //     });
            // },
            //x => x.Component(m =>
            //{
            //    m.Access(Accessor.Field);
            //    m.Property(i => i.Id, ma =>
            //    {
            //        ma.Access(Accessor.Field);
            //        ma.Column("CustomFieldTypeId");
            //    });
            //}));

        }
    }



    public class SharedJobIndexCustomFieldMap : ClassMapping<SharedJobIndexCustomField>
    {
        public SharedJobIndexCustomFieldMap()
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

    public class SharedJobIndexMap : ClassMapping<SharedJobIndex>
    {
        public SharedJobIndexMap()
        {

            Table("JobIndices");
            ComponentAsId(p => p.Id, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(p => p.Id, map => map.Access(Accessor.Field));
                });

            Join("AbstractJobIndices", mj =>
                {
                    mj.Table("AbstractJobIndices");
                    mj.Key(k => k.Column("Id"));
                    //mj.Fetch(FetchKind.Select);
                    mj.Property(p => p.Name, m => m.Access(Accessor.Field));
                    mj.Property(p => p.DictionaryName, m => m.Access(Accessor.Field));

                });

        }
    }
}
