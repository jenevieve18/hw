using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HW.EForm.Core.Repositories
{
	public interface IBaseRepository<T>
	{
		void Save(T t);
		void Update(T t, int id);
		void Delete(int id);
		T Read(int id);
		IList<T> FindAll();
	}
	
	public class BaseRepositoryStub<T> : IBaseRepository<T>
	{
		protected List<T> data = new List<T>();
		
		public void Save(T t)
		{
		}
		
		public void Update(T t, int id)
		{
		}
		
		public void Delete(int id)
		{
		}
		
		public T Read(int id)
		{
			throw new NotImplementedException();
		}
		
		public IList<T> FindAll()
		{
			return data;
		}
	}
}
