using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing.Core.Models
{
	public class Unit : BaseModel
	{
		public Company Company { get; set; }
		public string Name { get; set; }
		public bool Inactive { get; set; }
	}
}
