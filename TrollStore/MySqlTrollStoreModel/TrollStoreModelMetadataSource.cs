#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the FluentMappingGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Telerik.OpenAccess.Metadata.Relational;

namespace MySqlTrollStoreModel
{

	public partial class TrollStoreModelMetadataSource : FluentMetadataSource
	{

        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations = new List<MappingConfiguration>();

            var productMapping = new MappingConfiguration<MySqlProduct>();
            productMapping.MapType(product => new
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Manufacturer = product.Manufacturer,
                Quantity = product.Quantity,
                Store = product.Store,

            }).ToTable("ProductReports");

            productMapping.HasProperty(p => p.ProductID).IsIdentity();
            configurations.Add(productMapping);

            return configurations;
        }
		
		protected override void SetContainerSettings(MetadataContainer container)
		{
			container.Name = "TrollStoreModel";
			container.DefaultNamespace = "MySqlTrollStoreModel";
			container.NameGenerator.SourceStrategy = Telerik.OpenAccess.Metadata.NamingSourceStrategy.Property;
			container.NameGenerator.RemoveCamelCase = false;
		}
	}
}
#pragma warning restore 1591
