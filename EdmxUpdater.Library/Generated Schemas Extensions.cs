using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdmxUpdater.Schemas
{
    partial class TEdmx
    {
        public TRuntime Runtime
        {
            get
            {
                return Items.OfType<TRuntime>().FirstOrDefault();
            }
        }
        
        public TDesigner Designer
        {
            get
            {
                return Items.OfType<TDesigner>().FirstOrDefault();
            }
        }

        public TDataServices DataServices
        {
            get
            {
                return Items.OfType<TDataServices>().FirstOrDefault();
            }
        }
    }

    partial class TEntitySetMapping
    {
        public IEnumerable<TEntityTypeMapping> EntityTypeMapping
        {
            get
            {
                return Items.OfType<TEntityTypeMapping>();
            }
        }
    }

    partial class TEntityContainerMapping
    {
        public IEnumerable<TAssociationSetMapping> AssociationSetMapping
        {
            get
            {
                return Items.OfType<TAssociationSetMapping>();
            }
        }

        public IEnumerable<TEntitySetMapping> EntitySetMapping
        {
            get
            {
                return Items.OfType<TEntitySetMapping>();
            }
        }

        public IEnumerable<TFunctionImportMapping> FunctionImportMapping
        {
            get
            {
                return Items.OfType<TFunctionImportMapping>();
            }
        }

    }

    partial class TEntityType1
    {
        public string Name
        {
            get
            {
                return AnyAttr.Where(attr => attr.Name == "Name").Select(attr => attr.Value).FirstOrDefault();
            }
        }

        public IEnumerable<TEntityProperty1> Property 
        { 
            get
            {
                return Items.OfType<TEntityProperty1>();
            }
        }
    }

    partial class TMappingFragment
    {
        public IEnumerable<TScalarProperty> ScalarProperty
        {
            get
            {
                return Items.OfType<TScalarProperty>();
            }
        }
    }
}
