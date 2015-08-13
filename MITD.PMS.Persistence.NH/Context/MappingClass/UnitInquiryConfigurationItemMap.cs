using MITD.Data.NH;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class UnitInquiryConfigurationItemMap : ClassMapping<UnitInquiryConfigurationItem>
    {
        public UnitInquiryConfigurationItemMap()
        {
            Table("Unit_InquiryConfigurationItems");
            Id("dbId", m =>
                {
                    m.Column("Id");
                    m.Generator(Generators.HighLow, i => i.Params(new
                        {
                            table = "NH_Hilo",
                            column = "NextHi",
                            max_Lo = "1",
                            where = string.Format("TableKey='{0}'", "Unit_InquiryConfigurationItems")
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
                mapper.Component(x => x.InquirerId, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property("dbId", idMap =>
                    {
                        idMap.Lazy(false);
                        idMap.Column("InquirerId");
                    });

                    m.Property(i => i.EmployeeNo, map =>
                    {
                        map.Column("InquirerEmployeeNo");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                    m.Component(i => i.PeriodId,
                        mm =>
                        {
                            mm.Access(Accessor.Field);
                            mm.Property(p => p.Id, pm =>
                            {
                                pm.Access(Accessor.Field);
                                pm.Column("PeriodId");
                            });
                        });
                });

                //mapper.Component(x => x.InquirySubjectId, m =>
                //{
                //    m.Property("dbId", idMap =>
                //    {
                //        idMap.Lazy(false);
                //        idMap.Column("InquirySubjectId");
                //    });


                //    m.Access(Accessor.Field);
                //    m.Property(i => i.EmployeeNo, map =>
                //    {
                //        map.Column("SubjectEmployeeNo");
                //        map.NotNullable(true);
                //        map.Access(Accessor.Field);
                //    });
                //    m.Component(i => i.PeriodId,
                //        mm =>
                //        {
                //            mm.Access(Accessor.Field);
                //            mm.Property(p => p.Id, pm =>
                //            {
                //                pm.Access(Accessor.Field);
                //                pm.Column("PeriodId");
                //                pm.Generated(PropertyGeneration.Always);
                //            });
                //        });
                //});



                mapper.Component(x => x.InquirySubjectUnitId, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property("dbId", idMap =>
                    {
                        idMap.Lazy(false);
                        idMap.Column("PeriodInquirySubjectUnitId");
                        idMap.Generated(PropertyGeneration.Always);
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
                    m.Component(i => i.SharedUnitId,
                        mm =>
                        {
                            mm.Access(Accessor.Field);
                            mm.Property(p => p.Id, pm =>
                            {
                                pm.Access(Accessor.Field);
                                pm.Column("InquirySubjectUnitId");
                            });
                        });
                });

                mapper.Component(x => x.InquirerUnitId, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property("dbId", idMap =>
                    {
                        idMap.Lazy(false);
                        idMap.Column("PeriodInquirerUnitId");

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
                    m.Component(i => i.SharedUnitId,
                        mm =>
                        {
                            mm.Access(Accessor.Field);
                            mm.Property(p => p.Id, pm =>
                            {
                                pm.Access(Accessor.Field);
                                pm.Column("InquirerUnitId");
                            });
                        });
                });


            });

            ManyToOne(p => p.Unit, pm =>
            {
                pm.Access(Accessor.Field);
                pm.Column("PeriodInquirySubjectUnitId");
                pm.NotNullable(true);
            });

            Property(p => p.IsAutoGenerated, m =>
            {
                m.Access(Accessor.Field);
                m.Column("IsAutoGenerated");
                m.NotNullable(true);
            });
            
            Property(p => p.IsPermitted, m =>
            {
                m.Access(Accessor.Field);
                m.Column("IsPermitted");
                m.NotNullable(true);
            });

            
         
           


        }
    }


}
