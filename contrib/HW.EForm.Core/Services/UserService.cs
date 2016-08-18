// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class UserService
	{
		SqlUserRepository ur = new SqlUserRepository();
		SqlManagerRepository mr = new SqlManagerRepository();
		
		public UserService()
		{
		}
		
		public Manager ReadByEmailAndPassword(string name, string password)
		{
			return mr.ReadByEmailAndPassword(name, password);
		}
	}
}
