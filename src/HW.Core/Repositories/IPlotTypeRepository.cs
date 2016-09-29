using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IPlotTypeRepository : IBaseRepository<PlotType>
	{
		IList<PlotTypeLanguage> FindByLanguage(int langID);
	}
	
	public class PlotTypeRepositoryStub : BaseRepositoryStub<PlotType>, IPlotTypeRepository
	{
		public IList<PlotTypeLanguage> FindByLanguage(int langID)
		{
			return new [] {
				new PlotTypeLanguage {},
				new PlotTypeLanguage {},
				new PlotTypeLanguage {}
			};
		}
	}
}
