using System;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class ManagerService
	{
        SqlManagerFunctionRepository mr;
        SqlSponsorRepository sr;
        SqlSponsorAdminRepository sar;

        public ManagerService(SqlManagerFunctionRepository mr, SqlSponsorRepository sr, SqlSponsorAdminRepository sar)
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
		
		public IList<SponsorAdmin> FindAdminBySponsor(int sponsorID, int sponsorAdminID, string sort, int sortFirstName, int sortLastName, int lid)
		{
            string orderBy = "";
            if (sort == "LastName")
            {
                orderBy = string.Format(
                    "ORDER BY sa.LastName {0}, sa.Name {1}",
                    sortLastName == 0 ? "ASC" : "DESC",
                    sortFirstName == 0 ? "ASC" : "DESC"
                );
            }
            else
            {
                orderBy = string.Format(
                    "ORDER BY sa.Name {0}, sa.LastName {1}",
                    sortFirstName == 0 ? "ASC" : "DESC",
                    sortLastName == 0 ? "ASC" : "DESC"
                );   
            }

			var admins = sr.FindAdminBySponsor(sponsorID, sponsorAdminID, orderBy);
			foreach (var a in admins) {
				a.AddFunctions(mr.FindBySponsorAdminX(a.Id, lid));
			}
			return admins;
		}
	}
}
