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
		public DateTime? CreatedAt { get; set; }
		
		public static List<Status> GetStatuses()
		{
			return new List<Status>(
				new[] {
					new Status { Id = 0, Name = "Open" },
					new Status { Id = 1, Name = "Fixed" },
					new Status { Id = 2, Name = "Tested" },
					new Status { Id = 3, Name = "Deactivated" }
				}
			);
		}
		
		public string GetStatus()
		{
			switch (Status) {
					case 0: return "<span class='label label-default'>OPEN</span>";
					case 1: return "<span class='label label-success'>FIXED</span>";
					case 2: return "<span class='label label-warning'>TESTED</span>";
					default: return "<span class='label label-danger'>DEACTIVATED</span>";
			}
		}
	}
	
	public class Status : BaseModel
	{
		public string Name { get; set; }
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
