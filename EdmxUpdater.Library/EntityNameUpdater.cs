using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EdmxUpdater.Schemas;

namespace EdmxUpdater.Library
{
    public sealed class EntityNameUpdater : EntityUpdaterBase
    {
        public EntityNameUpdater()
        {
            PropertyNoUnderscores = true;
            PropertyPascalCase = true;
        }

        public bool PropertyPascalCase { get; set; }
        public bool PropertyNoUnderscores { get; set; }

        string UpdateName(string storageName)
        {
            var conceptualName = storageName;

            if (PropertyPascalCase)
            {
                //TODO: better tokenize, split on case, etc...
                var tokens = storageName.Split('_');

                conceptualName = string.Join("_", tokens.Select(t => t.Substring(0, 1).ToUpperInvariant() + t.Substring(1)));
            }

            if (PropertyNoUnderscores)
            {
                conceptualName = conceptualName.Replace("_", "");
            }

            return conceptualName;
        }

        protected override void UpdateEdmx(TEdmx edmx)
        {
            var scalarProperties = from esm in edmx.Runtime.Mappings.Mapping.EntityContainerMapping.EntitySetMapping
                                   from etm in esm.EntityTypeMapping
                                   from f in etm.MappingFragment
                                   from sp in f.ScalarProperty
                                   select new { ScalarProperty = sp, etm.TypeName };

            var mapQuery = from sp in scalarProperties
                           let item = new { sp.TypeName, sp.ScalarProperty.Name, sp.ScalarProperty.ColumnName }
                           group item by sp.TypeName into g
                           select g;

            //build map of new entity property names
            var map = mapQuery.ToDictionary(g => g.Key, g => g.Distinct().ToDictionary(sp => sp.Name, sp => UpdateName(sp.ColumnName)));

            //update map property names:
            foreach (var sp in scalarProperties)
            {
                sp.ScalarProperty.Name = map[sp.TypeName][sp.ScalarProperty.Name];
            }

            //conceptual entities
            foreach(var entity in edmx.Runtime.ConceptualModels.Schema.EntityType)
            {
                var typeName = String.Format("{0}.{1}", edmx.Runtime.ConceptualModels.Schema.Namespace, entity.Name);
                var typeMap = map[typeName];

                //keys
                foreach (var keyRef in entity.Key.PropertyRef)
                {
                    keyRef.Name = typeMap[keyRef.Name];
                }

                //conceptual properties
                foreach (var property in entity.Property)
                {
                    property.Name = typeMap[property.Name];
                }
            }

            var principalPropertyQuery = from association in edmx.Runtime.ConceptualModels.Schema.Association
                                         let end = association.End.Where(e => e.Role == association.ReferentialConstraint.Principal.Role).Single()
                                         from property in association.ReferentialConstraint.Principal.PropertyRef
                                         select new { TypeName = end.Type, Property = property };

            var dependentPropertyQuery = from association in edmx.Runtime.ConceptualModels.Schema.Association
                                         let end = association.End.Where(e => e.Role == association.ReferentialConstraint.Dependent.Role).Single()
                                         from property in association.ReferentialConstraint.Dependent.PropertyRef
                                         select new { TypeName = end.Type, Property = property };

            foreach (var property in principalPropertyQuery.Union(dependentPropertyQuery))
            {
                property.Property.Name = map[property.TypeName][property.Property.Name];
            }
        }
    }
}
