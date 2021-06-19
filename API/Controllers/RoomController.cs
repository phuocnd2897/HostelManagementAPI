using HM.Model.RequestModel;
using HM.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpPost]
        public IActionResult Add(RoomRequestModel newItem)
        {
            try
            {
                var accountId = User.Identity.Name;
                var baseUrl = string.Format("{0}://{1}", Request.Scheme, Request.Host);
                var result = this._roomService.Add(newItem, accountId, Directory.GetCurrentDirectory(), baseUrl);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Có lỗi xảy ra vui lòng thử lại");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Update(RoomRequestModel newItem)
        {
            try
            {
                var accountId = User.Identity.Name;
                var baseUrl = string.Format("{0}://{1}", Request.Scheme, Request.Host);
                var result = this._roomService.Update(newItem, accountId, Directory.GetCurrentDirectory(), baseUrl);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Có lỗi xảy ra vui lòng thử lại");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete(string Id)
        {
            try
            {
                this._roomService.Delete(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = this._roomService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Get(string Id)
        {
            try
            {
                var result = this._roomService.Get(Id);
                if (result == null)
                {
                    return BadRequest("Có lỗi xảy ra vui lòng thử lại");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
