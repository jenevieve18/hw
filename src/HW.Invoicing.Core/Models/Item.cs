using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class Issue : BaseModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int Status { get; set; }
		public bool Inactive { get; set; }
		public DateTime? CreatedAt { get; set; }
	}
	
	public class Item : BaseModel
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public Unit Unit { get; set; }
		public bool Inactive { get; set; }
    }
}
