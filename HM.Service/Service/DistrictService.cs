using HM.Data.Repository;
using HM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HM.Service.Service
{
    public interface IDistrictService
    {
        IEnumerable<District> GetByProvinceId(int Id);
    }
    public class DistrictService : IDistrictService
    {
        private IDistrictRepository _districtRepository;
        public DistrictService(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }
        public IEnumerable<District> GetByProvinceId(int Id)
        {
            return this._districtRepository.GetMulti(s => s.ProvinceId == Id);
        }
    }
}
