using System;
using System.Data;
using System.Reflection;
using MITD.Data.NH;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.Policies;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Tool.hbm2ddl;

namespace MITD.PMS.Persistence.NH
{
    public static class PMSSession
    {
        private static Lazy<ISessionFactory> sessionFactory =
            new Lazy<ISessionFactory>(() => createSessionFactory());
        private const string sessionName = "PMSDBConnection";

        private static ISessionFactory createSessionFactory()
        {
            var configure = new Configuration();
            configure.SessionFactoryName(sessionName);

            configure.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2008Dialect>();
                db.Driver<SqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.IsolationLevel = IsolationLevel.ReadCommitted;

                db.ConnectionStringName = sessionName;
                db.Timeout = 10;
            });

           
            // mapping by code 
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly()
                .GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            // mapping by xml 
            configure.AddAssembly("MITD.PMS.Persistence.NH");

            //Other setting 
            configure.AddDeserializedMapping(mapping, sessionName);
            SchemaMetadataUpdater.QuoteTableAndColumns(configure);

            return configure.BuildSessionFactory();
        }

        public static ISession GetSession()
        {
            return sessionFactory.Value.OpenSession();
        }

        public static ISession GetSession(IDbConnection conn)
        {
            return sessionFactory.Value.OpenSession(conn);
        }

        public static IStatelessSession GetStatelessSession()
        {
            return sessionFactory.Value.OpenStatelessSession();
        }
    }

    public class PolicyMap : ClassMapping<Policy>
    {
        public PolicyMap()
        {
            Table("Policies");
            Mutable(false);
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
                mapper.Length(256);
                mapper.NotNullable(true);
            });
        }
    }

    public class REPolicyMap : JoinedSubclassMapping<RuleEngineBasedPolicy>
    {
        public REPolicyMap()
        {
            Table("Policies_RE");
            Key(m => m.Column("Id"));
            IdBag(p => p.Rules, m =>
            {
                m.Table("Policies_RE_Rules");
                m.Key(i => i.Column("PolicyId"));
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
                    ma.Column("RuleId");
                });
            }));
            IdBag(p => p.RuleFunctions, m =>
            {
                m.Table("Policies_RE_RuleFunctions");
                m.Key(i => i.Column("PolicyId"));
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
                    ma.Column("RuleFunctionId");
                });
            }));
        }
    }

    public class CalculationMap : ClassMapping<Calculation>
    {
        public CalculationMap()
        {
            Table("Calculations");
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
                    m.Column(c=>c.Name("RowVersion"));
                    m.Generated(VersionGeneration.Always);
                    m.Type<BinaryTimestamp>();
                });
            Property(c => c.State, m =>
            {
                m.Access(Accessor.Field);
                m.Column("State");
                m.NotNullable(true);
                m.Type<EnumerationTypeConverter<CalculationState>>();
            });
            Property(p => p.IsDeterministic, m => m.Access(Accessor.Field));
            Property(p => p.EndRunTime, m => m.Access(Accessor.Field));
            Property(p => p.StartRunTime, m => m.Access(Accessor.Field));
            Property(p => p.Name, m => m.Access(Accessor.Field));
            Property("employeeIdDelimetedList", m =>
            {
                m.Length(int.MaxValue);
                m.Column("employeeIdDelimetedList");
            });
            Property(c => c.LibraryText, m =>
            {
                m.Access(Accessor.Field);
                m.Length(int.MaxValue);
                m.Column("LibraryText");
            });
            Property("serializedRules", m =>
            {
                m.Length(int.MaxValue);
                m.Column("Rules");
            });
            Component(p => p.CalculationResult, m =>
            {
                m.Lazy(false);
                m.Access(Accessor.Field);
                m.Property(i => i.TotalEmployeesCount, mapper =>
                {
                    mapper.Column("TotalEmployeesCount");
                    mapper.Access(Accessor.Field);
                });
                m.Property(i => i.EmployeesCalculatedCount, mapper =>
                {
                    mapper.Column("EmployeesCalculatedCount");
                    mapper.Access(Accessor.Field);
                });
                m.Property("messagesDelimited", mapper =>
                {
                    mapper.Column("Messages");
                });
                m.Property(i => i.LastCalculatedPath, mapper =>
                {
                    mapper.Column("LastCalculatedPath");
                    mapper.Access(Accessor.Field);
                });
                m.Component(p => p.LastCalculatedEmployeeId, mp =>
                {
                    mp.Access(Accessor.Field);
                    mp.Property("dbId", pm =>
                    {
                        pm.Access(Accessor.Field);
                        pm.Column("LastCalculatedEmployeeId");
                    });
                    mp.Component(i => i.PeriodId,
                     mm =>
                     {
                         mm.Access(Accessor.Field);
                         mm.Property(p => p.Id, pm =>
                         {
                             pm.Access(Accessor.Field);
                             pm.Column("LastCalculatedEmployeePeriodId");
                         });
                     });
                    mp.Property(i => i.EmployeeNo, mapper =>
                    {
                        mapper.Column("LastCalculatedEmployeeNo");
                        mapper.Access(Accessor.Field);
                    });
                });
            });
            Component(p => p.PeriodId, m =>
            {
                m.Lazy(false);
                m.Access(Accessor.Field);
                m.Property(i => i.Id, mapper =>
                {
                    mapper.Column("PeriodId");
                    mapper.Access(Accessor.Field);
                });
            });
            Component(p => p.PolicyId, m =>
            {
                m.Lazy(false);
                m.Access(Accessor.Field);
                m.Property(i => i.Id, mapper =>
                {
                    mapper.Column("PolicyId");
                    mapper.Access(Accessor.Field);
                });
            });

           
        }
    }

    public class CalculationPointMap : ClassMapping<CalculationPoint>
    {
        public CalculationPointMap()
        {
            Table("CalculationPoints");
            ComponentAsId(j => j.Id, m =>
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
            Component(j => j.PeriodId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property(id => id.Id, map =>
                {
                    map.Column("PeriodId");
                    map.Access(Accessor.Field);
                });
            });
            Component(j => j.CalculationId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property(id => id.Id, map =>
                {
                    map.Column("CalculationId");
                    map.Access(Accessor.Field);
                });
            });
            Property(j => j.Value, m =>
            {
                m.Access(Accessor.Field);
                m.NotNullable(true);
            });
            Property(j => j.Name, m =>
            {
                m.Access(Accessor.Field);
                m.Length(255);
            });
            Property(j => j.IsFinal, m =>
            {
                m.Access(Accessor.Field);
                m.NotNullable(true);
            });
        }
    }

    public class SummaryCalculationPointMap : JoinedSubclassMapping<SummaryCalculationPoint>
    {
        public SummaryCalculationPointMap()
        {
            Table("SummaryCalculationPoints");
            Key(m => m.Column("Id"));
        }
    }

    public class EmployeePointMap : JoinedSubclassMapping<EmployeePoint>
    {
        public EmployeePointMap()
        {
            Table("EmployeePoints");
            Key(m => m.Column("Id"));
            Component(j=>j.EmployeeId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", map =>
                {
                    map.Column("EmployeeId");
                });
                mapper.Component(i => i.PeriodId, map =>
                {
                    map.Access(Accessor.Field);
                    map.Property(id => id.Id, map2 =>
                    {
                        map2.Column("PeriodId");
                        map2.Access(Accessor.Field);
                        
                    });
                });
                mapper.Property(i => i.EmployeeNo, map =>
                {
                    map.Access(Accessor.Field);
                });
            });
        }
    }

    public class SummaryEmployeePointMap : JoinedSubclassMapping<SummaryEmployeePoint>
    {
        public SummaryEmployeePointMap()
        {
            Table("SummaryEmployeePoints");
            Key(m => m.Column("Id"));
        }
    }

    public class JobPositionPointMap : JoinedSubclassMapping<JobPositionPoint>
    {
        public JobPositionPointMap()
        {
            Table("JobPositionPoints");
            Key(m => m.Column("Id"));
            Component(i=>i.JobPositionId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", map =>
                {
                    map.Column("JobPositionId");
                });
                mapper.Component(id => id.SharedJobPositionId, map =>
                {
                    map.Property(id => id.Id, map2 =>
                    {
                        map2.Column("SharedJobPositionId");
                        map2.Access(Accessor.Field);
                    });
                    map.Access(Accessor.Field);
                });
                mapper.Component(i => i.PeriodId, map =>
                {
                    map.Access(Accessor.Field);
                    map.Property(id => id.Id, map2 =>
                    {
                        map2.Column("PeriodId");
                        map2.Access(Accessor.Field);
                    });
                });
            });
        }
    }

    public class SummaryJobPositionPointMap : JoinedSubclassMapping<SummaryJobPositionPoint>
    {
        public SummaryJobPositionPointMap()
        {
            Table("SummaryJobPositionPoints");
            Key(m => m.Column("Id"));
        }
    }

    public class JobIndexPointMap : JoinedSubclassMapping<JobIndexPoint>
    {
        public JobIndexPointMap()
        {
            Table("JobIndexPoints");
            Key(m => m.Column("Id"));
            Component(i=>i.JobIndexId, mapper =>
            {
                mapper.Property(id => id.Id, map =>
                {
                    map.Column("JobIndexId");
                    map.Access(Accessor.Field);
                });
            });
        }
    }
}
