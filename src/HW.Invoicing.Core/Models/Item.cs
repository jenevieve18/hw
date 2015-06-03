using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class Item : BaseModel
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
    }
}
