using System.Threading.Tasks;
using NgTemplate.API.DTOs;
using NgTemplate.API.DTOs.Enums;

namespace NgTemplate.API.Services
{
    public interface IDemoService
    {
        Task<BaseResponse> AddDemo(Demo demo);
        Task<BaseResponse> UpdateDemo(Demo demo);

        Task<BaseResponse> GetAllDemos();
        Task<BaseResponse> GetDemo(int id);
        Task<ResourceOperationResult> DeleteDemo(int id);
    }
}