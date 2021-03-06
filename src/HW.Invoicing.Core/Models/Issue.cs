﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class Priority : BaseModel
    {
        public const int HIGHEST = 1;
        public const int HIGH = 2;
        public const int NORMAL = 3;
        public const int LOW = 4;
        public const int LOWEST = 5;

        public override string ToString()
        {
            return Name;
        }

		public string Name { get; set; }

        public static List<Priority> GetPriorities()
        {
            return new List<Priority>(
                new[] {
					new Priority { Id = HIGHEST, Name = "Highest (1)" },
					new Priority { Id = HIGH, Name = "High (2)" },
					new Priority { Id = NORMAL, Name = "Normal (3)" },
					new Priority { Id = LOW, Name = "Low (4)" },
					new Priority { Id = LOWEST, Name = "Lowest (5)" },
				}
            );
        }

        public static List<Priority> GetPriorities2()
        {
            return new List<Priority>(
                new[] {
					new Priority { Id = HIGHEST, Name = "<span class='text text-danger'>Highest (1)</span>" },
					new Priority { Id = HIGH, Name = "<span class='text text-warning'>High (2)</span>" },
					new Priority { Id = NORMAL, Name = "<span class='text text-info'>Normal (3)</span>" },
					new Priority { Id = LOW, Name = "<span class='text text-success'>Low (4)</span>" },
					new Priority { Id = LOWEST, Name = "<span class='text text-primary'>Lowest (5)</span>" },
				}
            );
        }
	}
	
	public class IssueComment : BaseModel
	{
		public Issue Issue { get; set; }
		public string Comments { get; set; }
		public User User { get; set; }
        public DateTime? Date { get; set; }
	}
	
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            // Calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
	
	public class Issue : BaseModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int Status { get; set; }
		public DateTime? CreatedAt { get; set; }
		public Milestone Milestone { get; set; }
		public Priority Priority { get; set; }
		public IList<IssueComment> Comments { get; set; }

        public Priority GetPriority()
        {
            foreach (var p in Priority.GetPriorities2())
            {
                if (p.Id == Priority.Id)
                {
                    return p;
                }
            }
            return null;
        }
		
		public const int OPEN = 0;
		public const int INPROGRESS = 1;
		public const int FIXED = 2;
		public const int TESTED = 3;
		public const int DEACTIVATED = 4;
		public const int DELETED = 5;
		
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
}
