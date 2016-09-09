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
		[Test]
		public void TestMethod()
		{
		}
	}
	
	public class OptionRepositoryStub : BaseRepositoryStub<Option>, IOptionRepository
	{
		public OptionRepositoryStub()
		{
			foreach (var i in new int[] { 1, 2, 3, 4, 9 }) {
				items.Add(new Option { OptionID = i, OptionType = i, Internal = "Option" + i });
			}
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
			for (int i = 1; i <= 5; i++) {
				items.Add(new OptionComponents { OptionID = 1, OptionComponentID = i });
			}
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
			for (int i = 1; i <= 5; i++) {
				items.Add(new OptionComponent { OptionComponentID = i, Internal = "OptionComponent" + i });
			}
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
			for (int i = 1; i <= 5; i++) {
				for (int j = 1; j <= 2; j++) {
					items.Add(new OptionComponentLang { OptionComponentID = i, LangID = j, Text = "OptionComponent" + i + "Lang" + j });
				}
			}
		}
		
		public IList<OptionComponentLang> FindByOptionComponent(int optionComponentID)
		{
			return items.FindAll(x => x.OptionComponentID == optionComponentID);
		}
	}
}
