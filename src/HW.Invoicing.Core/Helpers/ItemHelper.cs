using System;
using System.Collections.Generic;
using HW.Invoicing.Core.Models;
using Newtonsoft.Json;

namespace HW.Invoicing.Core.Helpers
{
	public class ItemHelper
	{
		public ItemHelper()
		{
		}
		
		public static string ToJson(IList<Item> items)
		{
			var o = new List<object>();
			foreach (var i in items) {
				o.Add(new { id = i.Id, name = i.Name, description = i.Description, price = i.Price });
			}
			return JsonConvert.SerializeObject(o);
		}
	}
}
