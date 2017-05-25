using System;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Core.Tests.Repositories
{
	[TestFixture]
	public class DepartmentRepositoryTests
	{
		SqlDepartmentRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlDepartmentRepository();
		}
		
		[Test]
		public void TestReadByIdAndSponsor()
		{
			r.ReadByIdAndSponsor(1, 1);
		}
		
		[Test]
		public void TestDeleteSponsorAdminDepartment()
		{
			r.DeleteSponsorAdminDepartment(1, 1);
		}
		
		[Test]
		public void TestInsertSponsorAdminDepartment()
		{
			var d = new SponsorAdminDepartment {
				Department = new Department { Id = 1 }
			};
			r.SaveSponsorAdminDepartment(d);
		}
		
		[Test]
		public void TestFindBySponsorWithSponsorAdminInDepth()
		{
			r.FindBySponsorWithSponsorAdminInDepth(1, 1);
		}
		
		[Test]
		public void TestFindBySponsorInDepth()
		{
			r.FindBySponsorInDepth(1);
		}
		
		[Test]
		public void TestFindBySponsor2()
		{
			r.FindBySponsor(1);
		}
		
		[Test]
		public void TestFindBySponsor()
		{
			r.FindBySponsorOrderedBySortStringIn(1, "0", 10);
		}
		
		[Test]
		public void TestFindBySponsorWithSponsorAdmin()
		{
			r.FindBySponsorWithSponsorAdminIn(1, 1, "0", 10);
		}
		
		[Test]
		public void TestA()
		{
			r.a(1, 1);
		}
		
		[Test]
		public void TestB()
		{
			r.b(1, 1);
		}
	}
}
