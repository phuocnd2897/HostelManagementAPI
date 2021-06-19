using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface IDistrictRepository : IRepository<District, int>
    {

    }
    public class DistrictRepository : RepositoryBase<District, int>, IDistrictRepository
    {
        public DistrictRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
