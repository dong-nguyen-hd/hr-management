﻿using Business.Domain.Services;
using Business.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    public class DongNguyenController<Response, Insert, Update, Entity> : ControllerBase
    {
        #region Property
        private readonly IBaseService<Response, Insert, Update, Entity> _baseService;
        protected readonly ResponseMessage ResponseMessage;
        #endregion

        #region Constructor
        public DongNguyenController(IBaseService<Response, Insert, Update, Entity> baseService,
            IOptionsMonitor<ResponseMessage> responseMessage)
        {
            this._baseService = baseService;
            this.ResponseMessage = responseMessage.CurrentValue;
        }
        #endregion

        #region Action
        [NonAction]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            var result = await _baseService.GetAllAsync();

            if (!result.Success)
                return BadRequest(result);

            if (result.Resource is null)
                return NoContent();

            return Ok(result);
        }

        [NonAction]
        public virtual async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _baseService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result);

            if (result.Resource is null)
                return NoContent();

            return Ok(result);
        }

        [NonAction]
        public virtual async Task<IActionResult> CreateAsync([FromBody] Insert entity)
        {
            var result = await _baseService.InsertAsync(entity);

            if (result.Success)
                return StatusCode(201, result);

            return BadRequest(result);
        }

        [NonAction]
        public virtual async Task<IActionResult> UpdateAsync(int id, [FromBody] Update entity)
        {
            var result = await _baseService.UpdateAsync(id, entity);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [NonAction]
        public virtual async Task<IActionResult> SwapAsync([FromBody] SwapResource resource)
        {
            var result = await _baseService.SwapAsync(resource);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [NonAction]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _baseService.RemoveAsync(id);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [NonAction]
        public virtual async Task<IActionResult> DeleteRangeAsync(List<int> ids)
        {
            var result = await _baseService.RemoveRangeAsync(ids);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
        #endregion
    }
}
