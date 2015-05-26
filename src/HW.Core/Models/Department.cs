using System;
using System.Collections;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Department : BaseModel
	{
		public virtual Sponsor Sponsor { get; set; }
		public virtual string Name { get; set; }
		public virtual Department Parent { get; set; }
		public virtual int SortOrder { get; set; }
		public virtual string SortString { get; set; }
		public virtual string ShortName { get; set; }
		public virtual string AnonymizedName { get; set; }
		public virtual int MinUserCountToDisclose { get; set; }
		public virtual int LoginDays { get; set; }
		public virtual int LoginWeekDay { get; set; }
		
		public virtual int Depth { get; set; }
		public virtual int Siblings { get; set; }
		public virtual string TreeName { get; set; }
		public virtual double Average { get; set; }
		public virtual int Count { get; set; }
		
		public string GetReminder(Dictionary<int, string> loginDays, Dictionary<int, string> loginWeekdays)
		{
			if (LoginWeekDay == -1) {
//				return "OFF";
				return loginWeekdays[LoginWeekDay];
//				if (Sponsor.LoginWeekday == -1) {
//					return "OFF";
//				}
			} else if (Sponsor != null && Sponsor.LoginWeekday == -1) {
//				return "OFF";
				return loginWeekdays[Sponsor.LoginWeekday];
			}
			if (LoginDays == -1) {
				if (Sponsor.LoginDays == -1) {
					return "OFF";
				} else {
					return loginDays[Sponsor.LoginDays];
				}
			} else {
				return loginDays[LoginDays];
			}
		}
		
		public override string ToString()
		{
			return Name;
		}
	}
}
