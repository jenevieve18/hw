using System;

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
		
		public virtual int Depth { get; set; }
		public virtual int Siblings { get; set; }
		public virtual string TreeName { get; set; }
		
		public override string ToString()
		{
			return Name;
		}
	}
}
