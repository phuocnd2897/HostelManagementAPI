using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface IFeeRepository : IRepository<Fee, int>
    {

    }
    public class FeeRepository : RepositoryBase<Fee, int>, IFeeRepository
    {
        public FeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
