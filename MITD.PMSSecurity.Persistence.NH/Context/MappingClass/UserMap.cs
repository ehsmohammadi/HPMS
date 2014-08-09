using System;
using MITD.Data.NH;
using MITD.PMSSecurity.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMSSecurity.Persistence.NH.Context.MappingClass
{
    public class PartyMap : ClassMapping<Party>
    {
        public PartyMap()
        {
            Table("Parties");
            Id("dbId", m =>
            {
                m.Column("Id");
                m.Generator(Generators.HighLow, gm => gm.Params(new
                {
                    table = "NH_HiLo",
                    column = "NextHi",
                    max_lo = 1,
                    where = String.Format("TableKey = '{0}'", "Parties")
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
                m.Property(i => i.PartyName, mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Column("PartyName");
                    mapper.Length(100);
                    mapper.NotNullable(true);
                });
            });

            

            Map<int, bool>(c => c.CustomActions, dMapper =>
            {
                dMapper.Table("Parties_CustomActions");
                dMapper.Key(k => k.Column("PartyId"));
                dMapper.Access(Accessor.Field);
            }, keyMapper => keyMapper.Element(pm =>
            {
                pm.Column("ActionTypeId");

            }), valueMapper => valueMapper.Element(e => e.Column("IsGrant"))
            );


        }
    }

    public class UserMap : JoinedSubclassMapping<User>
    {
        public UserMap()
        {
            Table("Users");
            Key(m => m.Column("Id"));

            Bag(x => x.GroupList, cm =>
            {
                cm.Access(Accessor.Field);
                cm.Table("Users_Groups");
                //cm.Cascade(Cascade.All | Cascade.DeleteOrphans);
                cm.Inverse(false);
                cm.Key(k => k.Column("UserId"));
            }, map => map.ManyToMany(m => m.Column("GroupId")));

            Bag(x => x.WorkListUserList, cm =>
            {
                cm.Access(Accessor.Field);
                cm.Table("Users_WorkListUsers");
                //cm.Cascade(Cascade.All | Cascade.DeleteOrphans);
                cm.Inverse(false);
                cm.Key(k => k.Column("UserId"));
            }, map => map.ManyToMany(m => m.Column("WorkListUserId")));


            Property(pi => pi.FirstName, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(100);
                mapper.NotNullable(true);
            });
            Property(pi => pi.LastName, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(256);
                mapper.NotNullable(true);
            });
            Property(pi => pi.Email, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(256);
                mapper.NotNullable(true);
            });

            Property(pi => pi.Active, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.NotNullable(true);
            });


        }
    }


    public class GroupMap : JoinedSubclassMapping<Group>
    {
        public GroupMap()
        {
            Table("Groups");
            Key(m => m.Column("Id"));

            Property(pi => pi.Description, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(256);
                mapper.NotNullable(true);
            });

        }
    }


//    public class ActionTypeMap : ClassMapping<ActionType>
//    {
//        public ActionTypeMap()
//        {
//            Table("ActionTypes");
//            Id(p=>p.Value, m =>
//            {
//                pm.Column("Id");
//                pm.Access(Accessor.Field);
//            });
//            Property(p=>p.DisplayName , m=>
//            {
//                pm.Column("Name");
//                pm.Access(Accessor.Field);
//            });

//        }
//    }
}
