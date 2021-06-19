using HM.Data.Repository;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HM.Service.Service
{
    public interface IWardService
    {
        IEnumerable<Ward> GetByDistrictId(int Id);
    }
    public class WardService : IWardService
    {
        private IWardRepository _wardRepository;
        public WardService(IWardRepository wardRepository)
        {
            _wardRepository = wardRepository;
        }
        public IEnumerable<Ward> GetByDistrictId(int Id)
        {
            return this._wardRepository.GetMulti(s => s.DistrictId == Id);
        }
    }
}
