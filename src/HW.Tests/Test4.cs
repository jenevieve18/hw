//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core.Models;
using HW.Core.Repositories.NHibernate;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests
{
	[TestFixture]
	public class Test4
	{
		[Test]
		public void TestMethod()
		{
			var q = new NHibernateQuestionRepository().ReadBackgroundQuestion(3);
			foreach (var l in q.Languages) {
				Console.WriteLine(l);
			}
		}
		
		[Test]
		public void a()
		{
			var s = new NHibernateSponsorRepository().Read(96);
			Console.WriteLine(s.ProjectRoundUnit.Name);
		}
		
		[Test]
		public void b()
		{
		}
		
		[Test]
		public void c()
		{
			string s = "\r\nSELECT d.Department,\r\n\tdbo.cf_departmentDepth(d.DepartmentID),\r\n\td.DepartmentID,\r\n\t(\r\n\t\tSELECT COUNT(*) FROM Department x\r\n\t\tWHERE (x.ParentDepartmentID = d.ParentDepartmentID OR x.ParentDepartmentID IS NULL AND d.ParentDepartmentID IS NULL)\r\n\t\tAND d.SponsorID = x.SponsorID\r\n\t\tAND d.SortString < x.SortString\r\n\t),\r\n\tISNULL(sad.DepartmentID, sa.SuperUser),\r\n\td.DepartmentShort\r\nFROM Department d\r\nINNER JOIN SponsorAdmin sa ON sa.SponsorAdminID = 514\r\nLEFT OUTER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID\r\nAND sad.SponsorAdminID = 514 \r\nWHERE d.SponsorID = 83\r\nORDER BY d.SortString";
			Console.WriteLine(s);
		}
		
		[Test]
		public void d()
		{
			new SqlSponsorRepository().UpdateSponsorAdmin(
				new SponsorAdmin {
					Name = "Karin",
					Email = "",
					Usr = "",
					Password = "",
					Sponsor = new Sponsor { Id = 1 }
				}
			);
		}
	}
}
