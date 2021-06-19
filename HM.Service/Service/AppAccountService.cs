using HM.Common.Helper;
using HM.Data.Repository;
using HM.Model.Model;
using HM.Model.RequestModel;
using HM.Model.ResponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HM.Service.Service
{
    public interface IAppAccountService
    {
        AppAccountResponseModel Login(string username, string password);
        AppAccountResponseModel RegisterAccount(AppAccountRegisterRequestModel newItem, string savePath, string url);
        AppAccountResponseModel UpdateAccount(AppAccountRegisterRequestModel newItem, string accountId);
        void ChangePassword(string oldPass, string newPass, string accountId);
        void UploadAvatar(IFormFile newItem, string savePath, string url, string accountId);
    }
    public class AppAccountService : IAppAccountService
    {
        public IAppAccountRepository _appAccountRepository;
        public IAppAccountDetailRepository _appAccountDetailRepository;
        public AppAccountService(IAppAccountRepository appAccountRepository, IAppAccountDetailRepository appAccountDetailRepository)
        {
            _appAccountRepository = appAccountRepository;
            _appAccountDetailRepository = appAccountDetailRepository;
        }

        public void ChangePassword(string oldPass, string newPass, string accountId)
        {
            var account = this._appAccountRepository.GetSingle(s => s.Id == accountId);
            if (!IdentityHelper.VerifyHashedPassword(account.Password, oldPass))
            {
                throw new Exception("Password cũ không đúng");
            }
            account.Password = IdentityHelper.HashPassword(newPass);
            this._appAccountRepository.Update(account);
            this._appAccountRepository.Commit();
        }

        public AppAccountResponseModel Login(string username, string password)
        {
            var account = this._appAccountRepository.GetSingle(s => s.Username == username, new string[] { "AppAccountDetails" });
            if (account == null || !IdentityHelper.VerifyHashedPassword(account.Password, password))
                return null;
            var result = new AppAccountResponseModel
            {
                Id = account.Id,
                FullName = account.AppAccountDetails.FirstOrDefault().FullName
            };
            return result;
        }

        public AppAccountResponseModel RegisterAccount(AppAccountRegisterRequestModel newItem, string savePath, string url)
        {
            string imageName = "";
            var account = this._appAccountRepository.GetSingle(s => s.Username == newItem.Username);
            if (account != null)
                throw new Exception("Tên tài khoản đã được đăng kí. Vui lòng thử lại số khác");
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
            account = this._appAccountRepository.Add(new AppAccount
            {
                Username = newItem.Username,
                Password = IdentityHelper.HashPassword(newItem.Password),
            });
            var accountDetail = this._appAccountDetailRepository.Add(new AppAccountDetail
            {
                AccountId = account.Id,
                FullName = newItem.FullName,
                BirthDate = newItem.BirthDate,
                Sex = newItem.Sex,
                WardId = newItem.WardId,
                Address = newItem.Address,
                Email = newItem.Email,
                Avatar = imageName != "" ? url + "/Avatar/" + imageName : "",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });
            this._appAccountDetailRepository.Commit();
            return new AppAccountResponseModel 
            {
                Id = account.Id,
                FullName = accountDetail.FullName,
            };
        }

        public AppAccountResponseModel UpdateAccount(AppAccountRegisterRequestModel newItem, string accountId)
        {
            var account = this._appAccountRepository.GetSingle(s => s.Id == accountId, new string[] { "AppAccountDetails" });
            if (account == null)
            {
                throw new Exception("Có lỗi xảy ra vui lòng thử lại.");
            }
            var accountDetail = account.AppAccountDetails.FirstOrDefault();
            accountDetail.FullName = newItem.FullName;
            accountDetail.BirthDate = newItem.BirthDate;
            accountDetail.Sex = newItem.Sex;
            accountDetail.WardId = newItem.WardId;
            accountDetail.Address = newItem.Address;
            accountDetail.Email = newItem.Email;
            accountDetail.UpdatedDate = DateTime.Now;
            this._appAccountDetailRepository.Update(accountDetail);
            this._appAccountDetailRepository.Commit();
            return new AppAccountResponseModel
            {
                Id = account.Id,
                FullName = accountDetail.FullName,
            };
        }

        public void UploadAvatar(IFormFile newItem, string savePath, string url, string accountId)
        {
            string fileName = "";
            var account = this._appAccountRepository.GetSingle(s => s.Id == accountId, new string[] { "AppAccountDetails" });
            if (newItem != null)
            {
                Uri uri = new Uri(account.AppAccountDetails.FirstOrDefault().Avatar);
                if (uri.IsFile)
                {
                    fileName = System.IO.Path.GetFileName(uri.LocalPath);
                }
                var path = Path.Combine(savePath, "wwwroot/Avatar", fileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string imageName = new string(Path.GetFileNameWithoutExtension(newItem.FileName).Take(10).ToArray()).Replace(' ', '-');
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(newItem.FileName);
                var imagePath = Path.Combine(savePath, "wwwroot/Avatar", imageName);
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    newItem.CopyTo(fileStream);
                    fileStream.Flush();
                }
                account.AppAccountDetails.FirstOrDefault().Avatar = url + "/Avatar/" + imageName;
            }
            this._appAccountDetailRepository.Update(account.AppAccountDetails.FirstOrDefault());
            this._appAccountDetailRepository.Commit();
        }
    }
}
