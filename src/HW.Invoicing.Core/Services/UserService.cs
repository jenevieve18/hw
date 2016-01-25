using System;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing.Core.Services
{
	public class UserService
	{
		SqlCompanyRepository cr = new SqlCompanyRepository();
		SqlUserRepository ur = new SqlUserRepository();
		
		public UserService()
		{
		}
		
		public void UpdateUser(User user, int id)
		{
			ur.Update(user, id);
		}
		
		public Company ReadCompany(int companyId)
		{
			return cr.Read(companyId);
		}
		
		public User ReadUser(int userId)
		{
			var user = ur.Read(userId);
            if (user != null) {
                user.Companies = cr.FindCompanies(userId);
                user.CompaniesThatHasAccessTo = cr.FindCompaniesThatHasAccessTo(userId);

                foreach (var c in user.AllCompanies) {
                    c.Links = ur.FindLinks(userId, c.Id);
                }
            }
			return user;
		}
	}
}
