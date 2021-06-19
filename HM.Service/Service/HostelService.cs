using HM.Common.Constant;
using HM.Data.Repository;
using HM.Model.Model;
using HM.Model.RequestModel;
using HM.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HM.Service.Service
{
    public interface IHostelService
    {
        HostelRequestModel Add(HostelRequestModel newItem, string accountId, string savePath, string url);
        HostelRequestModel Update(HostelRequestModel newItem, string accountId, string savePath, string url);
        void Delete(string Id);
        IEnumerable<HostelResponseModel> GetAll();
        HostelResponseModel Get(string Id);
    }
    public class HostelService : IHostelService
    {
        private IHostelRepository _hostelRepository;
        public HostelService(IHostelRepository hostelRepository)
        {
            _hostelRepository = hostelRepository;
        }
        public HostelRequestModel Add(HostelRequestModel newItem, string accountId, string savePath, string url)
        {
            string imageName = "";
            if (newItem.Avatar != null)
            {
                imageName = new string(Path.GetFileNameWithoutExtension(newItem.Avatar.FileName).Take(10).ToArray()).Replace(' ', '-');
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(newItem.Avatar.FileName);
                var imagePath = Path.Combine(savePath, "wwwroot/Avatar", imageName);
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    newItem.Avatar.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            var result = this._hostelRepository.Add(new Hostel
            {
                Name = newItem.Name,
                WardId = newItem.WardId,
                Address = newItem.Address,
                Avatar = imageName != "" ? url + "/Hostel//" + imageName : "",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Status = 1,
                Lock = false,
                AccountId = accountId
            });
            if (result != null)
            {
                return newItem;
            }
            return null;
        }

        public void Delete(string Id)
        {
            var hostel = this._hostelRepository.GetSingle(s => s.Id == Id, new string[] { "Rooms" });
            var usedRoom = hostel.Rooms.Where(s => s.Status == (int)EnumStatusRoom.Used);
            if (usedRoom.Count() > 0)
            {
                throw new Exception("Có phòng đang sử dụng. Không thể xoá.");
            }
            this._hostelRepository.Delete(hostel);
            this._hostelRepository.Commit();
        }

        public HostelResponseModel Get(string Id)
        {
            var hostel = this._hostelRepository.GetSingle(s => s.Id == Id, new string[] { "Rooms", "Ward.District.Province" });
            var room = hostel.Rooms;
            return new HostelResponseModel
            {
                Id = hostel.Id,
                Name = hostel.Name,
                Avatar = hostel.Avatar,
                WardId = hostel.WardId,
                ProvinceName = hostel.Ward.District.Province.Name,
                DistrictName = hostel.Ward.District.Name,
                WardName = hostel.Ward.Name,
                Address = hostel.Address,
                NumberOfRoom = room.Count(),
                NumberOfEmptyRoom = room.Where(s => s.Status == (int)EnumStatusRoom.Vacant).Count(),
                NumberOfCustomer = room.Sum(s => s.Customers.Where(s => s.Status == (int)EnumStatusCustomer.Stay).Count()),
                Status = hostel.Status,
                Lock = hostel.Lock
            };
        }

        public IEnumerable<HostelResponseModel> GetAll()
        {
            var result = this._hostelRepository.GetAll(new string[] { "Rooms", "Ward.District.Province" }).Select(s => new HostelResponseModel
            {
                Id = s.Id,
                Name = s.Name,
                Avatar = s.Avatar,
                WardId = s.WardId,
                ProvinceName = s.Ward.District.Province.Name,
                DistrictName = s.Ward.District.Name,
                WardName = s.Ward.Name,
                Address = s.Address,
                NumberOfRoom = s.Rooms.Count(),
                NumberOfEmptyRoom = s.Rooms.Where(s => s.Status == (int)EnumStatusRoom.Vacant).Count(),
                NumberOfCustomer = s.Rooms.Sum(s => s.Customers.Where(s => s.Status == (int)EnumStatusCustomer.Stay).Count()),
                Status = s.Status,
                Lock = s.Lock
            });
            return result;
        }

        public HostelRequestModel Update(HostelRequestModel newItem, string accountId, string savePath, string url)
        {
            var hostel = this._hostelRepository.GetSingle(s => s.Id == newItem.Id);
            if (newItem.Avatar != null)
            {
                string fileName = "";
                Uri uri = new Uri(hostel.Avatar);
                if (uri.IsFile)
                {
                    fileName = System.IO.Path.GetFileName(uri.LocalPath);
                }
                var path = Path.Combine(savePath, "wwwroot/Avatar", fileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string imageName = new string(Path.GetFileNameWithoutExtension(newItem.Avatar.FileName).Take(10).ToArray()).Replace(' ', '-');
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(newItem.Avatar.FileName);
                var imagePath = Path.Combine(savePath, "wwwroot/Avatar", imageName);
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    newItem.Avatar.CopyTo(fileStream);
                    fileStream.Flush();
                }
                hostel.Avatar = imageName != "" ? url + "/Hostel//" + imageName : "";
            }
            hostel.Name = newItem.Name;
            hostel.WardId = newItem.WardId;
            hostel.Address = newItem.Address;
            hostel.UpdatedDate = DateTime.Now;
            hostel.Status = newItem.Status;
            hostel.Lock = newItem.Lock;
            this._hostelRepository.Update(hostel);
            this._hostelRepository.Commit();
            return newItem;
        }
    }
}
