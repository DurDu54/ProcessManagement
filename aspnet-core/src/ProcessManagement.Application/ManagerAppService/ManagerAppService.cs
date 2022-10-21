using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Authorization.Users;
using ProcessManagement.CustomerAppService.CustomerDtos;
using ProcessManagement.Customers;
using ProcessManagement.CustomMapper;
using ProcessManagement.Developers;
using ProcessManagement.Manager.Dto;
using ProcessManagement.ManagerAppService.Dto;
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
        private readonly IRepository<Professionlar.Profession> _professionRepository;
        private readonly CustomMapperManager _mapper;
        public ManagerAppService(
            IRepository<User, long> userRepository,
            IRepository<Managers.Manager> repository,
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
        public async Task Create(ManagerDto input)
        {

            var newUser = _userCreateManager.CreateUser("Manager", input.CreateUserDto);
            if (newUser == null)
            {
                throw new UserFriendlyException("Sistem tarafindfaan hata oluştu");
            }
            var newManager = new Managers.Manager
            {
                User = newUser.Result,
            };
            await _repository.InsertAsync(newManager);
        }
        public async Task Update(int id, GetManagerDto input)
        {
            var customerentity = _repository.GetAll().Include(q => q.User).Include(q => q.Projects).Where(a => a.Id == id).FirstOrDefault();
            await _userCreateManager.UpdateUser(customerentity.User, input.User);
        }
        public async Task<List<GetManagerDto>> GetList()
        {
            var entityList = await _repository.GetAll().Include(q => q.User).ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetManagerDto>> GetPaginatedList(int pageSize, int pageNumber)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _repository.GetAll().Include(q => q.User).Skip((int)PageShow).Take((int)PageSize).ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<GetManagerDto> GetById(int id)
        {
            var entity = await _repository.GetAll().Where(q => q.Id == id).Include(q => q.User).FirstOrDefaultAsync();
            
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
