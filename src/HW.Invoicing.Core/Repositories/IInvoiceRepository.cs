using System;
using HW.Core.Repositories;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories
{
	public interface IInvoiceRepository : IBaseRepository<Invoice>
	{
	}
	
	public class InvoiceRepositoryStub : BaseRepositoryStub<Invoice>, IInvoiceRepository
	{
	}
}
