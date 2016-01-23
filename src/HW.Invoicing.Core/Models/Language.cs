using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing.Core.Models
{
	public class Language : BaseModel
	{
		public string Name { get; set; }
		
		public static List<Language> GetLanguages()
		{
			return new List<Language>(
				new Language[] {
					new Language { Id = 1, Name = "Svenska" },
					new Language { Id = 2, Name = "English" },
				}
			);
		}
		
		public static Language GetLanguage(int id)
		{
			foreach (var l in GetLanguages()) {
				if (l.Id == id) {
					return l;
				}
			}
			return null;
		}
	}
}
