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
            foreach (var s in GetStatuses())
            {
                if (s.Id == Status)
                {
                    return s.Name;
                }
            }
            return "";
        }

        public const int OPEN = 0;
        public const int INPROGRESS = 1;
        public const int FIXED = 2;
        public const int DEACTIVATED = 3;

        public static List<IssueStatus> GetStatuses()
        {
            var s = new List<IssueStatus>();
            s.Add(new IssueStatus { Id = OPEN, Name = "Open" });
            s.Add(new IssueStatus { Id = INPROGRESS, Name = "In Progress" });
            s.Add(new IssueStatus { Id = FIXED, Name = "Fixed" });
            s.Add(new IssueStatus { Id = DEACTIVATED, Name = "Deactivated" });
            return s;
        }
		
		public Issue()
		{
		}
	}
}
