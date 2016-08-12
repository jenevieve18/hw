/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 8/11/2016
 * Time: 12:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
