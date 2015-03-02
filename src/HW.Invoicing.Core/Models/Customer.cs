using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW.Invoicing.Core.Models
{
	public class Customer : BaseModel
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
	}
}
