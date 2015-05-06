using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IDepartmentRepository : IBaseRepository<Department>
	{
	}
	
	public class DepartmentRepositoryStub : BaseRepositoryStub<Department>, IDepartmentRepository
	{
	}
}
