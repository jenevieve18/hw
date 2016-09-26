using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public interface IOptionComponentsRepository : IBaseRepository<OptionComponents>
	{
		IList<OptionComponents> FindByOption(int optionID);
	}
	
	public class OptionComponentRepositoryStub : BaseRepositoryStub<OptionComponent>, IOptionComponentRepository
	{
		public OptionComponentRepositoryStub()
		{
			data.Add(new OptionComponent { OptionComponentID = 1, Internal = "" });
		}
	}
}
