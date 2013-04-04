//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IBaseRepository<T>
	{
		void SaveOrUpdate(T t);
		
		void Delete(T t);
		
		T Read(int id);
		
		IList<T> FindAll();
	}
	
	public class BaseRepositoryStub<T> : IBaseRepository<T>
	{
		protected List<T> data;
		
		public BaseRepositoryStub()
		{
			data = new List<T>();
		}
		
		public void SaveOrUpdate(T t)
		{
			data.Add(t);
		}
		
		public void Delete(T t)
		{
			data.Remove(t);
		}
		
		public virtual T Read(int id)
		{
			return data[id];
		}
		
		public virtual IList<T> FindAll()
		{
			return data;
		}
	}
}
