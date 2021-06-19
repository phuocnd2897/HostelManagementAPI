using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface IHostelRepository : IRepository<Hostel, string>
    {

    }
    public class HostelRepository : RepositoryBase<Hostel, string>, IHostelRepository
    {
        public HostelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
