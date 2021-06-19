using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface ICustomerRepository : IRepository<Customer, string>
    {

    }
    public class CustomerRepository : RepositoryBase<Customer, string>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
