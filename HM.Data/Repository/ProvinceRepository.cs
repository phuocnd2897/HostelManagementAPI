using HM.Context;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using VG.Context;

namespace HM.Data.Repository
{
    public interface IProvinceRepository : IRepository<Province, int>
    {

    }
    public class ProvinceRepository : RepositoryBase<Province, int>, IProvinceRepository
    {
        public ProvinceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
