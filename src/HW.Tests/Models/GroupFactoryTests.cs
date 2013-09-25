using System;
using System.Collections;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Tests.Models
{
	[TestFixture]
	public class GroupFactoryTests
	{
		[Test]
		public void TestMethod()
		{
//			Hashtable desc = new Hashtable();
//			Hashtable join = new Hashtable();
//			ArrayList item = new ArrayList();
			Dictionary<string, string> desc = new Dictionary<string, string>();
			Dictionary<string, string> join = new Dictionary<string, string>();
			List<string> item = new List<string>();
			string extraDesc = "";
			GroupFactory.GetCount(3, 1, 1, 1, "0,1", ref extraDesc, desc, join, item, new DepartmentRepositoryStub(), new QuestionRepositoryStub());
		}
	}
}
