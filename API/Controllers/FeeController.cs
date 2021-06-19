using HM.Model.RequestModel;
using HM.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        private IFeeService _feeService;
        public FeeController(IFeeService feeService)
        {
            _feeService = feeService;
        }
        [HttpPost]
        public IActionResult Add(FeeRequestModel newItem)
        {
            try
            {
                var result = this._feeService.Add(newItem);
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
        public IActionResult Update(FeeRequestModel newItem)
        {
            try
            {
                var result = this._feeService.Update(newItem);
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
        public IActionResult Delete(int Id)
        {
            try
            {
                this._feeService.Delete(Id);
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
                var result = this._feeService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Get(int Id)
        {
            try
            {
                var result = this._feeService.Get(Id);
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
