using Abp.Application.Services;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Missions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.MissionsAppService
{
    public interface IMissionAppService : IApplicationService { }
    public interface ICommitAppService : IApplicationService { }
    public class MissionAppService: IMissionAppService
    {
        private readonly MissionManager _manager;
        public MissionAppService(MissionManager manager)
        {
            _manager = manager;
        }
        public async Task CreateMission(CreateMissionDto input)
        {
            await _manager.CreateMission(input);
        }
        public async Task UpdateMission(GetMissionDto input)
        {
            await _manager.UpdateMission(input);
        }
        public async Task<List<GetMissionDto>> ListMission()
        {
            return await _manager.ListMission();
        }
        public async Task<List<GetMissionDto>> PaginatedListMission(int pageNumber, int pageSize)
        {
            return await _manager.PaginatedListMission(pageNumber, pageSize);
        }
        public async Task DeleteMission(int id)
        {
            await _manager.DeleteMission(id);
        }
        public async Task CreateCommit(GetCommitDto input)
        {
            await _manager.CreateCommit(input);
        }
        public async Task UpdateCommit(GetCommitDto input)
        {
            await _manager.UpdateCommit(input); 
        }
        public async Task<List<GetCommitDto>> ListCommit()
        {
            return await _manager.ListCommit();
        }
        public async Task<List<GetCommitDto>> PaginatedListCommit(int pageNumber, int pageSize)
        {
            return await _manager.PaginatedListCommit(pageNumber, pageSize);
        }
        public async Task DeleteCommit(int id)
        {
            await _manager.DeleteCommit(id);
        }
    }
}
