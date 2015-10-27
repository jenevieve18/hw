using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class Item : BaseModel
	{
		public Company Company { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public Unit Unit { get; set; }
		public bool Inactive { get; set; }

		public override string ToString()
		{
			return string.Format("{0} - {1}", Name, Price);
		}

		public override void Validate()
		{
			base.Validate();
//			Errors.Clear();
			AddErrorIf(Name == "", "Item name shouldn't be empty.");
			AddErrorIf(Price <= 0, "Price should be at least greater than zero.");
		}
	}
}
