using MITD.Data.NH;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class InquiryUnitIndexPointMap : ClassMapping<InquiryUnitIndexPoint>
    {
        public InquiryUnitIndexPointMap()
        {
            Table("Inquiry_UnitIndexPoints");

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

            Component(x => x.ConfigurationItemId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Lazy(false);
                    m.Column("ConfigurationItemId");
                  //  m.Generated(PropertyGeneration.Always);
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

                //mapper.Component(x => x.InquirerUnitId, m =>
                //{
                //    m.Access(Accessor.Field);
                //    m.Property("dbId", idMap =>
                //    {
                //        idMap.Lazy(false);
                //        idMap.Column("PeriodInquirerUnitId");

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
                //    m.Component(i => i.SharedUnitId,
                //        mm =>
                //        {
                //            mm.Access(Accessor.Field);
                //            mm.Property(p => p.Id, pm =>
                //            {
                //                pm.Access(Accessor.Field);
                //                pm.Column("InquirerUnitId");
                //            });
                //        });
                //});

                mapper.Component(x => x.UnitIndexIdUintPeriod, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(idMap => idMap.Id, idMap1 =>
                    {
                        idMap1.Access(Accessor.Field);
                        idMap1.Column("UnitIndexIdUintPeriod");
                                       
                    });
                });
                });
         
            #region old
            // Component(x => x.ConfigurationItemId, mapper =>
           // {
           //     mapper.Access(Accessor.Field);
           //     mapper.Property("dbId", m =>
           //     {
           //         m.Lazy(false);
           //         m.Column("ConfigurationItemId");
           //     });


           //     //mapper.Component(i => i.UnitIndexIdUintPeriod, mm =>
           //     //{
           //     //    mm.Access(Accessor.Field);
           //     //    mm.Property(p => p.Id, pm =>
           //     //    {
           //     //        pm.Access(Accessor.Field);
           //     //        pm.Column("PeriodUnitIndexId");

           //     //    });
           //     //});



           //     //mapper.Component(x => x.InquirerId, m =>
           //     //{
           //     //    m.Property("dbId", idMap =>
           //     //    {
           //     //        idMap.Lazy(false);
           //     //        idMap.Column("InquirerId");
           //     //    });

           //     //    m.Access(Accessor.Field);
           //     //    m.Property(i => i.EmployeeNo, map =>
           //     //    {
           //     //        map.Column("InquirerEmployeeNo");
           //     //        map.NotNullable(true);
           //     //        map.Access(Accessor.Field);
           //     //    });
           //     //    m.Component(i => i.PeriodId, mm =>
           //     //    {
           //     //        mm.Access(Accessor.Field);
           //     //        mm.Property(p => p.Id, pm =>
           //     //        {
           //     //            pm.Access(Accessor.Field);
           //     //            pm.Column("PeriodId");

           //     //        });
           //     //    });
           //     //});

           //     //mapper.Component(x => x.InquirySubjectId, m =>
           //     //{
           //     //    m.Property("dbId", idMap =>
           //     //    {
           //     //        idMap.Lazy(false);
           //     //        idMap.Column("InquirySubjectId");
           //     //    });


           //     //    m.Access(Accessor.Field);
           //     //    m.Property(i => i.EmployeeNo, map =>
           //     //    {
           //     //        map.Column("SubjectEmployeeNo");
           //     //        map.NotNullable(true);
           //     //        map.Access(Accessor.Field);
           //     //    });
           //     //    m.Component(i => i.PeriodId, mm =>
           //     //    {
           //     //        mm.Access(Accessor.Field);
           //     //        mm.Property(p => p.Id, pm =>
           //     //        {
           //     //            pm.Access(Accessor.Field);
           //     //            pm.Column("PeriodId");
           //     //            pm.Generated(PropertyGeneration.Always);
           //     //        });
           //     //    });
           //     //});

           //     //mapper.Component(x => x.InquirySubjectUnitId, m =>
           //     //{
           //     //    m.Access(Accessor.Field);
           //     //    m.Property("dbId", idMap =>
           //     //    {
           //     //        idMap.Lazy(false);
           //     //        idMap.Column("PeriodInquirySubjectUnitId");

           //     //    });


           //     //    m.Component(i => i.PeriodId,
           //     //        mm =>
           //     //        {
           //     //            mm.Access(Accessor.Field);
           //     //            mm.Property(p => p.Id, pm =>
           //     //            {
           //     //                pm.Access(Accessor.Field);
           //     //                pm.Column("PeriodId");
           //     //                pm.Generated(PropertyGeneration.Always);
           //     //            });
           //     //        });
           //     //    m.Component(i => i.SharedUnitId,
           //     //        mm =>
           //     //        {
           //     //            mm.Access(Accessor.Field);
           //     //            mm.Property(p => p.Id, pm =>
           //     //            {
           //     //                pm.Access(Accessor.Field);
           //     //                pm.Column("InquirySubjectUnitId");
           //     //            });
           //     //        });
           //     //});

           //     //mapper.Component(x => x.InquirerUnitId, m =>
           //     //{
           //     //    m.Access(Accessor.Field);
           //     //    m.Property("dbId", idMap =>
           //     //    {
           //     //        idMap.Lazy(false);
           //     //        idMap.Column("PeriodInquirerUnitId");

           //     //    });

           //     //    m.Component(i => i.PeriodId,
           //     //        mm =>
           //     //        {
           //     //            mm.Access(Accessor.Field);
           //     //            mm.Property(p => p.Id, pm =>
           //     //            {
           //     //                pm.Access(Accessor.Field);
           //     //                pm.Column("PeriodId");
           //     //                pm.Generated(PropertyGeneration.Always);
           //     //            });
           //     //        });
           //     //    m.Component(i => i.SharedUnitId,
           //     //        mm =>
           //     //        {
           //     //            mm.Access(Accessor.Field);
           //     //            mm.Property(p => p.Id, pm =>
           //     //            {
           //     //                pm.Access(Accessor.Field);
           //     //                pm.Column("InquirerUnitId");
           //     //            });
           //     //        });
           //     //});
            //});
            #endregion


            Property(p => p.UnitIndexValue, m =>
            {
                m.Access(Accessor.Field);
                m.Column("UnitIndexValue");
                m.NotNullable(false);
            });



       }
    }


}
