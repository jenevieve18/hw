using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing.Core.Models
{
	public class Currency : BaseModel
	{
		public string Name { get; set; }
		public string ShortName { get; set; }
		
		public override string ToString()
		{
			return string.Format("{0} - {1}", ShortName, Name);
		}
		
		public static List<Currency> GetCurrencies()
		{
			return new List<Currency>(
				new Currency[] {
					new Currency { Id = 1, ShortName = "SEK", Name = "Swedish Krona" },
					new Currency { Id = 2, ShortName = "USD", Name = "U.S. Dollar" },
					new Currency { Id = 3, ShortName = "PHP", Name = "Philippine Peso" },
				}
			);
		}
		
		public static Currency GetCurrency(int id)
		{
			foreach (var c in GetCurrencies()) {
				if (c.Id == id) {
					return c;
				}
			}
			return null;
		}
	}
}
