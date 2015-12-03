using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class User : BaseModel
	{
		public User()
		{
			Links = new List<UserLink>();
		}
		public string Username { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string Color { get; set; }
		public IList<UserCompany> Companies { get; set; }
		public Company SelectedCompany { get; set; }
		public bool HasCompanies {
			get { return Companies != null && Companies.Count > 0; }
		}
		public IList<UserLink> Links { get; set; }
		public void AddLinks(IList<UserLink> links)
		{
			foreach (var l in links) {
				AddLink(l);
			}
		}
		public void AddLink(UserLink link)
		{
			link.User = this;
			Links.Add(link);
		}
		public bool HasAccess(Link link)
		{
			if (Links != null) {
				foreach (var l in Links) {
					if (l.Link.Id == link.Id) {
						return true;
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
	}
}
