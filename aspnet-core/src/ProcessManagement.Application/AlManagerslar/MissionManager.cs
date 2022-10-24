using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.Authorization.Users;
using ProcessManagement.Missions;
using ProcessManagement.Missions.Dto;
using ProcessManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoMapper.Internal.ExpressionFactory;

namespace ProcessManagement.AlManagerslar
{
    public class MissionManager : IDomainService
    {
        private readonly IRepository<Missions.Mission> _missionRepository;
        private readonly IRepository<Missions.Commit> _commitRepository;
        private readonly IRepository<User, long> _userRepo;
        private readonly CustomMapperManager _mapper;
        private readonly IAbpSession _session;
        private long _userId;
        private User _user;
        public MissionManager(IRepository<Missions.Commit> commitRepository, IRepository<Missions.Mission> missionRepository, CustomMapperManager customMapperManager, IAbpSession session, IRepository<User, long> userRepo)
        {
            _commitRepository = commitRepository;
            _missionRepository = missionRepository;
            _mapper = customMapperManager;
            _session = session;
            _userId = (long)session.UserId;
            _userRepo = userRepo;
            GetUser();
        }
        private void GetUser()
        {
            _user= _userRepo.Get(_userId);
            foreach (var item in _user.Roles)
            {
                switch (item.RoleId)
                {
                    case 1:
                        //Admin
                        break;
                    case 2:
                        //Customer
                        break;
                    case 3:
                        //Developer
                        break;
                    case 4:
                        //Manager
                        break;
                    default:
                        //Admin
                        break;
                }
            }
        }
        public async Task CreateMission(CreateMissionDto input)
        {
            var entity = new Mission();
            entity.Id = 0;
            entity.ProjectId = input.ProjectId;
            entity.Text=input.Text;
            entity.Id=input.Id;
            entity.Status=input.Status;
            entity.BeginTime=input.BeginTime;
            entity.EndTime=input.EndTime;
            entity.DeveloperId = input.DeveloperId;            
            await _missionRepository.InsertAsync(entity);
        }
        public async Task UpdateMission (GetMissionDto input)
        {
            var entity = _missionRepository.Get(input.Id);
            if (entity.CreatorUserId != _userId)
            {
                throw new UserFriendlyException("Sadece missionu oluşturan silebilir");
            }
            await _missionRepository.UpdateAsync(_mapper.Map(input));
        }
        public async Task<List<GetMissionDto>> ListMission()
        {
            var missionList = await _missionRepository.GetAll().Include(q=>q.Developers).Include(q=>q.Commits).Include(q => q.Projects).ToListAsync();
            return missionList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetMissionDto>> PaginatedListMission(int pageNumber, int pageSize)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _missionRepository.GetAll()
            .Skip((int)PageShow).Take((int)PageSize)
            .ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task DeleteMission(int id)
        {
            var entity = _missionRepository.Get(id);
            if (entity.CreatorUserId!=_userId)
            {
                throw new UserFriendlyException("Sadece missionu oluşturan silebilir");
            }
            await _missionRepository.DeleteAsync(id);
        }


        //Commit

        public async Task CreateCommit(GetCommitDto input)
        {
            await _commitRepository.InsertAsync(_mapper.Map(input));
        }
        public async Task UpdateCommit(GetCommitDto input)
        {
            var entity = _commitRepository.Get(input.Id);
            if (entity.CreatorUserId != _userId)
            {
                throw new UserFriendlyException("Sadece commiti oluşturan silebilir");
            }
            await _commitRepository.UpdateAsync(_mapper.Map(input));

        }
        public async Task<List<GetCommitDto>> ListCommit()
        {
            var missionList = await _commitRepository.GetAllListAsync();
            return missionList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetCommitDto>> PaginatedListCommit(int pageNumber, int pageSize)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _commitRepository.GetAll()
            .Skip((int)PageShow).Take((int)PageSize)
            .ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task DeleteCommit(int id)
        {
            var entity = _commitRepository.Get(id);
            if (entity.CreatorUserId != _userId)
            {
                throw new UserFriendlyException("Sadece commiti oluşturan silebilir");
            }
            await _commitRepository.DeleteAsync(id);
        }
    }
}
