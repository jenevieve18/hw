using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Repositories
{
	public interface IAnswerRepository : IBaseRepository<Answer>
	{
	}
	
	public class AnswerRepositoryStub : BaseRepositoryStub<Answer>, IAnswerRepository
	{
		public AnswerRepositoryStub()
		{
			data.Add(new Answer {});
		}
	}
}
