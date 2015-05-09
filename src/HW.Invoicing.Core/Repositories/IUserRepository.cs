using System;
using HW.Core.Repositories;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories
{
	public interface IUserRepository : IBaseRepository<User>
	{
		User ReadByNameAndPassword(string name, string password);
	}
	
	public class UserRepositoryStub : BaseRepositoryStub<User>, IUserRepository
	{
		public User ReadByNameAndPassword(string name, string password)
		{
			return new User { Name = name, Password = password };
		}
	}
}
