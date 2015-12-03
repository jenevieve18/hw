﻿using System;
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
		public string Code { get; set; }
		public override string ToString()
		{
			return string.Format("{0} - {1}", Name, Code);
		}
	}
}