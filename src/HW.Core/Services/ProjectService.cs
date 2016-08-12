/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 8/11/2016
 * Time: 12:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class ProjectService
	{
		IProjectRepository pr;
		
		public ProjectService(IProjectRepository pr)
		{
			this.pr = pr;
		}
		
		public IList<Project> FindAllProjects()
		{
			return pr.FindAll();
		}
	}
}
