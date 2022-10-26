using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Authorization.Users;
using ProcessManagement.CustomerAppService.CustomerDtos;
using ProcessManagement.Customers;
using ProcessManagement.Developers;
using ProcessManagement.Enums;
using ProcessManagement.Manager.Dto;
using ProcessManagement.ManagerAppService.Dto;
using ProcessManagement.Missions.Dto;
using ProcessManagement.Project.Dto;
using ProcessManagement.Projects;
using ProcessManagement.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.ManagerAppService
{
    public class ManagerAppService : IMangerAppService
    {
        private readonly UserCreateManager _userCreateManager;
        private readonly IRepository<Managers.Manager> _repository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Developer> _developerRepository;
        private readonly IRepository<Missions.Mission> _missionRepository;
        private readonly IRepository<Missions.Commit> _commitRepository;
        private readonly IRepository<Professionlar.Profession> _professionRepository;
        private readonly CustomMapperManager _mapper;
        private readonly IRepository<Projects.Project> _projectRepository;
        MissionManager _missionManager;

        public ManagerAppService(
            IRepository<User, long> userRepository,
            IRepository<Managers.Manager> repository,
            UserCreateManager userCreateManager,
            IRepository<Professionlar.Profession> professionRepository,
            CustomMapperManager mapper,
            IRepository<Projects.Project> projectRepository,
            IRepository<Developer> developerRepository,
            IRepository<Missions.Mission> missionRepository,
            IRepository<Missions.Commit> commitRepository,
            MissionManager missionManager)
        {
            _userRepository = userRepository;
            _repository = repository;
            _userCreateManager = userCreateManager;
            _professionRepository = professionRepository;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _developerRepository = developerRepository;
            _missionRepository = missionRepository;
            _commitRepository = commitRepository;
            _missionManager = missionManager;
        }
        public async Task Create(ManagerDto input)
        {

            var newUserId = _userCreateManager.CreateUser("Manager", input.CreateUserDto);
            var newUser = _userRepository.GetAsync(newUserId.Result).Result;
            if (newUser == null)
            {
                throw new UserFriendlyException("Sistem tarafindfaan hata oluştu");
            }
            var newManager = new Managers.Manager
            {
                User = newUser,
            };
            await _repository.InsertAsync(newManager);
        }
        public async Task Update(int id, GetManagerDto input)
        {
            var customerentity = _repository.GetAll().Include(q => q.User).Include(q => q.Projects).Where(a => a.Id == id).FirstOrDefault();
            await _userCreateManager.UpdateUser(customerentity.User, input.UserDto);
        }
        public async Task<List<GetManagerDto>> GetList()
        {

            var entityList = await _repository.GetAll()
                .Include(q => q.User)
                .Include(p => p.Projects).ThenInclude(x => x.Customer)
                .Include(p => p.Projects).ThenInclude(x => x.Manager)
                .ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetManagerDto>> GetPaginatedList(int pageSize = 10, int pageNumber = 1)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _repository.GetAll()
                .Include(q => q.User)
                .Include(p => p.Projects).ThenInclude(x => x.Customer)
                .Include(p => p.Projects).ThenInclude(x => x.Manager)
                .Skip((int)PageShow).Take((int)PageSize)
                .ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<GetManagerDto> GetById(int id)
        {
            var entity = await _repository.GetAll().Where(q => q.Id == id)
                .Include(q => q.User)
                .Include(p => p.Projects).ThenInclude(x => x.Customer)
                .Include(p => p.Projects).ThenInclude(x => x.Manager)
                .FirstOrDefaultAsync();

            return _mapper.Map(entity);
        }
        public async Task Delete(int id)
        {
            var entity = await _repository.GetAll().Include(q => q.User).Where(a => a.Id == id).FirstOrDefaultAsync();
            await _userRepository.DeleteAsync(entity.User.Id);
            await _repository.DeleteAsync(id);
        }
        public async Task CreateProject(CreateProjectDto input) => await _projectRepository.InsertAsync(_mapper.Map(input));
        public async Task<List<GetProjectDto>> GetMyProject(long managerId, int pageSize = 10, int pageNumber = 1)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var projects = await _projectRepository.GetAll()
                .Include(q => q.Manager).ThenInclude(q => q.User)
                .Include(q => q.Customer).ThenInclude(q => q.User)
                .Include(q => q.Developers).ThenInclude(q => q.User)
                .Include(q => q.Missions)
                .Skip((int)PageShow).Take((int)PageSize)
                .Where(q => q.Manager.Id == managerId).ToListAsync();
            return projects.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task UpdateProject(GetProjectDto input)
        {
            var project = await _projectRepository.GetAsync(input.Id);
            project.Name = input.Name;
            project.Status = input.Status;
            project.EndTime = input.EndTime;
            await _projectRepository.UpdateAsync(project);
        }
        public async Task UpdateProjectStatus(int id, StatusProject status)
        {
            var entity = _projectRepository.Get(id);
            entity.Status = status;
            await _projectRepository.UpdateAsync(entity);
        }
        public async Task UpdateProjectManager(int projectId,int newManagerId)
        {
            var entity = _projectRepository.Get(projectId);
            entity.ManagerId = newManagerId;
            await _projectRepository.UpdateAsync(entity);
        }
        public async Task GiveDeveloperAcsesToTheProject(int projeId, int devId)
        {
            var project = _projectRepository.Get(projeId);
            var developer = _developerRepository.Get(devId);
            project.Developers.Add(developer);
            developer.Projects.Add(project);
            _developerRepository.Update(developer);
            _projectRepository.Update(project);
        }
        public async Task RemoveDeveloperAcsesToTheProject(int projeId, int devId)
        {
            var project = _projectRepository.Get(projeId);
            var developer = _developerRepository.Get(devId);
            project.Developers.Remove(developer);
            developer.Projects.Remove(project);
            _developerRepository.Update(developer);
            _projectRepository.Update(project);
        }
        public async Task DeleteProject(int projetId) => await _projectRepository.DeleteAsync(projetId);
        public async Task CreateMission(CreateMissionDto input) => await _missionManager.CreateMission(input);
        public async Task<List<GetMissionDto>> GetMyMission(long managerId, int pageSize = 10, int pageNumber = 1)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var list = await _missionRepository.GetAll()
                .Include(q => q.Commits)
                .Where(q => q.Projects.ManagerId == managerId)
                .Skip((int)PageShow).Take((int)PageSize)
                .ToListAsync();
            return list.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task UpdateMyMission(GetMissionDto input) => await _missionManager.UpdateMission(input);
        public async Task DeleteMyMission(int missionId) => await _missionManager.DeleteMission(missionId);
        public async Task CreateCommit(GetCommitDto input) => await _missionManager.CreateCommit(input);
        public async Task<List<GetCommitDto>> GetListMissionCommits(int missionId, int pageNumber = 1, int pageSize = 10)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var list = await _commitRepository.GetAll().Where(q => q.MissionId == missionId).ToListAsync();
            return list.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task UpdateCommit(GetCommitDto input) => await _missionManager.UpdateCommit(input);
        public async Task DeleteCommit(int commitId) => await _missionManager.DeleteCommit(commitId);
    }
}
