using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;

namespace HW.Core.Repositories.NHibernate
{
	public class BaseNHibernateRepository<T> : IBaseRepository<T>
	{
		public BaseNHibernateRepository()
		{
		}
		
		public virtual void SaveOrUpdate(T t)
		{
			ITransaction trans = null;
			try {
				ISession session = NHibernateHelper.OpenSession();
				trans = session.BeginTransaction();
				session.SaveOrUpdate(t);
				session.Flush();
				trans.Commit();
			} catch (Exception ex) {
				trans.Rollback();
				throw ex;
			}
		}
		
		public virtual void SaveOrUpdate<U>(U t)
		{
			SaveOrUpdate<U>(t, "eForm");
		}
		
		public virtual void SaveOrUpdate<U>(U t, string sessionName)
		{
			ITransaction trans = null;
			try {
				ISession session = NHibernateHelper.OpenSession(sessionName);
				trans = session.BeginTransaction();
				session.SaveOrUpdate(t);
				session.Flush();
				trans.Commit();
			} catch (Exception ex) {
				trans.Rollback();
				throw ex;
			}
		}
		
		public virtual void Delete(T t)
		{
			ISession session = NHibernateHelper.OpenSession();
			session.Delete(t);
			session.Flush();
		}
		
		public virtual void Delete<U>(U t)
		{
			ISession session = NHibernateHelper.OpenSession();
			session.Delete(t);
			session.Flush();
		}
		
		public virtual T Read(int id)
		{
			return Read(id, "eForm");
		}
		
		public virtual T Read(int id, string sessionName)
		{
			return NHibernateHelper.OpenSession(sessionName).Load<T>(id);
		}
		
		public virtual U Read<U>(int id)
		{
			return Read<U>(id, "eForm");
		}
		
		public virtual U Read<U>(int id, string sessionName)
		{
			return NHibernateHelper.OpenSession(sessionName).Load<U>(id);
		}
		
		public IList<U> FindAll<U>()
		{
			return FindAll<U>("eForm");
		}
		
		public virtual IList<U> FindAll<U>(string sessionName)
		{
			return NHibernateHelper.OpenSession(sessionName).CreateCriteria(typeof(U)).List<U>();
		}
		
		public virtual IList<T> FindAll()
		{
			return FindAll("eForm");
		}
		
		public virtual IList<T> FindAll(string sessionName)
		{
			return NHibernateHelper.OpenSession(sessionName).CreateCriteria(typeof(T)).List<T>();
		}
	}
	
	public class NHibernateHelper
	{
		static IDictionary<string, ISession> sessions;
		
		public static IDictionary<string, ISession> Sessions {
			get { return sessions; }
		}
		
		static NHibernateHelper()
		{
			sessions = new Dictionary<string, ISession>();
			
			foreach (System.Configuration.ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings) {
				if (c.Name == "eForm" || c.Name == "healthWatch") {
					var factory = new Configuration().Configure().SetProperty("connection.connection_string", c.ConnectionString).BuildSessionFactory();
					sessions.Add(c.Name, factory.OpenSession());
				}
			}
			
//			var factory = new Configuration().Configure().SetProperty("connection.connection_string", "Server=.;Database=eform;Trusted_Connection=True;").BuildSessionFactory();
//			sessions.Add("eForm", factory.OpenSession());
//			factory = new Configuration().Configure().SetProperty("connection.connection_string", "Server=.;Database=healthWatch;Trusted_Connection=True;").BuildSessionFactory();
//			sessions.Add("healthWatch", factory.OpenSession());
		}
		
		public static ISession OpenSession()
		{
			return OpenSession("eForm");
		}
		
		public static ISession OpenSession(string name)
		{
			return sessions[name];
		}
		
		public static void CloseSession()
		{
			foreach (var s in sessions.Values) {
				s.Close();
			}
		}
	}
}
