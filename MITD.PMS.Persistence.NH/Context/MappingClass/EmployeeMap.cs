using System;
using MITD.Data.NH;
using MITD.PMS.Domain.Model.Employees;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using MITD.PMS.Domain.Model.Jobs;

namespace MITD.PMS.Persistence.NH
{
    public class EmployeeMap : ClassMapping<Employee>
    {
        public EmployeeMap()
        {
            Table("Employees");
            Id("dbId", m =>
            {
                m.Column("Id");
                m.Generator(Generators.HighLow, gm => gm.Params(new
                {
                    table = "NH_HiLo",
                    column = "NextHi",
                    max_lo = 1,
                    where = String.Format("TableKey = '{0}'", "Employees")
                }));
            });
            Version("rowVersion", m =>
            {
                m.Column("RowVersion");
                m.Generated(VersionGeneration.Always);
                m.Type<BinaryTimestamp>();
            });
            Component(p => p.Id, m =>
            {
                m.Access(Accessor.Field);
                m.Property("dbId", mapper =>
                {
                    mapper.Column("Id");
                    mapper.Generated(PropertyGeneration.Always);
                });
                m.Component(i => i.PeriodId, mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Property(id => id.Id, map =>
                    {
                        map.Column("PeriodId");
                        map.Access(Accessor.Field);
                    });
                });
                m.Property(pi => pi.EmployeeNo, mapper =>
                {
                    mapper.Access(Accessor.Field);
                });
            });

            Property(p => p.FirstName, m =>
            {
                m.Access(Accessor.Field);
                m.Length(512);
                m.NotNullable(true);
            });

            Property(p => p.LastName, m =>
            {
                m.Access(Accessor.Field);
                m.Length(512);
                m.NotNullable(true);
            });
            Property(c => c.EmployeePointState, m =>
            {
                m.Access(Accessor.Field);
                m.Column("PointState");
                m.NotNullable(true);
                m.Type<EnumerationTypeConverter<EmployeePointState>>();
            });
            Property(p => p.FinalPoint, m =>
            {
                m.Access(Accessor.Field);
                m.NotNullable(false);
            });

            Map<SharedEmployeeCustomFieldId, string>(c => c.CustomFieldValues, dMapper =>
            {
                dMapper.Table("Employees_CustomFields");
                dMapper.Key(k => k.Column("EmployeeId"));
                dMapper.Access(Accessor.Field);
            }, keyMapper => keyMapper.Component(c => c.Property(kc => kc.Id, pm =>
            {
                pm.Column("CustomFieldTypeId");
                pm.Access(Accessor.Field);
            })), valueMapper => valueMapper.Element(e => e.Column("CustomFieldValue")));

