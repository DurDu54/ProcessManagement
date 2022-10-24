using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Authorization.Users;
using ProcessManagement.CustomerAppService.CustomerDtos;
using ProcessManagement.Customers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessManagement.CustomerAppService
{
    public class CustomerAppService : IApplicationService
    {
        private readonly UserManager _userManager;
        private readonly UserCreateManager _userCreateManager;
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<User, long> _userRepository;
        private readonly CustomMapperManager _mapper;
        public CustomerAppService(
            UserManager userManager,
            IRepository<User, long> userRepository,
            IRepository<Customer> repository,
            UserCreateManager userCreateManager,
            CustomMapperManager mapper
            )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _repository = repository;
            _userCreateManager = userCreateManager;
            _mapper = mapper;
        }


        /// <summary>
        /// RoleNamese boşta olsa değer yolla yoksa patlarsın
        /// </summary>
        public async Task Create(CustomerDto input)
        {

            var newUser = _userCreateManager.CreateUser("Customer",input.CreateUserDto);
            if (newUser==null)
            {
                throw new UserFriendlyException("Sistem tarafindfaan hata oluştu");
            }
            var newCustomer = new Customer
            {
                User = newUser.Result,
            };
            await _repository.InsertAsync(newCustomer);
        }
        public async Task Update(int id ,GetCustomerDto input)
        {
            var customerentity = _repository.GetAll().Include(q => q.User).Where(a => a.Id == id).FirstOrDefault();
            await _userCreateManager.UpdateUser(customerentity.User, input.UserDto);
        }
        public async Task<List<GetCustomerDto>> GetList()
        {
            var entityList = await _repository.GetAll().Include(q => q.User).ToListAsync();
            return entityList.Select(q =>_mapper.Map(q) ).ToList();
        }
        public async Task<List<GetCustomerDto>> GetPaginatedList(int pageSize , int pageNumber)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _repository.GetAll().Include(q => q.User).Skip((int)PageShow).Take((int)PageSize).ToListAsync();
            return entityList.Select(q => new GetCustomerDto
            {
                Id = q.Id,
                UserDto = _mapper.Map(q.User),
            }).ToList();
        }
        public async Task<GetCustomerDto> GetById(int id)
        {
            var entity = await _repository.GetAll().Where(q=>q.Id==id).Include(q => q.User).FirstOrDefaultAsync();
            var Dto = new GetCustomerDto();
            Dto.Id = entity.Id;
            Dto.UserDto= _mapper.Map(entity.User);
            return Dto;
        }
        public async Task Delete(int id)
        {
            var entity = await _repository.GetAll().Include(q=>q.User).Where(a=>a.Id==id).FirstOrDefaultAsync();
            await _userRepository.DeleteAsync(entity.User.Id);
            await _repository.DeleteAsync(id);
        }

    }
}
