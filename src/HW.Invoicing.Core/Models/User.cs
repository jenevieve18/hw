using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class User : BaseModel
	{
		public const int ACTIVE = 0;
		public const int INACTIVE = 1;
		
		public User()
		{
		}
		public string Username { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string Color { get; set; }
		public IList<Company> Companies { get; set; }
		public Company SelectedCompany { get; set; }
        public bool IsOwner(Company company)
        {
            return company.User.Id == Id;
        }
		public bool HasSelectedCompany {
			get { return SelectedCompany != null && SelectedCompany.Id > 0; }
		}
		public bool HasCompanies {
			get { return Companies != null && Companies.Count > 0; }
		}
		public IList<UserCompany> CompaniesThatHasAccessTo { get; set; }
		public bool HasCompaniesThatHasAccessTo {
			get { return CompaniesThatHasAccessTo != null && CompaniesThatHasAccessTo.Count > 0; }
		}
		public IList<Company> AllCompanies {
			get {
				var companies = new List<Company>();
				companies.AddRange(Companies);
				foreach (var x in CompaniesThatHasAccessTo) {
					companies.Add(x.Company);
				}
				return companies;
			}
		}
		public Company GetCompany(int companyId)
		{
			foreach (var c in AllCompanies) {
				if (c.Id == companyId) {
					return c;
				}
			}
			return null;
		}
//		public IList<UserLink> Links { get; set; }
//		public void AddLinks(IList<UserLink> links)
//		{
//			foreach (var l in links) {
//				AddLink(l);
//			}
//		}
//		public void AddLink(UserLink link)
//		{
//			link.User = this;
//			Links.Add(link);
//		}
		public bool HasAccess(Link link, int companyId)
		{
			var company = GetCompany(companyId);
			if (company != null) {
				if (company.Links != null) {
					foreach (var l in GetCompany(companyId).Links) {
						if (l.Id == link.Id) {
							return true;
						}
					}
				}
			}
			return false;
		}
	}
	
	public class UserLink : BaseModel
	{
		public User User { get; set; }
		public Link Link { get; set; }
	}
	
	public class UserCompany : BaseModel
	{
		public User User { get; set; }
		public Company Company { get; set; }
//		public IList<UserCompanyLink> Links { get; set; }
	}
//	
//	public class UserCompanyLink : BaseModel
//	{
//		public User User { get; set; }
//		public Company Company { get; set; }
//		public Link Link { get; set; }
//	}
}
