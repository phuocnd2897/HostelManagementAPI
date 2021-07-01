using HM.Common.Constant;
using HM.Data.Repository;
using HM.Model.Model;
using HM.Model.RequestModel;
using HM.Model.ResponseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HM.Service.Service
{
    public interface IRoomService
    {
        RoomRequestModel Add(RoomRequestModel newItem, string savePath, string url);
        RoomRequestModel Update(RoomRequestModel newItem, string savePath, string url);
        void Delete(string Id);
        IEnumerable<RoomResponseModel> GetByHostelId(string Id);
        RoomResponseModel Get(string Id);
    }
    public class RoomService : IRoomService
    {
        private IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public RoomRequestModel Add(RoomRequestModel newItem, string savePath, string url)
        {
            string imageName = "";
            if (newItem.Avatar != null)
            {
                imageName = new string(Path.GetFileNameWithoutExtension(newItem.Avatar.FileName).Take(10).ToArray()).Replace(' ', '-');
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(newItem.Avatar.FileName);
                var imagePath = Path.Combine(savePath, "wwwroot/Room", imageName) + ".jpg";
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    newItem.Avatar.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            var result = this._roomRepository.Add(new Room
            {
                Name = newItem.Name,
                Description = newItem.Description,
                Avatar = imageName != "" ? (url + "/Room/" + imageName) : (url + "/Room/phongtrodep.jpg"),
                Capacity = newItem.Capacity,
                NumberOfCustomer = 0,
                Price = newItem.Price,
                Status = (int)EnumStatusRoom.Vacant,
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                HostelId = newItem.HostelId,
                RoomFees = JsonConvert.DeserializeObject<int[]>(newItem.FeeIds).Select(s => new RoomFee { FeeId = s }).ToArray()
            });
            this._roomRepository.Commit();
            if (result != null)
            {
                return newItem;
            }
            return null;
        }

        public void Delete(string Id)
        {
            var result = this._roomRepository.GetSingle(s => s.Id == Id, new string[] { "Customers" });
            if (result.Customers.Where(s => s.Status == (int)EnumStatusCustomer.Stay).Count() > 0)
            {
                throw new Exception("Phòng đang có khách ở. Không thể xoá");
            }
            else if (result.Customers.Count() > 0)
            {
                result.IsActive = false;
                this._roomRepository.Update(result);
            }
            else
            {
                this._roomRepository.Delete(result);
            }
            this._roomRepository.Commit();

        }

        public RoomResponseModel Get(string Id)
        {
            var room = this._roomRepository.GetSingle(s => s.Id == Id, new string[] { "Customers" });
            var customer = room.Customers;
            return new RoomResponseModel
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                Avatar = room.Avatar,
                Capacity = room.Capacity,
                NumberOfCustomer = customer.Where(s => s.Status == (int)EnumStatusCustomer.Stay).Count(),
                Price = room.Price,
                Status = room.Status,
                HostelId = room.HostelId
            };
        }

        public IEnumerable<RoomResponseModel> GetByHostelId(string Id)
        {
            var result = this._roomRepository.GetMulti(s => s.IsActive == true && s.HostelId == Id, new string[] { "Customers", "RoomFees" })
                .Select(s => new RoomResponseModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Avatar = s.Avatar,
                    Capacity = s.Capacity,
                    NumberOfCustomer = s.Customers.Where(s => s.Status == (int)EnumStatusCustomer.Stay).Count(),
                    Price = s.Price,
                    Status = s.Status,
                    Fees = s.RoomFees.Select(s => s.FeeId).ToArray(),
                    HostelId = s.HostelId
                });
            return result;
        }

        public RoomRequestModel Update(RoomRequestModel newItem, string savePath, string url)
        {
            var result = this._roomRepository.GetSingle(s => s.Id == newItem.Id);
            if (newItem.Avatar != null)
            {
                string fileName = "";
                Uri uri = new Uri(result.Avatar);
                if (uri.IsFile)
                {
                    fileName = System.IO.Path.GetFileName(uri.LocalPath);
                }
                var path = Path.Combine(savePath, "wwwroot/Room", fileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string imageName = new string(Path.GetFileNameWithoutExtension(newItem.Avatar.FileName).Take(10).ToArray()).Replace(' ', '-');
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(newItem.Avatar.FileName);
                var imagePath = Path.Combine(savePath, "wwwroot/Room", imageName) + ".jpg";
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    newItem.Avatar.CopyTo(fileStream);
                    fileStream.Flush();
                }
                result.Avatar = imageName != "" ? (url + "/Room/" + imageName) : (url + "/Room/phongtrodep.jpg");
            }
            result.Name = newItem.Name;
            result.Description = newItem.Description;
            result.Capacity = newItem.Capacity;
            result.Price = newItem.Price;
            result.Status = newItem.Status;
            result.UpdatedDate = DateTime.Now;
            result.RoomFees = JsonConvert.DeserializeObject<int[]>(newItem.FeeIds).Select(s => new RoomFee { FeeId = s }).ToArray();
            this._roomRepository.Update(result);
            this._roomRepository.Commit();
            return newItem;
        }
    }
}
