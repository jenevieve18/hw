/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 8/11/2016
 * Time: 12:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Repositories;
using NUnit.Framework;
using HW.Core.Services;

namespace HW.ReportGenerator.Tests
{
	[TestFixture]
	public class ProjectsTests
	{
		Projects v = new Projects();
		ProjectService s = new ProjectService(new ProjectRepositoryStub());
		
		[Test]
		public void TestMethod()
		{
			v.SetProjects(s.FindAllProjects());
		}
	}
}
