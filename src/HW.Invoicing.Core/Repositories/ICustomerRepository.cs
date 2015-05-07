using System;
using System.Collections.Generic;
using HW.Core.Repositories;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories
{
	public interface ICustomerRepository : IBaseRepository<Customer>
	{
	}
	
	public class CustomerRepositoryStub : BaseRepositoryStub<Customer>, ICustomerRepository
	{
		public override IList<Customer> FindAll()
		{
			return new[] {
				new Customer { Name = "Customer1" },
				new Customer { Name = "Customer2" },
				new Customer { Name = "Customer3" }
			};
		}
	}
}
