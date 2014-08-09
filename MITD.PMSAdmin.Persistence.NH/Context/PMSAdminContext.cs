using System;
using System.Data;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Linq.InnerJoinFetch;

namespace MITD.PMSAdmin.Persistence.NH
{
    public static class PMSAdminSession
    {
        private static Lazy<ISessionFactory> sessionFactory =
            new Lazy<ISessionFactory>(createSessionFactory);
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

            //configure.AddXmlFile("Entity2.hbm.xml");
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly()
                .GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

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
}
