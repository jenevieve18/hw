//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Helpers
{
	public class ConvertHelper
	{
		public ConvertHelper()
		{
		}
		
		public static int ToInt32(object val)
		{
			try {
				return Convert.ToInt32(val);
			} catch {
				return 0;
			}
		}
	}
}
