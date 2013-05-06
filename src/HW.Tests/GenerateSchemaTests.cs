//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core.Models;
using HW.Core.Repositories.NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace HW.Tests
{
	[TestFixture]
	public class GenerateSchemaTests
	{
		[Test]
		public void TestEFormExport()
		{
			var cfg = new Configuration();
			cfg.SetProperty("connection.connection_string", System.Configuration.ConfigurationManager.ConnectionStrings["eForm"].ConnectionString);
            cfg.Configure();
            
            new SchemaExport(cfg).Execute(false, true, false);
		}
		
		
		[Test]
		public void TestHealthWatchExport()
		{
			var cfg = new Configuration();
			cfg.SetProperty("connection.connection_string", System.Configuration.ConfigurationManager.ConnectionStrings["healthWatch"].ConnectionString);
            cfg.Configure();
            
            new SchemaExport(cfg).Execute(false, true, false);
		}
		
		[Test]
		public void a()
		{
			var cfg = new Configuration();
			cfg.SetProperty("connection.connection_string", System.Configuration.ConfigurationManager.ConnectionStrings["healthWatch"].ConnectionString);
            cfg.Configure();
            
            new NHibernateSponsorRepository().SaveOrUpdate(new Sponsor { Name = "Sponsor 1" });
		}
	}
}
