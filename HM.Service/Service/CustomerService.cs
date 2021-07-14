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
    public interface ICustomerService
    {
        CustomerRequestModel Add(CustomerRequestModel newItem, string savePath, string url);
        CustomerRequestModel Update(CustomerRequestModel newItem, string savePath, string url);
        void Delete(string Id);
        IEnumerable<CustomerResponseModel> GetByRoomId(string Id);
    }
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private IRoomRepository _roomRepository;
        public CustomerService(ICustomerRepository customerRepository, IRoomRepository roomRepository)
        {
            _customerRepository = customerRepository;
            _roomRepository = roomRepository;
        }
        public CustomerRequestModel Add(CustomerRequestModel newItem, string savePath, string url)
        {
            string imageName = "";
            if (newItem.Image != null)
            {
                imageName = new string(Path.GetFileNameWithoutExtension(newItem.Image.FileName).Take(10).ToArray()).Replace(' ', '-');
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(newItem.Image.FileName);
                var imagePath = Path.Combine(savePath, "wwwroot/Customer", imageName) + ".jpg";
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    newItem.Image.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            var result = this._customerRepository.Add(new Customer
            {
                FullName = newItem.FullName,
                BirthDate = newItem.BirthDate,
                Sex = newItem.Sex,
                PhoneNumber = newItem.PhoneNumber,
                IdCard = newItem.IdCard,
                WardId = newItem.WardId,
                Address = newItem.Address,
                Image = imageName != "" ? (url + "/Customer/" + imageName) : (url + "/Customer/sbcf-default-avatar.png"),
                DateIn = DateTime.Now,
                Status = 1,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                RoomId = newItem.RoomId,
            });
            var room = this._roomRepository.GetSingle(s => s.Id == newItem.RoomId);
            if (room.Status == (int)EnumStatusRoom.Vacant)
            {
                room.Status = (int)EnumStatusRoom.Used;
            }
            room.NumberOfCustomer += 1;
            this._roomRepository.Update(room);
            this._customerRepository.Commit();
            if (result != null)
            {
                return newItem;
            }
            return null;
        }

        public void Delete(string Id)
        {
            var customer = this._customerRepository.GetSingle(s => s.Id == Id, new string[] { "Room" });
            customer.Room.NumberOfCustomer = customer.Room.NumberOfCustomer - 1;
            if (customer.Room.NumberOfCustomer == 0)
            {
                customer.Room.Status = (int)EnumStatusRoom.Vacant;
                this._roomRepository.Update(customer.Room);
            }
            this._customerRepository.Delete(customer);
            this._customerRepository.Commit();
        }

        public IEnumerable<CustomerResponseModel> GetByRoomId(string Id)
        {
            var result = this._customerRepository.GetMulti(s => s.RoomId == Id, new string[] { "Ward.District.Province" }).Select(s => new CustomerResponseModel
            {
                Id = s.Id,
                FullName = s.FullName,
                BirthDate = s.BirthDate,
                Sex = s.Sex,
                PhoneNumber = s.PhoneNumber,
                IdCard = s.IdCard,
                ProvinceId = s.Ward.District.ProvinceId,
                DistrictId = s.Ward.DistrictId,
                WardId = s.WardId,
                ProvinceName = s.Ward.District.Province.Name,
                DistrictName = s.Ward.District.Name,
                WardName = s.Ward.Name,
                Address = s.Address,
                Image = s.Image,
                Status = s.Status,
                RoomId = s.RoomId
            });
            return result;
        }

        public CustomerRequestModel Update(CustomerRequestModel newItem, string savePath, string url)
        {
            var result = this._customerRepository.GetSingle(s => s.Id == newItem.Id);
            if (newItem.Image != null)
            {
                string fileName = "";
                Uri uri = new Uri(result.Image);
                if (uri.IsFile)
                {
                    fileName = System.IO.Path.GetFileName(uri.LocalPath);
                }
                var path = Path.Combine(savePath, "wwwroot/Customer", fileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string imageName = new string(Path.GetFileNameWithoutExtension(newItem.Image.FileName).Take(10).ToArray()).Replace(' ', '-');
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(newItem.Image.FileName);
                var imagePath = Path.Combine(savePath, "wwwroot/Customer", imageName) + ".jpg";
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    newItem.Image.CopyTo(fileStream);
                    fileStream.Flush();
                }
                result.Image = imageName != "" ? (url + "/Customer/" + imageName) : (url + "/Customer/sbcf-default-avatar.png");
            }
            result.FullName = newItem.FullName;
            result.BirthDate = newItem.BirthDate;
            result.Sex = newItem.Sex;
            result.PhoneNumber = newItem.PhoneNumber;
            result.IdCard = newItem.IdCard;
            result.WardId = newItem.WardId;
            result.Address = newItem.Address;
            result.UpdatedDate = DateTime.Now;
            this._customerRepository.Update(result);
            this._customerRepository.Commit();
            return newItem;
        }
    }
}
