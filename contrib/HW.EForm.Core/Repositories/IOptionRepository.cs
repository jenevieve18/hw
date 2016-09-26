using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public interface IOptionRepository : IBaseRepository<Option>
	{
	}
	
	public class OptionRepositoryStub : BaseRepositoryStub<Option>, IOptionRepository
	{
		public OptionRepositoryStub()
		{
			data.Add(new Option { OptionID = 1, OptionType = OptionTypes.SingleChoice, Internal = "" });
		}
	}
}
