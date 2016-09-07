// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.Core2.Models;
using HW.Core2.Repositories;

namespace HW.Core2.Services
{
	public class DepartmentService
	{
		SqlDepartmentRepository departmentRepo = new SqlDepartmentRepository();
		
		public DepartmentService()
		{
		}
		
		public IList<Department> FindAllDepartments()
		{
			return departmentRepo.FindAll();
		}
	}
}
