using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IDepartmentRepository : IBaseRepository<Department>
	{
		void SaveSponsorAdminDepartment(SponsorAdminDepartment d);
		
		void UpdateDepartmentSortString(int sponsorID);
		
		void UpdateDepartment(Department d);
		
//		void UpdateDepartment2(Department d);
		
		void DeleteSponsorAdminDepartment(int sponsorAdminID, int departmentID);
		
		Department ReadBySponsor(int sponsorId);
		
		Department ReadByIdAndSponsor(int departmentID, int sponsorID);
		
		IList<SponsorAdminDepartment> a(int sponsorID, int sponsorAdminID);
		
		IList<SponsorAdminDepartment> b(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindIn(string rndsd2);
		
		IList<Department> FindBySponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindBySponsorWithSponsorAdminSortStringAndTree(int sponsorID, string sortString, int sponsorAdminID);
		
		IList<Department> FindBySponsorWithSponsorAdminAndTree(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindBySponsorWithSponsorAdmin(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindBySponsorOrderedBySortString(int sponsorID);
		
		IList<Department> FindBySponsorWithSponsorAdminIn(int sponsorID, int sponsorAdminID, string GID);
		
		IList<Department> FindBySponsorOrderedBySortStringIn(int sponsorID, string GID);
		
		IList<Department> FindBySponsor(int sponsorID);
		
		IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindBySponsorInDepth(int sponsorID);
	}
	
	public class DepartmentRepositoryStub : BaseRepositoryStub<Department>, IDepartmentRepository
	{
		public DepartmentRepositoryStub()
		{
			var r = new Random();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Name = "Department " + i,
					Id = i,
					ShortName = "Short Name " + i,
					Depth = r.Next(0, 8),
					Siblings = r.Next(0, 8),
					SortString = "SortString " + i
				};
				data.Add(d);
			}
		}
		
		public void SaveSponsorAdminDepartment(SponsorAdminDepartment d)
		{
		}
		
		public void UpdateDepartment(Department d)
		{
		}
		
		public void UpdateDepartment2(Department d)
		{
		}
		
		public void UpdateDepartmentSortString(int sponsorID)
		{
		}
		
		public void DeleteSponsorAdminDepartment(int sponsorAdminID, int departmentID)
		{
		}
		
		public Department ReadBySponsor(int sponsorID)
		{
			return new Department {
				SortString = "SortString",
				Parent = new Department { Id = 1 },
				Name = "Department",
				ShortName = "ShortName"
			};
		}
		
		public Department ReadByIdAndSponsor(int departmentID, int sponsorID)
		{
			return new Department {
				SortString = "SortString",
				Parent = new Department { Id = 1 },
				Name = "Department",
				ShortName = "ShortName"
			};
		}
		
		public IList<SponsorAdminDepartment> a(int sponsorID, int sponsorAdminID)
		{
			var departments = new List<SponsorAdminDepartment>();
			var r = new Random();
			for (int i = 0; i < 10; i++) {
				var d = new SponsorAdminDepartment {
					Admin = new SponsorAdmin { SuperUser = true },
					Department = new Department {
						Name = "Department " + i,
						Depth = r.Next(0, 8),
						Id = i,
						Siblings = r.Next(0, 8),
						ShortName = "Short Name " + i
					}
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<SponsorAdminDepartment> b(int sponsorID, int sponsorAdminID)
		{
			var departments = new List<SponsorAdminDepartment>();
			var r = new Random();
			for (int i = 0; i < 10; i++) {
				var d = new SponsorAdminDepartment {
					Admin = new SponsorAdmin { SuperUser = false },
					Department = new Department {
						Name = "Department " + i,
						Depth = r.Next(0, 8),
						Id = i,
						Siblings = r.Next(0, 8),
						ShortName = "Short Name " + i
					}
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindIn(string rndsd2)
		{
			var departments = new List<Department>();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Name = "Department " + i,
					TreeName = "TreeName " + i
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminSortStringAndTree(int sponsorID, string sortString, int sponsorAdminID)
		{
			var departments = new List<Department>();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Name = "Department " + i,
					TreeName = "TreeName " + i
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminAndTree(int sponsorID, int sponsorAdminID)
		{
			var departments = new List<Department>();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Id = i,
					TreeName = "TreeName " + i
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID)
		{
			var departments = new List<Department>();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Id = i,
					TreeName = "TreeName " + i
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminIn(int sponsorID, int sponsorAdminID, string GID)
		{
			return data;
		}
		
		public IList<Department> FindBySponsorOrderedBySortStringIn(int sponsorID, string GID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorWithSponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			return data;
		}
		
		public IList<Department> FindBySponsorOrderedBySortString(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsor(int sponsorID)
		{
			return data;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID)
		{
			return data;
		}
		
		public IList<Department> FindBySponsorInDepth(int sponsorID)
		{
			throw new NotImplementedException();
		}
	}
}
