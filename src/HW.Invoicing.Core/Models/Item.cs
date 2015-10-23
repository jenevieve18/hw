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
		public Milestone Milestone { get; set; }

        public const int OPEN = 0;
        public const int INPROGRESS = 1;
        public const int FIXED = 2;
        public const int TESTED = 3;
        public const int DEACTIVATED = 4;
		
		public static List<Status> GetStatuses()
		{
			return new List<Status>(
				new[] {
					new Status { Id = OPEN, Name = "Open" },
                    new Status { Id = INPROGRESS, Name = "In Progress" },
					new Status { Id = FIXED, Name = "Fixed" },
					new Status { Id = TESTED, Name = "Tested" },
					new Status { Id = DEACTIVATED, Name = "Deactivated" },
				}
			);
		}
		
		public string GetStatus()
		{
			switch (Status) {
					case OPEN: return "<span class='label label-default'>OPEN</span>";
					case FIXED: return "<span class='label label-success'>FIXED</span>";
					case TESTED: return "<span class='label label-warning'>TESTED</span>";
                    case INPROGRESS: return "<span class='label label-info'>IN PROGRESS</span>";
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
            Errors.Clear();
            AddErrorIf(Name == "", "Item name shouldn't be empty.");
            AddErrorIf(Price <= 0, "Price should be at least greater than zero.");
        }
	}
}
