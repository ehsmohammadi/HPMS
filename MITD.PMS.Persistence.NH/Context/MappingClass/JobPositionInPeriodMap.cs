using MITD.Data.NH;
using MITD.PMS.Domain.Model.JobPositions;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class JobPositionInPeriodMap : ClassMapping<JobPosition>
    {
        public JobPositionInPeriodMap()
        {
            Table("Periods_JobPositions");
            Id("dbId", m =>
            {
                m.Column("Id");
                m.Generator(Generators.HighLow, i => i.Params(new
                {
                    table = "NH_Hilo",
                    column = "NextHi",
                    max_Lo = "1",
                    where = string.Format("TableKey='{0}'", "Periods_JobPositions")
                }));

            });


            Component(x => x.Id, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
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

                mapper.Component(x => x.SharedJobPositionId, m =>
                {

                    m.Access(Accessor.Field);
                    m.Property(i => i.Id, map =>
                    {
                        map.Generated(PropertyGeneration.Always);
                        map.Column("JobPositionId");
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

            Component(x => x.JobId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Column("PeriodJobId");
                    //m.Generated(PropertyGeneration.Always);
                });
                mapper.Component(x => x.PeriodId, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(i => i.Id, map =>
                    {
                        map.Column("PeriodId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                        map.Insert(false);
                        map.Update(false);
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


            });

            Component(x => x.UnitId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Column("PeriodUnitId");
                    //m.Generated(PropertyGeneration.Always);
                });
                mapper.Component(x => x.PeriodId, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(i => i.Id, map =>
                    {
                        map.Column("PeriodId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                        map.Insert(false);
                        map.Update(false);
                    });
                });

                mapper.Component(x => x.SharedUnitId, m =>
                {

                    m.Access(Accessor.Field);
                    m.Property(i => i.Id, map =>
                    {
                        map.Column("UnitId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                });


            });

            ManyToOne<SharedJobPosition>(j=>j.SharedJobPosition, m =>
            {
                m.Access(Accessor.Field);
                m.Column("JobPositionId");
                m.Lazy(LazyRelation.NoLazy);

            });

            ManyToOne<JobPosition>("Parent", m =>
            {
                m.Access(Accessor.Field);
                m.Column("ParentId");
                m.Lazy(LazyRelation.NoLazy);

            });

            Bag(e => e.Employees, cm =>
            {
                cm.Table("Employees_JobPositions");
                cm.Key(k =>
                {
                    k.Column("PeriodJobPositionId");
                    k.ForeignKey("none");
                });
                cm.Access(Accessor.Field);
                cm.Cascade(Cascade.All);

            }, mapping => mapping.OneToMany());

            Bag(e => e.ConfigurationItemList, cm =>
            {
                cm.Table("JobPostion_InquiryConfigurationItems");
                cm.Key(k =>
                {
                    k.Column("PeriodInquirySubjectJobPositionId");
                    k.ForeignKey("none");
                });
                cm.Access(Accessor.Field);
                cm.Cascade(Cascade.All|Cascade.DeleteOrphans);

            }, mapping => mapping.OneToMany());


        }
    }

    public class SharedJobPositionMap : ClassMapping<SharedJobPosition>
    {
        public SharedJobPositionMap()
        {
           
            Table("JobPositions");
            ComponentAsId(p=>p.Id,m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(p => p.Id, map => map.Access(Accessor.Field));
                });
            Property(p => p.Name, m => m.Access(Accessor.Field));
            Property(p => p.DictionaryName, m => m.Access(Accessor.Field));
        }
    }

    public class JobPositionEmployeeMap : ClassMapping<JobPositionEmployee>
    {
        public JobPositionEmployeeMap()
        {
            Table("Employees_JobPositions");

            ManyToOne(p => p.JobPosition, pm =>
            {
                pm.Access(Accessor.Field);
                pm.Column("PeriodJobPositionId");
                pm.NotNullable(true);
            });

            Property("dbId", m =>
            {
                m.Column("Id");
                m.Access(Accessor.Field);
                m.Generated(PropertyGeneration.Always);
            });

            Component(x => x.EmployeeId, m =>
            {
                m.Access(Accessor.Field);
                m.Property("dbId", idMap =>
                {
                    idMap.Lazy(false);
                    idMap.Column("EmployeeId");
                    idMap.Generated(PropertyGeneration.Always);
                });

                m.Property(i => i.EmployeeNo, map =>
                {
                    map.Column("EmployeeNo");
                    map.NotNullable(true);
                    map.Access(Accessor.Field);
                    map.Generated(PropertyGeneration.Always);
                });
                m.Component(i => i.PeriodId,
                    mm =>
                    {
                        mm.Access(Accessor.Field);
                        mm.Property(p => p.Id, pm =>
                    {
                        pm.Access(Accessor.Field);
                        pm.Column("PeriodId");
                        pm.Generated(PropertyGeneration.Always);
                    });
                    });
            });

            Property(c => c.FromDate, m =>
            {
                m.Access(Accessor.Field);
                m.Generated(PropertyGeneration.Always);
            });
            Property(c => c.ToDate, m =>
            {
                m.Access(Accessor.Field);
                m.Generated(PropertyGeneration.Always);
            });

        }
    }
}
