using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Authorization.Users;
using ProcessManagement.Developers;
using ProcessManagement.Deveoper.Dto;
using ProcessManagement.Enums;
using ProcessManagement.Missions.Dto;
using ProcessManagement.Project.Dto;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessManagement.Deveoper
{
    public class DeveloperAppService : IDeveloperAppService
    {
        private readonly UserCreateManager _userCreateManager;
        private readonly MissionManager _missionManager;

        private readonly IRepository<Developer> _repository;
        private readonly IRepository<Missions.Mission> _missionRepository;
        private readonly IRepository<Missions.Commit> _commitRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Professionlar.Profession> _professionRepository;
        private readonly CustomMapperManager _mapper;
        private readonly IRepository<Projects.Project> _projectRepository;

        public DeveloperAppService(
            IRepository<User, long> userRepository,
            IRepository<Developer> repository,
            UserCreateManager userCreateManager,
            IRepository<Professionlar.Profession> professionRepository,
            CustomMapperManager mapper,
            IRepository<Projects.Project> projectRepository,
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
            _missionRepository = missionRepository;
            _commitRepository = commitRepository;
            _missionManager = missionManager;
        }
        /// <summary>
        /// RoleNamese boşta olsa değer yolla yoksa patlarsın
        /// </summary>
        public async Task Create(DeveloperDto input)
        {
            var prof = await _professionRepository.GetAsync(input.ProfessionId);
            var newUserId = _userCreateManager.CreateUser("Developer", input.CreateUserDto);
            var newUser = _userRepository.GetAsync(newUserId.Result).Result;
            if (newUser == null)
            {
                throw new UserFriendlyException("Sistem tarafindfaan hata oluştu");
            }
            var newDeveloper = new Developer
            {
                Id = 0,
                User = newUser,
                ProfessionId = prof.Id,
                Profession = prof,
            };
            await _repository.InsertAsync(newDeveloper);

        }
        public async Task Update(int id, GetDeveloperDto input)
        {
            var customerentity = _repository.GetAll().Include(q => q.User).Where(a => a.Id == id).FirstOrDefault();
            await _userCreateManager.UpdateUser(customerentity.User, input.UserDto);
        }
        public async Task<List<GetDeveloperDto>> GetList()
        {
            var entityList = await _repository.GetAll()
                .Include(q => q.User)
                .Include(q => q.Profession)
                .Include(q => q.Projects).ThenInclude(q=>q.Manager)
                .Include(q =>q.Missions)
                .ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetDeveloperDto>> GetPaginatedList(int pageSize = 10, int pageNumber = 1)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _repository.GetAll()
                .Include(q => q.User)
                .Include(q => q.Profession)
                .Include(q => q.Projects).ThenInclude(q => q.Manager)
                .Include(q => q.Missions)
                .Skip((int)PageShow).Take((int)PageSize)
                .ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<GetDeveloperDto> GetById(int id)
        {
            var entity = await _repository.GetAll()
                .Where(q => q.Id == id)
                .Include(q => q.User)
                .Include(q => q.Profession)
                .Include(q => q.Projects).ThenInclude(q => q.Manager)
                .Include(q => q.Missions)
                .FirstOrDefaultAsync();

            return _mapper.Map(entity);
        }
        public async Task Delete(int id)
        {
            var entity = await _repository.GetAll().Include(q => q.User).Where(a => a.Id == id).FirstOrDefaultAsync();
            await _userRepository.DeleteAsync(entity.User.Id);
            await _repository.DeleteAsync(id);
        }
        public async Task<List<GetProjectDto>> GetMyProject(long devId, int pageSize = 10, int pageNumber = 1)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var projects = await _projectRepository.GetAll()
                .Include(q => q.Manager).ThenInclude(q => q.User)
                .Include(q => q.Customer).ThenInclude(q => q.User)
                .Include(q => q.Developers).ThenInclude(q => q.User)
                .Include(q => q.Missions)
                .Skip((int)PageShow).Take((int)PageSize)
                .Where(q => q.Developers.Select(a => a.Id == devId).FirstOrDefault()).ToListAsync();
            return projects.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task UpdateProJectStatus(int id, StatusProject status) 
        {
            var entity = _projectRepository.Get(id);
            entity.Status = status;
            await _projectRepository.UpdateAsync(entity);
        }
        public async Task<List<GetMissionDto>> GetMyMissions(int devId, int pageSize = 10, int pageNumber = 1)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var list=  await _missionRepository.GetAll()
                .Include(q=>q.Commits)
                .Where(q => q.DeveloperId == devId)
                .Skip((int)PageShow).Take((int)PageSize)
                .ToListAsync();
            return list.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetDeveloperDto>> GetDeveloperByProject(int projectid)
        {
            var entity = await _projectRepository.GetAll().Where(q=>q.Id == projectid).Include(u=>u.Developers).FirstOrDefaultAsync();
            return  entity.Developers.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task UpdateMissionStatus(int id , StatusMission status)
        {
            var entity = _missionRepository.Get(id);
            entity.Status = status;
            await _missionRepository.UpdateAsync(entity);
        }
        public async Task CreateCommit(GetCommitDto input) => await _missionManager.CreateCommit(input);
        public async Task<List<GetCommitDto>> GetListMissionCommits(int missionId, int pageNumber=1, int pageSize=10)
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
