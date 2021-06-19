using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface IRoomFeeRepository : IRepository<RoomFee, int>
    {

    }
    public class RoomFeeRepository : RepositoryBase<RoomFee, int>, IRoomFeeRepository
    {
        public RoomFeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
