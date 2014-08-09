using MITD.Data.NH;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Policies;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MITD.PMSAdmin.Persistence.NH
{

    public class CustomFieldTypeMap : ClassMapping<CustomFieldType>
    {
        public CustomFieldTypeMap()
        {
            Table("CustomFieldTypes");
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
                mapper.Length(512);
                mapper.NotNullable(true);
            });
            Property(pi => pi.Name, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.Length(256);
                mapper.NotNullable(true);
            });

            Property(pi => pi.EntityId, mapper =>
            {
                mapper.Type<EnumerationTypeConverter<EntityTypeEnum>>();
                mapper.Column("EntityTypeId");
                mapper.Access(Accessor.Field);
                mapper.Length(256);
                mapper.NotNullable(true);
            });
            Property(pi => pi.MaxValue, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.NotNullable(false);
            });
            Property(pi => pi.MinValue, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.NotNullable(false);
            });
            Property(pi => pi.TypeId, mapper =>
            {
                mapper.Access(Accessor.Field);
                mapper.NotNullable(true);
            });
        }
    }
}
