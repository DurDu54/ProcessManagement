using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.Authorization.Users;
using ProcessManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.AlManagerslar
{
    public interface IUserCreateManager : IDomainService
    {

    }
    public class UserCreateManager : IUserCreateManager
    {
        private readonly UserManager _userManager;
        private readonly IRepository<User, long> _repository;
        public UserCreateManager(
            IRepository<User, long> repository,
            UserManager userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<long> CreateUser(string roleName, CreateUserDto input)
        {
            input.RoleNames[0] = roleName;
            var user = new User
            {
                Id = 0,
                UserName = input.UserName,
                Name = input.Name,
                Surname = input.Surname,
                EmailAddress = input.EmailAddress,
                PhoneNumber = input.PhoneNumber,
                Password = input.Password,

            };
            var a = await _userManager.CreateAsync(user, input.Password);
            await _userManager.SetRolesAsync(user, input.RoleNames);
            var newUser = await _repository.GetAll().Where(q => q.UserName == input.UserName).FirstOrDefaultAsync();
            return newUser.Id;
        }

        public async Task UpdateUser(User user,UserDto userDto)
        {
            if (user.EmailAddress != userDto.EmailAddress)
            {
                user.EmailAddress = userDto.EmailAddress;
            }
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.PhoneNumber = userDto.PhoneNumber;
            await _userManager.UpdateAsync(user);
        }

    }
}
