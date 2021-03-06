﻿using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
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
		
		public void Delete(int id)
		{
		}
		
		public virtual T Read(int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual IList<T> FindAll()
		{
			return data;
		}
	}
}
