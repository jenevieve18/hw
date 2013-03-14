//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IOptionRepository : IBaseRepository<Option>
	{
		int CountByOption(int optionID);
		
		IList<OptionComponentLanguage> FindComponentsByLanguage(int optionID, int langID);
	}
	
	public class OptionRepositoryStub : BaseRepositoryStub<Option>, IOptionRepository
	{
		public int CountByOption(int optionID)
		{
			return 10;
		}
		
		public IList<OptionComponentLanguage> FindComponentsByLanguage(int optionID, int langID)
		{
			var components = new List<OptionComponentLanguage>();
			for (int i = 0; i < 10; i++) {
				var c = new OptionComponentLanguage {
					Text = "Text " + i,
					Component = new OptionComponent { Id = 1 }
				};
				components.Add(c);
			}
			return components;
		}
	}
}
