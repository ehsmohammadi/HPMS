using MITD.Data.NH;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class InquiryJobIndexPointMap : ClassMapping<InquiryJobIndexPoint>
    {
        public InquiryJobIndexPointMap()
        {
            Table("Inquiry_JobIndexPoints");

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
            Component(pi => pi.JobIndexId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property(p => p.Id, m =>
                {
                    m.Access(Accessor.Field);
                    m.Column("PeriodJobIndexId");
                    m.NotNullable(true);
                });
            });


            Component(x => x.ConfigurationItemId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Lazy(false);
                    m.Column("ConfigurationItemId");
                });
                mapper.Component(x => x.InquirerId, m =>
                {
                    m.Property("dbId", idMap =>
                    {
                        idMap.Lazy(false);
                        idMap.Column("InquirerId");
                    });

                    m.Access(Accessor.Field);
                    m.Property(i => i.EmployeeNo, map =>
                    {
                        map.Column("InquirerEmployeeNo");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                    m.Component(i => i.PeriodId,mm =>
                    {
                        mm.Access(Accessor.Field);
                        mm.Property(p => p.Id, pm =>
                        {
                            pm.Access(Accessor.Field);
                            pm.Column("PeriodId");

                        });
                    });
                });

                mapper.Component(x => x.InquirySubjectId, m =>
                {
                    m.Property("dbId", idMap =>
                    {
                        idMap.Lazy(false);
                        idMap.Column("InquirySubjectId");
                    });


                    m.Access(Accessor.Field);
                    m.Property(i => i.EmployeeNo, map =>
                    {
                        map.Column("SubjectEmployeeNo");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                    m.Component(i => i.PeriodId, mm =>
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

                mapper.Component(x => x.InquirySubjectJobPositionId, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property("dbId", idMap =>
                    {
                        idMap.Lazy(false);
                        idMap.Column("PeriodInquirySubjectJobPositionId");
                       
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
                    m.Component(i => i.SharedJobPositionId,
                        mm =>
                        {
                            mm.Access(Accessor.Field);
                            mm.Property(p => p.Id, pm =>
                            {
                                pm.Access(Accessor.Field);
                                pm.Column("InquirySubjectJobPositionId");
                            });
                        });
                });

                mapper.Component(x => x.InquirerJobPositionId, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property("dbId", idMap =>
                    {
                        idMap.Lazy(false);
                        idMap.Column("PeriodInquirerJobPositionId");

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
                    m.Component(i => i.SharedJobPositionId,
                        mm =>
                        {
                            mm.Access(Accessor.Field);
                            mm.Property(p => p.Id, pm =>
                            {
                                pm.Access(Accessor.Field);
                                pm.Column("InquirerJobPositionId");
                            });
                        });
                });
            });



            Property(p => p.JobIndexValue, m =>
            {
                m.Access(Accessor.Field);
                m.Column("JobIndexValue");
                m.NotNullable(false);
            });



        }
    }


}
