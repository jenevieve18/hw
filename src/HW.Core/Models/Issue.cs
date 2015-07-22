using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
    public class IssueStatus : BaseModel
    {
        public string Name { get; set; }
    }

	public class Issue : BaseModel
	{
		public string Title { get; set; }
		public DateTime? Date { get; set; }
		public string Description { get; set; }
		public User User { get; set; }
        public int Status { get; set; }

        public string GetStatus()
        {
            /*foreach (var s in GetStatuses())
            {
                if (s.Id == Status)
                {
                    return s.Name;
                }
            }
            return "";*/
            /*switch (Status)
            {
                case INPROGRESS: return "<span style='background:#5bc0de'>In Progress</span>";
                case FIXED: return "<span style='background:#5cb85c'>Fixed</span>";
                case TESTED: return "<span style='background:#f0ad4e'>Tested</span>";
                case DEACTIVATED: return "<span style='background:#d9534f'>Deactivated</span>";
                default: return "<span style='background:#777'>Opan</span>";
            }*/
            switch (Status)
            {
                case INPROGRESS: return "<span class='label label-info'>In Progress</span>";
                case FIXED: return "<span class='label label-success'>Fixed</span>";
                case TESTED: return "<span class='label label-warning'>Tested</span>";
                case DEACTIVATED: return "<span class='label label-danger'>Deactivated</span>";
                default: return "<span class='label label-default'>Open</span>";
            }
        }

        public const int OPEN = 0;
        public const int INPROGRESS = 1;
        public const int FIXED = 2;
        public const int TESTED = 3;
        public const int DEACTIVATED = 4;

        public static List<IssueStatus> GetStatuses()
        {
            var s = new List<IssueStatus>();
            s.Add(new IssueStatus { Id = OPEN, Name = "Open" });
            s.Add(new IssueStatus { Id = INPROGRESS, Name = "In Progress" });
            s.Add(new IssueStatus { Id = FIXED, Name = "Fixed" });
            s.Add(new IssueStatus { Id = TESTED, Name = "Tested" });
            s.Add(new IssueStatus { Id = DEACTIVATED, Name = "Deactivated" });
            return s;
        }
		
		public Issue()
		{
		}
	}
}
