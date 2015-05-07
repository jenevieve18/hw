using System;
using HW.Core.Repositories;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories
{
	public interface IUserRepository : IBaseRepository<User>
	{
	}
	
	public class UserRepositoryStub : BaseRepositoryStub<User>, IUserRepository
	{
	}
}
