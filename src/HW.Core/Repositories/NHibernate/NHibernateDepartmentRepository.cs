using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateDepartmentRepository : BaseNHibernateRepository<Department>, IDepartmentRepository
	{
		public NHibernateDepartmentRepository()
		{
		}
		
		public override IList<Department> FindAll()
		{
			return base.FindAll("healthWatch");
		}
		
		public void SaveSponsorAdminDepartment(SponsorAdminDepartment d)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateDepartmentSortString(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateDepartment(Department d)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateDepartment2(Department d)
		{
			throw new NotImplementedException();
		}
		
		public void DeleteSponsorAdminDepartment(int sponsorAdminID, int departmentID)
		{
			throw new NotImplementedException();
		}
		
		public Department ReadBySponsor(int sponsorId)
		{
			throw new NotImplementedException();
		}
		
		public Department ReadByIdAndSponsor(int departmentID, int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorAdminDepartment> a(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorAdminDepartment> b(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindIn(string rndsd2)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminSortStringAndTree(int sponsorID, string sortString, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminAndTree(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorWithSponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorOrderedBySortString(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminIn(int sponsorID, int sponsorAdminID, string GID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorOrderedBySortStringIn(int sponsorID, string GID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsor(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorInDepth(int sponsorID)
		{
			throw new NotImplementedException();
		}
	}
}
