using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface IWardRepository : IRepository<Ward, int>
    {

    }
    public class WardRepository : RepositoryBase<Ward, int>, IWardRepository
    {
        public WardRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
