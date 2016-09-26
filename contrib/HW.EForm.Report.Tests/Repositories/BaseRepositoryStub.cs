// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Repositories
{
	public class BaseRepositoryStub<T> : IBaseRepository<T>
	{
		protected List<T> items = new List<T>();
		
		public BaseRepositoryStub()
		{
		}
		
		public virtual void Save(T t)
		{
			throw new NotImplementedException();
		}
		
		public virtual void Update(T t, int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual void Delete(int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual T Read(int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual IList<T> FindAll()
		{
			return items;
		}
	}
}
