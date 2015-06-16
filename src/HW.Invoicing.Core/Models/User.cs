using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class User : BaseModel
	{
		public string Name { get; set; }
		public string Password { get; set; }
        public string Color { get; set; }
	}
}
