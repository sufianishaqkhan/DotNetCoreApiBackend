using HRMIS_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMIS_API.Models
{
    public interface IInst_RegionRepository
    {
        Task<BaseResponse> CreateRegion(Inst_Region region);
        Task<BaseResponse> DeleteRegion(Inst_Region region); 
        Task<BaseResponse> GetRegionByID(int id); 
        Task<BaseResponse> GetRegionByTitle(string title); 
        Task<BaseResponse> GetRegionByTitleLike(string title);
        Task<BaseResponse> GetRegions();
        Task<BaseResponse> Search(string title);
        Task<BaseResponse> UpdateRegion(Inst_Region region);
    }
}