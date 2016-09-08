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
	}
	
	public class OptionComponentsRepositoryStub : BaseRepositoryStub<OptionComponents>, IOptionComponentsRepository
	{
		public IList<OptionComponents> FindByOption(int optionID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class OptionComponentRepositoryStub : BaseRepositoryStub<OptionComponent>, IOptionComponentRepository
	{
	}
	
	public class OptionComponentLangRepositoryStub : BaseRepositoryStub<OptionComponentLang>, IOptionComponentLangRepository
	{
		public IList<OptionComponentLang> FindByOptionComponent(int optionComponentID)
		{
			throw new NotImplementedException();
		}
	}
}
