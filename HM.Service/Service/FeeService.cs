using HM.Data.Repository;
using HM.Model.Model;
using HM.Model.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HM.Service.Service
{
    public interface IFeeService
    {
        FeeRequestModel Add(FeeRequestModel newItem);
        FeeRequestModel Update(FeeRequestModel newItem);
        void Delete(int Id);
        IEnumerable<FeeRequestModel> GetAll();
        FeeRequestModel Get(int Id);
    }
    public class FeeService : IFeeService
    {
        private IFeeRepository _feeRepository;
        public FeeService(IFeeRepository feeRepository)
        {
            _feeRepository = feeRepository;
        }
        public FeeRequestModel Add(FeeRequestModel newItem)
        {
            var result = this._feeRepository.Add(new Fee
            {
                Name = newItem.Name,
                Price = newItem.Price,
                Unit = newItem.Unit,
                Status = 1,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });
            this._feeRepository.Commit();
            if (result == null)
            {
                return null;
            }
            return newItem;
        }

        public void Delete(int Id)
        {
            var result = this._feeRepository.GetSingle(s => s.Id == Id, new string[] { "RoomFees" });
            if (result == null)
            {
                throw new Exception("Có lỗi xảy ra vui lòng thử lại.");
            }
            if (result.RoomFees.Count > 0)
            {
                throw new Exception("Chi phí đang được kết nối với phòng. Không thể xoá");
            }
            this._feeRepository.Delete(result);
        }

        public FeeRequestModel Get(int Id)
        {
            var result = this._feeRepository.GetSingle(s => s.Id == Id);
            return new FeeRequestModel
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                Unit = result.Unit,
                Status = result.Status
            };
        }

        public IEnumerable<FeeRequestModel> GetAll()
        {
            return this._feeRepository.GetAll().Select(s => new FeeRequestModel
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                Unit = s.Unit,
                Status = s.Status
            });
        }

        public FeeRequestModel Update(FeeRequestModel newItem)
        {
            var result = this._feeRepository.GetSingle(s => s.Id == newItem.Id);
            result.Name = newItem.Name;
            result.Price = newItem.Price;
            result.Unit = newItem.Unit;
            result.Status = newItem.Status;
            this._feeRepository.Update(result);
            this._feeRepository.Commit();
            return newItem;
        }
    }
}
