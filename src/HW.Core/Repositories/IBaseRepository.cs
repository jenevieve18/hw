using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IBaseRepository<T>
	{
		void Save(T t);
		
		void Update(T t, int id);
		
		void SaveOrUpdate(T t);
		
		void SaveOrUpdate<U>(U u);
		
		void Delete(T t);
		
		void Delete<U>(U t);
		
		void Delete(int id);
		
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
		
		public void Save(T t)
		{
		}
		
		public void Update(T t, int id)
		{
		}
		
		public void SaveOrUpdate(T t)
		{
			data.Add(t);
		}
		
		public void SaveOrUpdate<U>(U t)
		{
//			data.Add(t);
		}
		
		public void Delete(int id)
		{
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
		
		public void Delete<U>(U t)
		{
			throw new NotImplementedException();
		}
	}
}
