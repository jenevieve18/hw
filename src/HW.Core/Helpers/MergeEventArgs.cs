using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Helpers
{
	public class MergeEventArgs : EventArgs
	{
		public int WeeksCount { get; set; }
	}
}
