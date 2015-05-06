using System;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.Core.Services
{
	public class ManagerService
	{
		IManagerFunctionRepository mr;
		ISponsorRepository sr;
		ISponsorAdminRepository sar;
		
		public ManagerService(IManagerFunctionRepository mr, ISponsorRepository sr, ISponsorAdminRepository sar)
		{
			this.mr = mr;
			this.sr = sr;
			this.sar = sar;
		}
		
		public bool SponsorAdminHasAccess(int sponsorAdminID, int functionID)
		{
			return sar.SponsorAdminHasAccess(sponsorAdminID, functionID);
		}
		
		public void SaveSponsorAdminSessionFunction(int sponsorAdminSessionID, int functionID, DateTime date)
		{
			sr.SaveSponsorAdminSessionFunction(sponsorAdminSessionID, functionID, date);
		}
		
		public void UpdateDeletedAdmin(int sponsorID, int sponsorAdminIDToBeDeleted)
		{
			sr.UpdateDeletedAdmin(sponsorID, sponsorAdminIDToBeDeleted);
		}
		
		public IList<SponsorAdmin> FindAdminBySponsor(int sponsorID, int sponsorAdminID, int sort, int lid)
		{
			var admins = sr.FindAdminBySponsor(sponsorID, sponsorAdminID, sort == 0 ? "ASC" : "DESC");
			foreach (var a in admins) {
				a.AddFunctions(mr.FindBySponsorAdminX(a.Id, lid));
			}
			return admins;
		}
	}
}
