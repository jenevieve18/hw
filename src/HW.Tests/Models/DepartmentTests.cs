
using System;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Grp;
using NUnit.Framework;

namespace HW.Tests.Models
{
	[TestFixture]
	public class DepartmentTests
	{
		Dictionary<int, string> loginDays;
		Dictionary<int, string> loginWeekdays;
		
		[SetUp]
		public void Setup()
		{
			loginDays = ReminderHelper.GetLoginDays();
			loginWeekdays = ReminderHelper.GetLoginWeekdays();
		}
		
		[Test]
		public void c()
		{
			var dr = new SqlDepartmentRepository();
			var sr = new SqlSponsorRepository();
			
			var d = dr.ReadWithReminder3(923);
			d.Sponsor = sr.ReadSponsor(83) as Sponsor;
			Console.WriteLine(d.GetReminder2(loginDays, loginWeekdays));
		}
		
		[Test]
		public void TestName()
		{
			var d = new Department { Name = "Department1" };
			Assert.AreEqual("Department1", d.ToString());
		}
		
		[Test]
		public void b()
		{
			int[,] days = new[,] {
				{ 1, 1, 1, 7 },
				{ 1, 7, 1, 7 },
				{ 1, 30, 1, 7 },
				{ 1, 90, 1, 7 },
				{ 1, 180, 1, 7 }
			};
			string[] values = new[] {
				"every day",
				"every day",
				"every day",
				"every day",
				"every day"
			};
			for (int i = 0; i < days.Length; i++) {
				var d = new Department {
					LoginWeekDay = days[i, 0], LoginDays = days[i, 1],
					Sponsor = new Sponsor { LoginWeekDay = days[i, 2], LoginDays = days[i, 3] }
				};
				Console.WriteLine("{0}. {1}", i, d.GetReminder(loginDays, loginWeekdays));
//				Assert.AreEqual(values[i++], d.GetReminder(loginDays, loginWeekdays));
			}
			
//			var d = new Department {
//				LoginWeekDay = 1, LoginDays = 1,
//				Sponsor = new Sponsor { LoginWeekDay = 1, LoginDays = 7 }
//			};
//			Assert.AreEqual("every day", d.GetReminder(loginDays, loginWeekdays));
		}
		
		[Test]
		public void a()
		{
			var d = new Department { LoginWeekDay = 1, LoginDays = 1 };
			Assert.AreEqual("every day", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = 1, LoginDays = 7 };
			Assert.AreEqual("week", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = 1, LoginDays = 14 };
			Assert.AreEqual("2 weeks", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = 1, LoginDays = 30 };
			Assert.AreEqual("month", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = 1, LoginDays = 90 };
			Assert.AreEqual("3 months", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = 1, LoginDays = 100 };
			Assert.AreEqual("6 months", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginWeekDay = -1 };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
		}
		
		[Test]
		public void TestReminderLoginDays()
		{
			var d = new Department { LoginWeekDay = 1, LoginDays = 1 };
			Assert.AreEqual("every day", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = -1, LoginDays = 1 };
			Assert.AreEqual("< disabled> ", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginWeekDay = 1, LoginDays = 7 };
			Assert.AreEqual("week", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = -1, LoginDays = 7 };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginWeekDay = 1, LoginDays = 14 };
			Assert.AreEqual("2 weeks", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = -1, LoginDays = 14 };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginWeekDay = 1, LoginDays = 30 };
			Assert.AreEqual("month", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = -1, LoginDays = 30 };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginWeekDay = 1, LoginDays = 90 };
			Assert.AreEqual("3 months", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = -1, LoginDays = 90 };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginWeekDay = 1, LoginDays = 100 };
			Assert.AreEqual("6 months", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginWeekDay = -1, LoginDays = 100 };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
		}
		
		[Test]
		public void TestReminderLoginWeekDay()
		{
			var d = new Department { LoginWeekDay = -1 };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
//			d = new Department { LoginWeekDay = 0, LoginDays = -1 };
//			Assert.AreEqual("< every day >", d.GetReminder(loginDays, loginWeekdays));
//			
//			d = new Department { LoginWeekDay = 1 };
//			Assert.AreEqual("Monday", d.GetReminder(loginDays, loginWeekdays));
//			
//			d = new Department { LoginWeekDay = 2 };
//			Assert.AreEqual("Tuesday", d.GetReminder(loginDays, loginWeekdays));
//			
//			d = new Department { LoginWeekDay = 3 };
//			Assert.AreEqual("Wednesday", d.GetReminder(loginDays, loginWeekdays));
//			
//			d = new Department { LoginWeekDay = 4 };
//			Assert.AreEqual("Thursday", d.GetReminder(loginDays, loginWeekdays));
//			
//			d = new Department { LoginWeekDay = 5 };
//			Assert.AreEqual("Friday", d.GetReminder(loginDays, loginWeekdays));
		}
		
		[Test]
		public void TestReminderLoginDaysSponsor()
		{
			var d = new Department { LoginDays = 1, Sponsor = new Sponsor { LoginWeekDay = 1, LoginDays = 1 } };
			Assert.AreEqual("every day", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = -1, LoginDays = 1 } };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = 1, LoginDays = 7 } };
			Assert.AreEqual("week", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = -1, LoginDays = 7 } };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = 1, LoginDays = 14 } };
			Assert.AreEqual("2 weeks", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = -1, LoginDays = 14 } };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = 1, LoginDays = 30 } };
			Assert.AreEqual("month", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = -1, LoginDays = 30 } };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = 1, LoginDays = 90 } };
			Assert.AreEqual("3 months", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = -1, LoginDays = 90 } };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
			
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = 1, LoginDays = 100 } };
			Assert.AreEqual("6 months", d.GetReminder(loginDays, loginWeekdays));
			d = new Department { LoginDays = -1, Sponsor = new Sponsor { LoginWeekDay = -1, LoginDays = 100 } };
			Assert.AreEqual("< disabled >", d.GetReminder(loginDays, loginWeekdays));
		}
	}
}
