﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW.Invoicing.Core.Models
{
	public class User : BaseModel
	{
		public string Name { get; set; }
		public string Password { get; set; }
	}
}