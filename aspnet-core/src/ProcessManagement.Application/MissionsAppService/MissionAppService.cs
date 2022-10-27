using Abp.Application.Services;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Configuration.Dto;
using ProcessManagement.Enums;
using ProcessManagement.Missions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.MissionsAppService
{
    public interface IMissionAppService : IApplicationService { }
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
        public async Task<List<GetMissionDto>> PaginatedListMissionByProject(int pageNumber, int pageSize, int projectid)
        {
            return await _manager.PaginatedListMissionByProject(pageNumber, pageSize, projectid);   
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
          public async Task<List<EnumListDto>> StatusListStr()
        {
            var enum1 = new EnumConvertDto() { Id = (int)StatusMission.ToDo, Text = "Yapilacak" };
            var enum2 = new EnumConvertDto() { Id = (int)StatusMission.InProgress, Text = "Geliştirme Aşamasında" };
            var enum3 = new EnumConvertDto() { Id = (int)StatusMission.CodeReview, Text = "Test Aşamasında" };
            var enum4 = new EnumConvertDto() { Id = (int)StatusMission.Done, Text = "Tamamlandı" };

            var enumlist = new List<EnumConvertDto>();
            enumlist.Add(enum1);
            enumlist.Add(enum2);
            enumlist.Add(enum3);
            enumlist.Add(enum4);

            return enumlist.Select(e => new EnumListDto
            {
                Id = e.Id,
                Text = e.Text,
            }).ToList();
        }
        public async Task DeleteCommit(int id)
        {
            await _manager.DeleteCommit(id);
        }
    }
}
