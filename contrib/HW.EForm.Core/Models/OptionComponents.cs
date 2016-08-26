using System;
	
namespace HW.EForm.Core.Models
{
	public class OptionComponents
	{
		public int OptionComponentsID { get; set; }
		public int OptionComponentID { get; set; }
		public int OptionID { get; set; }
		public int ExportValue { get; set; }
		public int SortOrder { get; set; }

		public OptionComponents()
		{
		}
		
		public OptionComponents(string text)
		{
			Component = new OptionComponent(text);
		}
		
		OptionComponent component;
		
		public OptionComponent Component {
			get {
				if (component == null) {
					OnComponentGet(null);
				}
				return component;
			}
			set { component = value; }
		}
		
		public event EventHandler ComponentGet;
		
		protected virtual void OnComponentGet(EventArgs e)
		{
			if (ComponentGet != null) {
				ComponentGet(this, e);
			}
		}
	}
}
