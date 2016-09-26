// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;
using System.Collections.Generic;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class OptionRepositoryTests
	{
		OptionRepositoryStub or = new OptionRepositoryStub();
		OptionComponentRepositoryStub ocr = new OptionComponentRepositoryStub();
		
		[Test]
		public void TestMethod()
		{
			var o = or.Read(1);
			Assert.AreEqual(1, o.OptionID);
		}
		
		[Test]
		public void TestMethod2()
		{
			var oc = ocr.Read(1);
			Assert.AreEqual(1, oc.ExportValue);
		}
	}
	
	public class OptionRepositoryStub : BaseRepositoryStub<Option>, IOptionRepository
	{
		public OptionRepositoryStub()
		{
			items.Add(new Option { OptionID = 1 });
		}
		
		public override Option Read(int id)
		{
			return items.Find(x => x.OptionID == id);
		}
	}
	
	public class OptionComponentsRepositoryStub : BaseRepositoryStub<OptionComponents>, IOptionComponentsRepository
	{
		public OptionComponentsRepositoryStub()
		{
			items.Add(new OptionComponents { OptionID = 1, OptionComponentID = 1});
			items.Add(new OptionComponents { OptionID = 1, OptionComponentID = 2});
			items.Add(new OptionComponents { OptionID = 1, OptionComponentID = 3});
			items.Add(new OptionComponents { OptionID = 1, OptionComponentID = 4});
			items.Add(new OptionComponents { OptionID = 1, OptionComponentID = 5});
		}
		
		public IList<OptionComponents> FindByOption(int optionID)
		{
			return items.FindAll(x => x.OptionID == optionID);
		}
	}
	
	public class OptionComponentRepositoryStub : BaseRepositoryStub<OptionComponent>, IOptionComponentRepository
	{
		public OptionComponentRepositoryStub()
		{
			items.Add(new OptionComponent { OptionComponentID = 5, Internal = "Completely agree", ExportValue = 5 });
			items.Add(new OptionComponent { OptionComponentID = 4, Internal = "Agree", ExportValue = 4 });
			items.Add(new OptionComponent { OptionComponentID = 3, Internal = "Neither agree nor disagree", ExportValue = 3 });
			items.Add(new OptionComponent { OptionComponentID = 2, Internal = "Disagree", ExportValue = 2 });
			items.Add(new OptionComponent { OptionComponentID = 1, Internal = "Complete disagree", ExportValue = 1 });
		}
		
		public override OptionComponent Read(int id)
		{
			return items.Find(x => x.OptionComponentID == id);
		}
	}
	
	public class OptionComponentLangRepositoryStub : BaseRepositoryStub<OptionComponentLang>, IOptionComponentLangRepository
	{
		public OptionComponentLangRepositoryStub()
		{
		}
		
		public IList<OptionComponentLang> FindByOptionComponent(int optionComponentID)
		{
			return items.FindAll(x => x.OptionComponentID == optionComponentID);
		}
	}
}
