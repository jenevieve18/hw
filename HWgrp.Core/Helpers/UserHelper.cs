//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Web;
using HW.Core.Models;

namespace HWgrp.Core.Helpers
{
	public class UserHelper
	{
		public UserHelper()
		{
		}
		
		public static User LoginForm()
		{
			var r = HttpContext.Current.Request;
			return new User {
				Name = r["ANV"],
				Password = r["LOS"]
			};
		}
	}
}