            Bag(e => e.JobPositions, cm =>
            {
                cm.Table("Employees_JobPositions");
                cm.Key(k =>
                {
                    k.Column("EmployeeId");
                    k.ForeignKey("none");
                });
                cm.Access(Accessor.Field);
                cm.Cascade(Cascade.DeleteOrphans|Cascade.All);
                
            }, mapping => mapping.OneToMany()
            
            );
        }
    }

    public class SharedEmployeeCustomFieldMap : ClassMapping<SharedEmployeeCustomField>
    {
        public SharedEmployeeCustomFieldMap()
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

    public class EmployeeJobPositionMap : ClassMapping<EmployeeJobPosition>
    {
        public EmployeeJobPositionMap()
        {
            Table("Employees_JobPositions");
            Id("dbId", i =>
            {
                i.Column("Id");
                i.Generator(Generators.HighLow, gm => gm.Params(new
                {
                    table = "NH_HiLo",
                    column = "NextHi",
                    max_lo = 3,
                    where = String.Format("TableKey = '{0}'", "Employees_JobPositions")
                }));
            });
            ManyToOne(p=>p.Employee, pm =>
            {
                pm.Access(Accessor.Field);
                pm.Column("EmployeeId");
                pm.NotNullable(true);
            });


            Bag(x => x.EmployeeJobCustomFieldValues, collectionMapping =>
            {
                collectionMapping.Access(Accessor.Field);
                collectionMapping.Table("Employees_JobCustomField_Values");
                collectionMapping.Cascade(Cascade.All | Cascade.DeleteOrphans);
               
                //collectionMapping.Inverse(false);
                collectionMapping.Key(k => { 
                    k.Column("EmployeeJobPositionId");
                    k.ForeignKey("none");
                });


            }, map => map.OneToMany());

           
            Component(c => c.JobPositionId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Column("PeriodJobPositionId");
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
                        map.Column("JobPositionId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                });
            });
            Property("employeeNo", m =>
            {
                m.Access(Accessor.Field);
            });

            Property(c => c.FromDate, m =>
            {
                m.Access(Accessor.Field);
            });
            Property(c => c.ToDate, m =>
            {
                m.Access(Accessor.Field);
            });

            Property(c => c.WorkTimePercent, m =>
            {
                m.Access(Accessor.Field);
            });

            Property(c => c.JobPositionWeight, m =>
            {
                m.Access(Accessor.Field);
            });
        }
    }

    public class EmployeeJobCustomFieldValueMap : ClassMapping<EmployeeJobCustomFieldValue>
    {
        public EmployeeJobCustomFieldValueMap()
        {

            Table("Employees_JobCustomField_Values");
            Id("dbId", m =>
            {
                m.Column("Id");
                m.Generator(Generators.HighLow, i => i.Params(new
                {
                    table = "NH_Hilo",
                    column = "NextHi",
                    max_Lo = "1",
                    where = string.Format("TableKey='{0}'", "Employees_JobCustomField_Values")
                }));

            });


            Component(x => x.JobCustomFieldId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Lazy(false);
                    m.Column("PeriodJobCustomFieldTypeId");
                    
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
                        
                        map.Column("CustomFieldId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                });

            });
            Property(x=>x.JobCustomFieldValue,mapper =>
                {
                    mapper.Column("CustomFieldValue");
                    mapper.Access(Accessor.Field);
                });

        }
    }




    public class EmployeeUnitMap : ClassMapping<EmployeeUnit>
    {
        public EmployeeUnitMap()
        {
            Table("Employees_Units");
            Id("dbId", i =>
            {
                i.Column("Id");
                i.Generator(Generators.HighLow, gm => gm.Params(new
                {
                    table = "NH_HiLo",
                    column = "NextHi",
                    max_lo = 3,
                    where = String.Format("TableKey = '{0}'", "Employees_Units")
                }));
            });
            ManyToOne(p => p.Employee, pm =>
            {
                pm.Access(Accessor.Field);
                pm.Column("EmployeeId");
                pm.NotNullable(true);
            });


            Bag(x => x.EmployeeUnitCustomFieldValues, collectionMapping =>
            {
                collectionMapping.Access(Accessor.Field);
                collectionMapping.Table("Employees_UnitCustomField_Values");
                collectionMapping.Cascade(Cascade.All | Cascade.DeleteOrphans);

                //collectionMapping.Inverse(false);
                collectionMapping.Key(k =>
                {
                    k.Column("EmployeeUnitId");
                    k.ForeignKey("none");
                });


            }, map => map.OneToMany());


            Component(c => c.UnitId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Column("PeriodUnitId");
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
            Property("employeeNo", m =>
            {
                m.Access(Accessor.Field);
            });

            Property(c => c.FromDate, m =>
            {
                m.Access(Accessor.Field);
            });
            Property(c => c.ToDate, m =>
            {
                m.Access(Accessor.Field);
            });

            Property(c => c.WorkTimePercent, m =>
            {
                m.Access(Accessor.Field);
            });

            //Property(c => c.JobPositionWeight, m =>
            //{
            //    m.Access(Accessor.Field);
            //});
        }
    }

    public class EmployeeUnitCustomFieldValueMap : ClassMapping<EmployeeUnitCustomFieldValue>
    {
        public EmployeeUnitCustomFieldValueMap()
        {

            Table("Employees_UnitCustomField_Values");
            Id("dbId", m =>
            {
                m.Column("Id");
                m.Generator(Generators.HighLow, i => i.Params(new
                {
                    table = "NH_Hilo",
                    column = "NextHi",
                    max_Lo = "1",
                    where = string.Format("TableKey='{0}'", "Employees_JobCustomField_Values")
                }));

            });


            Component(x => x.UnitCustomFieldId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Lazy(false);
                    m.Column("PeriodUnitCustomFieldTypeId");

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

                mapper.Component(x => x.SharedUnitCustomFieldId, m =>
                {

                    m.Access(Accessor.Field);
                    m.Property(i => i.Id, map =>
                    {

                        map.Column("CustomFieldId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                });

            });
            Property(x => x.UnitCustomFieldValue, mapper =>
            {
                mapper.Column("CustomFieldValue");
                mapper.Access(Accessor.Field);
            });

        }
    }


}
