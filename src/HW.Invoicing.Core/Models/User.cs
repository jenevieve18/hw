using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class User : BaseModel
	{
        public string Username { get; set; }
        public string Name { get; set; }
		public string Password { get; set; }
        public string Color { get; set; }
        public bool HasCompanies {
        	get { return Companies != null && Companies.Count > 0; }
        }
        public IList<UserCompany> Companies { get; set; }
        public Company SelectedCompany { get; set; }
	}
	
	public class UserCompany : BaseModel
	{
		public User User { get; set; }
		public Company Company { get; set; }
	}
}
