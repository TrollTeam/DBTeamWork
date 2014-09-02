#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ContextGenerator.ttinclude code generation file.
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
using TrollStore.Reports;


namespace MySqlTrollStoreModel	
{
	public partial class TrollStoreModel : OpenAccessContext, ITrollStoreModelUnitOfWork
	{
        private static string connectionStringName = @"mysqlTrollStoreConnection";
			
		private static BackendConfiguration backend = GetBackendConfiguration();
				
		private static MetadataSource metadataSource = new TrollStoreModelMetadataSource();
		
		public TrollStoreModel()
			:base(connectionStringName, backend, metadataSource)
		{ }
		
		public TrollStoreModel(string connection)
			:base(connection, backend, metadataSource)
		{ }
		
		public TrollStoreModel(BackendConfiguration backendConfiguration)
			:base(connectionStringName, backendConfiguration, metadataSource)
		{ }
			
		public TrollStoreModel(string connection, MetadataSource metadataSource)
			:base(connection, backend, metadataSource)
		{ }
		
		public TrollStoreModel(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
			:base(connection, backendConfiguration, metadataSource)
		{ }


        public IQueryable<MySqlProductReport> Products
        {
            get
            {
                return this.GetAll<MySqlProductReport>();
            }
        }

		public static BackendConfiguration GetBackendConfiguration()
		{
			BackendConfiguration backend = new BackendConfiguration();
			backend.Backend = "MySql";
			backend.ProviderName = "MySql.Data.MySqlClient";
		
			CustomizeBackendConfiguration(ref backend);
		
			return backend;
		}
		
		/// <summary>
		/// Allows you to customize the BackendConfiguration of TrollStoreModel.
		/// </summary>
		/// <param name="config">The BackendConfiguration of TrollStoreModel.</param>
		static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);
		
	}
	
	public interface ITrollStoreModelUnitOfWork : IUnitOfWork
	{
	}
}
#pragma warning restore 1591
