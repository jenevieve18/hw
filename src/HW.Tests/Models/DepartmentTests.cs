
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
		public void TestReminderOff()
		{
			var d = new Department {
				LoginWeekDay = -1,
				Sponsor = new Sponsor {
					LoginWeekday = -1
				}
			};
			Assert.AreEqual(d.GetReminder(loginDays, loginWeekdays), loginWeekdays[-1]);
		}
		
		[Test]
		public void TestReminderWithSponsorLoginWeekday()
		{
			var d = new Department {
				LoginWeekDay = -1,
				LoginDays = -1,
				Sponsor = new Sponsor {
					LoginDays = 1
				}
			};
			Assert.AreEqual(d.GetReminder(loginDays, loginWeekdays), loginDays[d.Sponsor.LoginDays]);
		}
	}
}
