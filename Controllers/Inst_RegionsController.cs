using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMIS_API.Models;

namespace HRMIS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Inst_RegionsController : ControllerBase
    {
        private readonly IInst_RegionRepository _context;
        
        CommonMessages _msgs = new CommonMessages();

        public Inst_RegionsController(IInst_RegionRepository context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> CreateRegion(Inst_Region data)
        {
            try
            {
                if (data == null)
                {
                    ModelState.AddModelError(_msgs._title_message, _msgs._message_id_mismatch);
                    return BadRequest(ModelState);
                }

                var reg = await _context.GetRegionByTitle(data.title);

                if (reg.data != null)
                {
                    ModelState.AddModelError(_msgs._title_message, data.title + " " + _msgs._message_record_already_exist);
                    return BadRequest(ModelState);
                }

                var createdRegion = await _context.CreateRegion(data);

                return CreatedAtAction(nameof(GetRegionByID), new { id = createdRegion.data.id }, createdRegion);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, _msgs._message_something_went_wrong);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BaseResponse>> DeleteRegion(int id, Inst_Region data)
        {
            try
            {
                if ((data == null) || (id != data.id))
                {
                    ModelState.AddModelError(_msgs._title_message, _msgs._message_id_mismatch + " or " + _msgs._message_bad_request);
                    return BadRequest(ModelState);
                }

                var recordToUpdate = await _context.GetRegionByID(id);

                if (recordToUpdate.data == null)
                {
                    return NotFound(_msgs._message_no_record_found + "[Ref ID : " + id + "]");
                }

                var result = await _context.DeleteRegion(data);
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, _msgs._message_something_went_wrong);
            }
        }

        [HttpGet("id/{id:int}")] 
        public async Task<ActionResult<BaseResponse>> GetRegionByID(int id)
        {
            try
            {
                var result = await _context.GetRegionByID(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, _msgs._message_something_went_wrong);
            }
        }

        [HttpGet("title/{title:alpha}")] 
        public async Task<ActionResult<BaseResponse>> GetRegionByTitle(string title, bool exactSearch=true)
        {
            try
            {
                var result = (exactSearch)? await _context.GetRegionByTitle(title) : await _context.GetRegionByTitleLike(title);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, _msgs._message_something_went_wrong);
            }
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetRegions()
        {
            try
            {
                return Ok(await _context.GetRegions());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, _msgs._message_something_went_wrong);
            }
        }

        [HttpGet("search")] 
        public async Task<ActionResult<BaseResponse>> Search(string title)
        {
            try
            {
                if (string.IsNullOrEmpty(title))
                {
                    ModelState.AddModelError(_msgs._title_message, _msgs._message_must_provide_value);
                    return BadRequest(ModelState);
                }
                var result = await _context.Search(title);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, _msgs._message_something_went_wrong);
            }
        }

        [HttpPut("{id:int}")] 
        public async Task<ActionResult<BaseResponse>> UpdateRegion(int id, Inst_Region data)
        {
            try
            {
                if ((data == null) || (id != data.id))
                {
                    ModelState.AddModelError(_msgs._title_message, _msgs._message_id_mismatch + " or " + _msgs._message_bad_request);
                    return BadRequest(ModelState);
                }

                var reg = await _context.GetRegionByTitle(data.title);

                if (reg.data != null && reg.data.id != data.id)
                {
                    ModelState.AddModelError(_msgs._title_message, data.title + " " + _msgs._message_record_already_exist);
                    return BadRequest(ModelState);
                }


                var recordToUpdate = await _context.GetRegionByID(id);

                if (recordToUpdate.data == null)
                {
                    return NotFound(_msgs._message_no_record_found + " Ref ID : " + id);
                }

                var result = await _context.UpdateRegion(data);
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, _msgs._message_something_went_wrong);
            }
        }

    }
}

