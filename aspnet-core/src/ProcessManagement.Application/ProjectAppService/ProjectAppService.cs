using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AlManagerslar;
using ProcessManagement.ManagerAppService.Dto;
using ProcessManagement.Managers;
using ProcessManagement.Project.Dto;
using ProcessManagement.Projects;
using System;
using System.Collections.Generic;
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
            try
            {
                var entity = _mapper.Map(input);
                var projeId = await _repository.InsertAndGetIdAsync(entity);
                if (input.ManagerId > 0)
                {

                    var manager = await _managerRepository.GetAll().Include(p=>p.Projects).FirstOrDefaultAsync(x=>x.Id==(int)input.ManagerId);
                    manager.Projects.Add(entity);
                    await _managerRepository.UpdateAsync(manager);


                }
            }
            catch (Exception ex)
            {

            }
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
    }
}
