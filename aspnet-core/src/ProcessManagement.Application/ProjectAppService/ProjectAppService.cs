using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Manager.Dto;
using ProcessManagement.ManagerAppService.Dto;
using ProcessManagement.Managers;
using ProcessManagement.Project.Dto;
using ProcessManagement.Projects;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.ProjectAppService
{
    public class ProjectAppService : IProjectAppService
    {
        private readonly IRepository<Projects.Project> _repository;
        private readonly IRepository<Managers.Manager> _managerRepository;
        private readonly CustomMapperManager _mapper;
        public ProjectAppService(IRepository<Projects.Project> repository, CustomMapperManager mapper, IRepository<Managers.Manager> managerRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _managerRepository = managerRepository;
        }
        public async Task Create(CreateProjectDto input)
        {
            var entity = _mapper.Map(input);
            var projeId = await _repository.InsertAndGetIdAsync(entity);
            if (input.ManagerId > 0)
            {
                var manager = await _managerRepository.GetAll().Include(p => p.Projects).FirstOrDefaultAsync(x => x.Id == (int)input.ManagerId);
                manager.Projects.Add(entity);
                await _managerRepository.UpdateAsync(manager);
            }
        }
        public async Task Update(GetProjectDto input)
        {
            var project = await _repository.GetAsync(input.Id);
            project.Name = input.Name;
            project.Status = input.Status;
            project.EndTime = input.EndTime;
            await _repository.UpdateAsync(project);
        }
        public async Task UpdateProjectManager(int projectId, int newManagerId)
        {
            var entity = _repository.Get(projectId);
            entity.ManagerId = newManagerId;
            await _repository.UpdateAsync(entity);
        }
        public async Task<List<GetProjectDto>> List()
        {
            var entityList = await _repository.GetAll()
                .Include(q => q.Manager).ThenInclude(q => q.User)
                .Include(q => q.Customer).ThenInclude(q => q.User)
                .Include(q => q.Developers).ThenInclude(q => q.User)
                .Include(q => q.Missions).ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetProjectDto>> PaginatedList(int pageSize, int pageNumber)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _repository.GetAll()
                .Include(q => q.Manager).ThenInclude(q => q.User)
                .Include(q => q.Customer).ThenInclude(q => q.User)
                .Include(q => q.Developers).ThenInclude(q => q.User)
                .Include(q => q.Missions)
                .Skip((int)PageShow).Take((int)PageSize)
                .ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<GetProjectDto> GetById(int id)
        {
            var entity = await _repository.GetAll().Where(q => q.Id == id)
                .Include(q => q.Manager).ThenInclude(q => q.User)
                .Include(q => q.Customer).ThenInclude(q => q.User)
                .Include(q => q.Developers).ThenInclude(q => q.User)
                .Include(q => q.Missions)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new UserFriendlyException("böyle bişi yok");
            }
            return _mapper.Map(entity);
        }
        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
