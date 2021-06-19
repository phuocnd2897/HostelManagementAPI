using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface IAppAccountRepository : IRepository<AppAccount, string>
    {

    }
    public class AppAccountRepository : RepositoryBase<AppAccount, string>, IAppAccountRepository
    {
        public AppAccountRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
