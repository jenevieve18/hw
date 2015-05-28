using System;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class RemindersTests
	{
		HW.Grp.Reminders v;
		
		[SetUp]
		public void Setup()
		{
			v = new HW.Grp.Reminders();
			
			v = new HW.Grp.Reminders(new DepartmentRepositoryStub());
		}
		
		[Test]
		public void TestSave()
		{
			v.Save();
		}
		
		[Test]
		public void TestIndex()
		{
			v.Index(1, 1);
		}
	}
}
