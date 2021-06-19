using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface IAppAccountDetailRepository : IRepository<AppAccountDetail, int>
    {

    }
    public class AppAccountDetailRepository : RepositoryBase<AppAccountDetail, int>, IAppAccountDetailRepository
    {
        public AppAccountDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
