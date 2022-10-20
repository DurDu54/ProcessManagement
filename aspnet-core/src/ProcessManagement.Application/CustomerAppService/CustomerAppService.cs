using Abp.Application.Services;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using AutoMapper;
using Castle.Windsor.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AllManagers;
using ProcessManagement.Authorization.Roles;
using ProcessManagement.Authorization.Users;
using ProcessManagement.CustomerAppService.CustomerDtos;
using ProcessManagement.Customers;
using ProcessManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.CustomerAppService
{
    public class CustomerAppService : IApplicationService
    {
        private readonly UserManager _userManager;
        private readonly UserCreateManager _userCreateManager;
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<User, long> _userRepository;
        public CustomerAppService(
            UserManager userManager,
            IRepository<User, long> userRepository,
            IRepository<Customer> repository,
            UserCreateManager userCreateManager
            )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _repository = repository;
            _userCreateManager = userCreateManager;
        }


        /// <summary>
        /// RoleNamese boşta olsa değer yolla yoksa patlarsın
        /// </summary>
        public async Task Create(CustomerDto input)
        {
            _userCreateManager.CreateUser("Customer", Map(input.CreateUserDto));
            var user = await _userRepository.GetAll().Where(q=>q.UserName==input.CreateUserDto.UserName).FirstOrDefaultAsync();
            var newCustomer = new Customer
            {
                User = user,
            };
            await _repository.InsertAsync(newCustomer);
        }
        public async Task Update(int id ,GetCustomerDto input)
        {
            var customerentity = await _repository.GetAll().Include(q => q.User).Where(a => a.Id == id).FirstOrDefaultAsync();
            customerentity.User.Name = input.UserDto.Name;
            customerentity.User.Surname = input.UserDto.Surname;
            customerentity.User.EmailAddress = input.UserDto.EmailAddress;
            customerentity.User.PhoneNumber = input.UserDto.PhoneNumber;
            await _userManager.UpdateAsync(customerentity.User);
        }
        public async Task<List<GetCustomerDto>> GetFilst()
        {
            var entityList = await _repository.GetAll().Include(q => q.User).ToListAsync();
            return entityList.Select(q => new GetCustomerDto
            {
                UserDto=Map(q.User),
                Id=q.Id,
                
            }).ToList();
        }
        public async Task<List<GetCustomerDto>> GetPaginated(int pageSize , int pageNumber)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _repository.GetAll().Include(q => q.User).Skip((int)PageShow).Take((int)PageSize).ToListAsync();
            return entityList.Select(q => new GetCustomerDto
            {
                UserDto = Map(q.User),
                Id = q.Id,

            }).ToList();
        }
        public async Task<GetCustomerDto> GetById(int id)
        {
            var entity = await _repository.GetAll().Where(q=>q.Id==id).Include(q => q.User).FirstOrDefaultAsync();
            var Dto = new GetCustomerDto();
            Dto.Id = entity.Id;
            Dto.UserDto=Map(entity.User);
            return Dto;
        }
        public async Task Delete(int id)
        {
            var entity = await _repository.GetAll().Include(q=>q.User).Where(a=>a.Id==id).FirstOrDefaultAsync();
            await _userRepository.DeleteAsync(entity.User.Id);
            await _repository.DeleteAsync(id);
        }
        
        
        
        #region User
        private Users.Dto.UserDto Map(User e)
        {
            return new Users.Dto.UserDto
            {
                Id = e.Id,
                UserName = e.UserName,
                Surname = e.Surname,
                CreationTime = e.CreationTime,
                EmailAddress=e.EmailAddress,
                PhoneNumber=e.PhoneNumber,
                FullName=e.FullName,
                Name = e.Name,
                IsActive = e.IsActive
            };
        }
        private User Map(Users.Dto.UserDto e)
        {
            return new User
            {
                Id = e.Id,
                UserName = e.UserName,
                Surname = e.Surname,
                CreationTime = e.CreationTime,
                EmailAddress = e.EmailAddress,
                PhoneNumber=e.PhoneNumber,
                Name = e.Name,
                IsActive = e.IsActive
            };
        }
        private User Map(CreateUserDto e)
        {
            return new User
            {
                Id = 0,
                UserName = e.UserName,
                Surname = e.Surname,
                CreationTime = DateTime.Now,
                EmailAddress = e.EmailAddress,
                PhoneNumber = e.PhoneNumber,
                Name = e.Name,
                IsActive = e.IsActive,
            };
        }
        #endregion
    }
}
