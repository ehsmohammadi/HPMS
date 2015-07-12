using MITD.Data.NH;
using MITD.PMS.Domain.Model.Units;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMS.Persistence.NH
{
    public class UnitInPeriodMap : ClassMapping<Unit>
    {
        public UnitInPeriodMap()
        {
            Table("Periods_Units");
            Id("dbId", m =>
                {
                    m.Column("Id");
                    m.Generator(Generators.HighLow, i => i.Params(new
                        {
                            table = "NH_Hilo",
                            column = "NextHi",
                            max_Lo = "1",
                            where = string.Format("TableKey='{0}'", "Periods_Units")
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

                    mapper.Component(x => x.SharedUnitId, m =>
                    {

                        m.Access(Accessor.Field);
                        m.Property(i => i.Id, map =>
                            {
                                map.Generated(PropertyGeneration.Always);
                                map.Column("UnitId");
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

            ManyToOne<SharedUnit>("sharedUnit", m =>
                {
                    m.Column("UnitId");
                    m.Lazy(LazyRelation.NoLazy);
                });

            ManyToOne<Unit>("Parent", m =>
            {
                m.Access(Accessor.Field);
                m.Column("ParentId");
                m.Lazy(LazyRelation.NoLazy);
            });


        }
    }

    public class UnitCustomFieldMap : ClassMapping<UnitCustomField>
    {
        public UnitCustomFieldMap()
        {

            Table("Periods_Units_CustomFields");
            Id("dbId", m =>
            {
                m.Column("Id");
                m.Generator(Generators.HighLow, i => i.Params(new
                {
                    table = "NH_Hilo",
                    column = "NextHi",
                    max_Lo = "1",
                    where = string.Format("TableKey='{0}'", "Periods_Units_CustomFields")
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
                        map.Generated(PropertyGeneration.Always);
                        map.Column("CustomFieldId");
                        map.NotNullable(true);
                        map.Access(Accessor.Field);
                    });
                });

            });

            ManyToOne<SharedUnitCustomField>("sharedUnitCustomField", m =>
            {

                m.Column("CustomFieldId");
                m.Lazy(LazyRelation.NoLazy);
                m.Fetch(FetchKind.Join);
            });

        }
    }

    public class SharedUnitMap : ClassMapping<SharedUnit>
    {
        public SharedUnitMap()
        {

            Table("Units");
            ComponentAsId(p => p.Id, m =>
                {
                    m.Access(Accessor.Field);
                    m.Property(p => p.Id, map => map.Access(Accessor.Field));
                });
            Property(p => p.Name, m => m.Access(Accessor.Field));
            Property(p => p.DictionaryName, m => m.Access(Accessor.Field));
        }
    }

    public class SharedUnitCustomFieldMap : ClassMapping<SharedUnitCustomField>
    {
        public SharedUnitCustomFieldMap()
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
