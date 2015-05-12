﻿using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IDepartmentRepository : IBaseRepository<Department>
	{
		IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID);
	}
	
	public class DepartmentRepositoryStub : BaseRepositoryStub<Department>, IDepartmentRepository
	{
		public IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID)
		{
			return new[] {
				new Department {},
				new Department {},
				new Department {}
			};
		}
	}
}
