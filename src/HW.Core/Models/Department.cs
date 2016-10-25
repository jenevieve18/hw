using System;
using System.Collections;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public interface IDepartment
	{
		string Name { get; set; }
		string Query { get; set; }
		int MinUserCountToDisclose { get; set; }
		
//		IList<IAnswer> Answers { get; set; }
		
		IList<Week> Weeks { get; set; }
		
		Week FindWeek(string week);
	}
	
	public class Week
	{
		public string Number { get; set; }
		public IAnswer Answer { get; set; }
		public bool HasAnswer {
			get { return Answer != null; }
		}
		
		public Week(string number)
		{
			this.Number = number;
		}
	}
	
	public class Department : BaseModel, IDepartment
	{
		public Department()
		{
//			Answers = new List<IAnswer>();
			
			Weeks = new List<Week>();
		}
		
		public Department(string key, string name, int minUserCountToDisclose, string query)
		{
			this.Key = key;
			this.Name = name;
			this.MinUserCountToDisclose = minUserCountToDisclose;
			this.Query = query;
			
//			Answers = new List<IAnswer>();
			
			Weeks = new List<Week>();
		}
		
		public Week FindWeek(string week)
		{
			foreach (var w in Weeks) {
				if (w.Number == week) {
					return w;
				}
			}
			return null;
		}
		
		public Sponsor Sponsor { get; set; }
		public string Name { get; set; }
		public Department Parent { get; set; }
		public int SortOrder { get; set; }
		public string SortString { get; set; }
		public string ShortName { get; set; }
		public string AnonymizedName { get; set; }
		public int MinUserCountToDisclose { get; set; }
		public int? LoginDays { get; set; }
		public int? LoginWeekDay { get; set; }
		
		public int Depth { get; set; }
		public int Siblings { get; set; }
		public string TreeName { get; set; }
		public double Average { get; set; }
		public int Count { get; set; }
		public string Key { get; set; }
		public string Query { get; set; }
		
		public IList<Department> Parents { get; set; }
		
//		public IList<IAnswer> Answers { get; set; }
		
		public IList<Week> Weeks { get; set; }
		
		public int? GetLoginDays()
		{
			if (LoginDays >= -1) {
				return LoginDays;
			} else {
                int i = 0;
                while (i < Parents.Count && Parents[i].LoginDays == -666) {
                    i++;
                }
                if (i < Parents.Count) {
                    return Parents[i].LoginDays;
                } else {
                    return Sponsor.LoginDays;
                }
			}
		}
		
		public int? GetLoginWeekDay()
		{
			if (LoginWeekDay >= -1) {
				return LoginWeekDay;
			} else {
				int i = 0;
				while (i < Parents.Count && Parents[i].LoginWeekDay == -666) {
					i++;
				}
				if (i < Parents.Count) {
					return Parents[i].LoginWeekDay;
				} else {
					return Sponsor.LoginWeekday;
				}
			}
		}
		
		public string GetReminder2(Dictionary<int, string> loginDays, Dictionary<int, string> loginWeekDays)
		{
			int i = 0;
			while (i < Parents.Count && Parents[i].LoginWeekDay == null) {
				i++;
			}
			if (i < Parents.Count) {
				var p = Parents[i];
				if (p.LoginWeekDay.Value == -1) {
					return loginWeekDays[-1];
				} else {
					i = 0;
					while (i < Parents.Count && Parents[i].LoginDays == null) {
						i++;
					}
					if (i < Parents.Count) {
						p = Parents[i];
						return loginDays[p.LoginDays.Value];
					} else {
						return loginDays[Sponsor.LoginDays.Value];
					}
				}
			} else {
				if (Sponsor.LoginWeekday == -1) {
					return loginWeekDays[-1];
				} else {
					i = 0;
					while (i < Parents.Count && Parents[i].LoginDays == null) {
						i++;
					}
					if (i < Parents.Count) {
						var p = Parents[i];
						return loginDays[p.LoginDays.Value];
					} else {
						return loginDays[Sponsor.LoginDays.Value];
					}
				}
			}
		}
		
//		public string GetReminder(Dictionary<int, string> loginDays, Dictionary<int, string> loginWeekDays)
//		{
//			if (LoginWeekDay == -1) {
//				return loginWeekDays[LoginWeekDay.Value];
//			} else if (LoginDays != -1) {
//				return loginDays[LoginDays.Value];
//			} else if (Sponsor != null && Sponsor.LoginWeekDay == -1) {
//				return loginWeekDays[Sponsor.LoginWeekDay.Value];
//			} else {
//				return loginDays[Sponsor.LoginDays.Value];
//			}
//		}
//		
		public override string ToString()
		{
			return Name;
		}
	}
}
