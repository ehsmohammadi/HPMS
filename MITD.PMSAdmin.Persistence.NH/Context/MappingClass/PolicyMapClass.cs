using MITD.Data.NH;
using MITD.PMSAdmin.Domain.Model.Policies;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMSAdmin.Persistence.NH
{
    public class PolicyMap : ClassMapping<Policy>
    {
        public PolicyMap()
        {
            Table("Policies");
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

}
