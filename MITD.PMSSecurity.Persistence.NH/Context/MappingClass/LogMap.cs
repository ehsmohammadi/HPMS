using System;
using MITD.Core;
using MITD.Data.NH;
using MITD.PMSSecurity.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using MITD.PMSSecurity.Domain.Logs;

namespace MITD.PMSSecurity.Persistence.NH.Context.MappingClass
{
    public class LogMap : ClassMapping<Log>
    {
        public LogMap()
        {
            Table("Logs");
            Id("dbId", m =>
            {
                m.Column("Id");
                m.Generator(Generators.HighLow, gm => gm.Params(new
                {
                    table = "NH_HiLo",
                    column = "NextHi",
                    max_lo = 1,
                    where = String.Format("TableKey = '{0}'", "Logs")
                }));
            });

            Component(p => p.Id, m =>
            {
                m.Access(Accessor.Field);
                m.Property("dbId", mapper =>
                {
                    mapper.Column("Id");
                    mapper.Generated(PropertyGeneration.Always);
                });
                m.Property(i => i.Guid, mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Column("GUID");
                    mapper.NotNullable(true);
                });
            });


            Property(pi => pi.Code, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(100);
                mapper.NotNullable(true);
            });
            Property(pi => pi.ClassName, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(256);
                mapper.NotNullable(true);
            });
            Property(pi => pi.MethodName, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(256);
                mapper.NotNullable(true);
            });

            Property(pi => pi.LogDate, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.NotNullable(true);
            });

            Property(pi => pi.LogLevel, mapper =>
            {
                mapper.Type<EnumerationTypeConverter<LogLevel>>();
                mapper.Column("LogLevelId");
                mapper.Access(Accessor.Field);
                mapper.NotNullable(true);
            });

            Property(pi => pi.Messages, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.NotNullable(true);
            });

            Property(pi => pi.Title, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.NotNullable(true);
            });

            Component(pi => pi.PartyId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Property("dbId", m =>
                {
                    m.Column("UserId");
                    m.Access(Accessor.Field);
                    m.NotNullable(false);
                    m.Lazy(false);
                });
                mapper.Property(p => p.PartyName, m =>
                    {
                        m.Column("UserName");
                        m.Access(Accessor.Field);
                        m.NotNullable(false);
                    });


            });

        }
    }

    public class ExceptionLogMap : JoinedSubclassMapping<ExceptionLog>
    {
        public ExceptionLogMap()
        {
            Table("ExceptionLogs");
            Key(m => m.Column("Id"));

        }
    }


    public class EventLogMap : JoinedSubclassMapping<EventLog>
    {
        public EventLogMap()
        {
            Table("EventLogs");
            Key(m => m.Column("Id"));

        }
    }

}
