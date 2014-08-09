
using MITD.Data.NH;
using MITD.PMS.Domain.Model.Jobs;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class JobInPeriodMap : ClassMapping<Job>
    {
        public JobInPeriodMap()
        {
            Table("Periods_Jobs");
            Id("dbId", m =>
                {
                    m.Column("Id");
                    m.Generator(Generators.HighLow, i => i.Params(new
                        {
                            table = "NH_Hilo",
                            column = "NextHi",
                            max_Lo = "1",
                            where = string.Format("TableKey='{0}'", "Periods_Jobs")
                        }));

                });
            Component(x => x.Id, mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Property("dbId", m =>
                        {
                            m.Lazy(false);
                            m.Column("Id");
                            m.Generated(PropertyGeneration.Always);
                        });
                    mapper.Component(x => x.PeriodId, m =>
                        {
                            m.Access(Accessor.Field);
                            m.Property(i => i.Id, map =>
                                {
                                    map.Column("PeriodId");
                                    map.NotNullable(true);
                                    map.Access(Accessor.Field);
                                });
                        });

                    mapper.Component(x => x.SharedJobId, m =>
                        {

                            m.Access(Accessor.Field);
                            m.Property(i => i.Id, map =>
                                {
                                    map.Generated(PropertyGeneration.Always);
                                    map.Column("JobId");
                                    map.NotNullable(true);
                                    map.Access(Accessor.Field);
                                });
                        });


                });

            Version("rowVersion", m =>
            {
                m.Column("RowVersion");
                m.Generated(VersionGeneration.Always);
                m.Type<BinaryTimestamp>();
            });

            ManyToOne<SharedJob>(j=>j.SharedJob, m =>
                {
                    m.Access(Accessor.Field);
                    m.Column("JobId");
                    m.Lazy(LazyRelation.NoLazy);
                    m.Fetch(FetchKind.Join);
                });

            //Bag(x => x.CustomFields, bag =>
            //{
            //    Table("Periods_Jobs_CustomFields");
            //    bag.Access(Accessor.Field);
            //    bag.Inverse(false); // Is collection inverse?
            //    bag.Cascade(Cascade.DeleteOrphans); //set cascade strategy
            //    bag.Key(k => k.Column(col => col.Name("PeriodJobId"))); //foreign key in Comment table
            //}, a => a.OneToMany());
            Bag(x => x.CustomFields, collectionMapping =>
                {
                    collectionMapping.Access(Accessor.Field);
                    collectionMapping.Table("Periods_Jobs_CustomFields");
                    collectionMapping.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    //collectionMapping.Inverse(false);
                    collectionMapping.Key(k => k.Column("PeriodJobId"));


                }, map => map.OneToMany());


            IdBag(p => p.JobIndexIdList, m =>
            {
                m.Table("Periods_Jobs_JobIndices");
                m.Key(i => i.Column("PeriodJobId"));
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
                    ma.Column("PeriodJobIndexId");
                });
            }));


        }
    }


    public class JobCustomFieldMap : ClassMapping<JobCustomField>
    {
        public JobCustomFieldMap()
        {

            Table("Periods_Jobs_CustomFields");
            Id("dbId", m =>
            {
                m.Column("Id");
                m.Generator(Generators.HighLow, i => i.Params(new
                {
                    table = "NH_Hilo",
                    column = "NextHi",
                    max_Lo = "1",
                    where = string.Format("TableKey='{0}'", "Periods_Jobs_CustomFields")
                }));

            });
            Component(x => x.Id, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Lazy(false);
                    m.Column("Id");
                    m.Generated(PropertyGeneration.Always);
                });
                mapper.Component(x => x.PeriodId, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(i => i.Id, map =>
                    {
                        map.Column("PeriodId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                });

                mapper.Component(x => x.SharedJobId, m =>
                {

                    m.Access(Accessor.Field);
                    m.Property(i => i.Id, map =>
                    {
                        
                        map.Column("JobId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                });

                mapper.Component(x => x.SharedJobCustomFieldId, m =>
                {

                    m.Access(Accessor.Field);
                    m.Property(i => i.Id, map =>
                    {
                        map.Generated(PropertyGeneration.Always);
                        map.Column("CustomFieldId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                });

            });

            ManyToOne<SharedJobCustomField>("sharedJobCustomField", m =>
            {

                m.Column("CustomFieldId");
                m.Lazy(LazyRelation.NoLazy);
                m.Fetch(FetchKind.Join);
            });
           
        }
    }

    public class SharedJobMap : ClassMapping<SharedJob>
    {
        public SharedJobMap()
        {
           
            Table("Jobs");
            ComponentAsId(p=>p.Id,m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(p => p.Id, map => map.Access(Accessor.Field));
                });
            Property(p => p.Name, m => m.Access(Accessor.Field));
            Property(p => p.DictionaryName, m => m.Access(Accessor.Field));
            
        }
    }


    public class SharedJobCustomFieldMap : ClassMapping<SharedJobCustomField>
    {
        public SharedJobCustomFieldMap()
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
            Property(p => p.TypeId, m => m.Access(Accessor.Field));
            

        }
    }
}
