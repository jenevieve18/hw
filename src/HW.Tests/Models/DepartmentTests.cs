
using System;
using System.Collections.Generic;
using HW.Core.Models;
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
		public void TestName()
		{
			var d = new Department { Name = "Department1" };
			Assert.AreEqual("Department1", d.ToString());
		}
		
		[Test]
		public void TestReminder2Weeks()
		{
			var d = new Department {
				LoginWeekDay = 1, LoginDays = 14,
				Sponsor = new Sponsor { LoginWeekday = 1 },
			};
			Assert.AreEqual("2 weeks", d.GetReminder(loginDays, loginWeekdays));
		}
		
		[Test]
		public void TestReminderOff()
		{
			var d = new Department {
				LoginWeekDay = -1
			};
			Assert.AreEqual("OFF", d.GetReminder(loginDays, loginWeekdays));
		}
		
		[Test]
		public void TestReminderOffForSponsor()
		{
			var d = new Department {
				Sponsor = new Sponsor { LoginWeekday = -1 }
			};
			Assert.AreEqual("OFF", d.GetReminder(loginDays, loginWeekdays));
		}
		
		[Test]
		public void TestReminderOffForSponsor2()
		{
			var d = new Department {
				LoginWeekDay = 1, LoginDays = -1,
				Sponsor = new Sponsor { LoginWeekday = 1, LoginDays = -1 }
			};
			Assert.AreEqual("OFF", d.GetReminder(loginDays, loginWeekdays));
		}
		
		[Test]
		public void TestReminderWithSponsorLoginWeekday()
		{
			var d = new Department {
				LoginDays = -1,
				Sponsor = new Sponsor { LoginDays = 1 }
			};
			Assert.AreEqual("every day", d.GetReminder(loginDays, loginWeekdays));
		}
	}
}
