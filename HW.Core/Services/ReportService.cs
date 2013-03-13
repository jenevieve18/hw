//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core.Repositories;

namespace HW.Core.Services
{
	public class ReportService
	{
		IReportRepository rr;
		
		public ReportService(IReportRepository rr)
		{
			this.rr = rr;
		}
	}
}
