using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRMIS_API.Models
{
    public class Inst_RegionRepository : IInst_RegionRepository
    {
        private readonly AppDbContext _context;
        
        CommonMessages _msgs = new CommonMessages();

        public Inst_RegionRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<BaseResponse> CreateRegion(Inst_Region data)
        {
            BaseResponse result;
            try
            {
                if (CheckIsNullData(data))
                {
                    return EmptyBaseResponse();
                }

                if (Inst_RegionExistsByTitle(data.title))
                {
                    result = new BaseResponse
                    {
                        data = null,
                        status = false,
                        message = _msgs._message_record_already_exist + " [REF: "+data.title+"]"
                    };
                    return result;
                }

                var resultSave = await _context.Inst_Regions.AddAsync(data);
                await _context.SaveChangesAsync();

                result = new BaseResponse
                {
                    data = resultSave.Entity,
                    status = true,
                    message = _msgs._message_success
                };
            }
            catch (Exception)
            {
                return EmptyBaseResponse();
            }
            return result;

        }
        public async Task<BaseResponse> DeleteRegion(Inst_Region data)
        {
            if (CheckIsNullData(data))
            {
                return EmptyBaseResponse();
            }

            BaseResponse result;
            try
            {
                var resultUpdate = await _context.Inst_Regions.FirstOrDefaultAsync(e => e.id == data.id);

                if (resultUpdate != null)
                {
                    resultUpdate.updated_user_id = data.updated_user_id;
                    resultUpdate.status = _msgs.delete_status_code;
                    resultUpdate.is_active = false;
                    resultUpdate.updated_date = DateTime.Now;

                    await _context.SaveChangesAsync();
                }

                result = new BaseResponse
                {
                    data = resultUpdate ?? null,
                    status = (resultUpdate == null) ? false : true,
                    message = (resultUpdate == null) ? _msgs._message_something_went_wrong : _msgs._message_success,
                };
            }
            catch (Exception)
            {
                return EmptyBaseResponse();
            }
            return result;

        }
        public async Task<BaseResponse> GetRegionByID(int id)
        {
            BaseResponse result;
            try
            {
                var qry = await _context.Inst_Regions.FirstOrDefaultAsync(e => e.id == id);
                result = new BaseResponse
                {
                    data = qry ?? null,
                    totalrecords = (qry == null) ? 0 : 1,
                    status = (qry == null) ? false : true,
                    message = (qry == null) ? _msgs._message_no_record_found : _msgs._message_success
                };
            }
            catch (Exception)
            {
                return EmptyBaseResponse();
            }
            return result;
        }
        public async Task<BaseResponse> GetRegionByTitle(string title)
        {
            BaseResponse result;
            try
            {
                var qry = await _context.Inst_Regions.FirstOrDefaultAsync(e => e.title == title);
                result = new BaseResponse
                {
                    data = qry ?? null,
                    totalrecords = (qry == null) ? 0 : 1,
                    status = (qry == null) ? false : true,
                    message = (qry == null) ? _msgs._message_no_record_found : _msgs._message_success
                };
            }
            catch (Exception)
            {
                return EmptyBaseResponse();
            }
            return result;

        }
        public async Task<BaseResponse> GetRegionByTitleLike(string title)
        {
            BaseResponse result;

            IQueryable<Inst_Region> query = _context.Inst_Regions;

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(e => e.title.Contains(title));
            }
           
            try
            {
                var qry = await query.ToListAsync();
                result = new BaseResponse
                {
                    data = qry ?? null,
                    totalrecords = (qry == null) ? 0 : 1,
                    status = (qry == null) ? false : true,
                    message = (qry == null) ? _msgs._message_no_record_found : _msgs._message_success
                };
            }
            catch (Exception)
            {
                return EmptyBaseResponse();
            }
            return result;

        }
        public async Task<BaseResponse> GetRegions()
        {
            BaseResponse result;
            try
            {
                result = new BaseResponse
                {
                    data = await _context.Inst_Regions.ToListAsync(),
                    totalrecords = await _context.Inst_Regions.CountAsync(),
                    status = true,
                    message = _msgs._message_success
                };
            }
            catch (Exception)
            {
                return EmptyBaseResponse();
            }

            return result;
        }
        public async Task<BaseResponse> Search(string title)
        {
            return await GetRegionByTitle(title);
        }
        public async Task<BaseResponse> UpdateRegion(Inst_Region data)
        {
            if (CheckIsNullData(data))
            {
                return EmptyBaseResponse();
            }

            BaseResponse result;
            try
            {
                var resultUpdate = await _context.Inst_Regions.FirstOrDefaultAsync(e => e.id == data.id);
                if (resultUpdate != null)
                {
                    resultUpdate.title = data.title;
                    resultUpdate.updated_user_id = data.updated_user_id;
                    resultUpdate.status = data.status;
                    resultUpdate.is_active = data.is_active;
                    resultUpdate.updated_date = DateTime.Now;

                    await _context.SaveChangesAsync();
                }

                result = new BaseResponse
                {
                    data = resultUpdate ?? null,
                    status = (resultUpdate == null)? false : true,
                    message = (resultUpdate == null) ? _msgs._message_something_went_wrong : _msgs._message_success,
                };
            }
            catch (Exception)
            {
                return EmptyBaseResponse();
            }
            return result;

        }

        private BaseResponse EmptyBaseResponse()
        {
            BaseResponse result = new BaseResponse
            {
                data = string.Empty,
                totalrecords = 0,
                status = false,
                message = _msgs._message_something_went_wrong
            };
            return result;
        }
        private bool Inst_RegionExistsByTitle(string title)
        {
            return (_context.Inst_Regions?.Any(e => e.title.Contains(title))).GetValueOrDefault();
        }
        private bool CheckIsNullData(Inst_Region data)
        {
            return (data == null) ? true : false;

        }


    }
}
