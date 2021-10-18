using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using NgTemplate.API.DTOs;
using NgTemplate.API.DTOs.Enums;
using NgTemplate.API.Repositories;

namespace NgTemplate.API.Services
{
    public class DemoService : IDemoService
    {
        private readonly IDemoRepository _repository;

        public DemoService(IDemoRepository repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponse> AddDemo(Demo demo)
        {
            var result = await _repository.AddAsync(demo.Adapt<Entities.Demo>());
            if (result is { })
            {
                return new BaseResponse { Data = result.Adapt<Demo>() };
            }

            return new BaseResponse { Message = "Creating a new resource failed.", InternalError = true };
        }
        public async Task<ResourceOperationResult> DeleteDemo(int id) => await _repository.DeleteAsync(id);

        public async Task<BaseResponse> GetAllDemos()
        {
            var result = await _repository.GetAllAsync();
            if (result is { })
            {
                return new BaseResponse { Data = result.Adapt<IEnumerable<Demo>>() };
            }

            return new BaseResponse { Message = "Fetch failed.", InternalError = true };
        }

        public async Task<BaseResponse> GetDemo(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result is { })
            {
                return new BaseResponse { Data = result.Adapt<Demo>() };
            }

            return new BaseResponse { Message = "No resource found.", InternalError = true };
        }

        public async Task<BaseResponse> UpdateDemo(Demo demo)
        {
            var result = await _repository.UpdateAsync(demo.Adapt<Entities.Demo>());
            if (result is { })
            {
                return new BaseResponse { Data = result.Adapt<Demo>() };
            }

            return new BaseResponse { Message = "Updating the resource failed." };
        }
    }
}