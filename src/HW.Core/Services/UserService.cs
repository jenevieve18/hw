
using System;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class UserService
	{
		SqlUserRepository ur = new SqlUserRepository();
		
		public UserService()
		{
		}
		
		public User ReadByNameAndPassword(string name, string password)
		{
			return ur.Read(1);
		}
	}
}
