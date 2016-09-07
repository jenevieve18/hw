using System;
	
namespace HW.Core2.Models
{
	public class ManagerFunctionLang
	{
		public int ManagerFunctionLangID { get; set; }
		public int ManagerFunctionID { get; set; }
		public string ManagerFunction { get; set; }
		public string URL { get; set; }
		public string Expl { get; set; }
		public int LangID { get; set; }

		public ManagerFunctionLang()
		{
		}
	}
}
