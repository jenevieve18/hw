/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;

namespace HW.Grp.Tests
{
	[TestFixture]
	public class RemindersTests
	{
		HW.Grp.Reminders v;
		
		[SetUp]
		public void Setup()
		{
			v = new HW.Grp.Reminders();
			
			//v = new HW.Grp.Reminders(new DepartmentRepositoryStub());
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
