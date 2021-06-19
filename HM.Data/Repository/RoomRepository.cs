using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface IRoomRepository : IRepository<Room, string>
    {

    }
    public class RoomRepository : RepositoryBase<Room, string>, IRoomRepository
    {
        public RoomRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
