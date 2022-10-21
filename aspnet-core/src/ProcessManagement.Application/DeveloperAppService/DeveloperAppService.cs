using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Authorization.Users;
using ProcessManagement.CustomerAppService.CustomerDtos;
using ProcessManagement.Customers;
using ProcessManagement.CustomMapper;
using ProcessManagement.Developers;
using ProcessManagement.Deveoper.Dto;
using ProcessManagement.Manager.Dto;
using ProcessManagement.Managers;
using ProcessManagement.Missions;
using ProcessManagement.Missions.Dto;
using ProcessManagement.Profession.Dto;
using ProcessManagement.Professionlar;
using ProcessManagement.Project.Dto;
using ProcessManagement.Projects;
using ProcessManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessManagement.Deveoper
{
    public class DeveloperAppService : IDeveloperAppService
    {
        private readonly UserCreateManager _userCreateManager;
        private readonly IRepository<Developer> _repository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Professionlar.Profession> _professionRepository;
        private readonly CustomMapperManager _mapper;
        public DeveloperAppService(
            IRepository<User, long> userRepository,
            IRepository<Developer> repository,
            UserCreateManager userCreateManager,
            IRepository<Professionlar.Profession> professionRepository,
            CustomMapperManager mapper)
        {
            _userRepository = userRepository;
            _repository = repository;
            _userCreateManager = userCreateManager;
            _professionRepository = professionRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// RoleNamese boşta olsa değer yolla yoksa patlarsın
        /// </summary>
        public async Task Create(DeveloperDto input)
        {
            var prof = await _professionRepository.GetAsync(input.ProfessionId);
            var newUser = _userCreateManager.CreateUser("Developer", input.CreateUserDto);
            if (newUser == null)
            {
                throw new UserFriendlyException("Sistem tarafindfaan hata oluştu");
            }
            var newDeveloper = new Developer
            {
                Id = 0,
                User = newUser.Result,
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
            var entityList = await _repository.GetAll().Include(q => q.User).Include(q => q.Profession).Include(q => q.Projects).Include(q =>q.Missions).ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetDeveloperDto>> GetPaginatedList(int pageSize, int pageNumber)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _repository.GetAll().Include(q => q.User).Include(q => q.Profession).Include(q => q.Projects).Include(q => q.Missions).Skip((int)PageShow).Take((int)PageSize).ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<GetDeveloperDto> GetById(int id)
        {
            var entity = await _repository.GetAll().Where(q => q.Id == id).Include(q => q.User).Include(q => q.Profession).Include(q => q.Projects).Include(q => q.Missions).FirstOrDefaultAsync();

            return _mapper.Map(entity);
        }
        public async Task Delete(int id)
        {
            var entity = await _repository.GetAll().Include(q => q.User).Where(a => a.Id == id).FirstOrDefaultAsync();
            await _userRepository.DeleteAsync(entity.User.Id);
            await _repository.DeleteAsync(id);
        }
    }
}
