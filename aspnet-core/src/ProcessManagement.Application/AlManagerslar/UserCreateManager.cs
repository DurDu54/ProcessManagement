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
        private readonly IObjectMapper _mapper;
        public UserCreateManager(
            IRepository<User, long> repository
            , IObjectMapper mapper,
            UserManager userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<User> CreateUser(string roleName, CreateUserDto input)
        {
            input.RoleNames[0] = roleName;
            var user = new User
            {
                UserName = input.UserName,
                Name = input.Name,
                Surname = input.Surname,
                EmailAddress = input.EmailAddress,
                PhoneNumber = input.PhoneNumber,
                Password = input.Password,

            };
            await _userManager.CreateAsync(user);
            await _userManager.SetRolesAsync(user, input.RoleNames);
            var newUser = await _repository.GetAll().Where(q => q.UserName == input.UserName).FirstOrDefaultAsync();
            return newUser;
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

        //input.CreateUserDto.RoleNames[0] = "Customer";
        //    var newUser = new User
        //    {
        //        UserName = input.CreateUserDto.UserName,
        //        Name = input.CreateUserDto.Name,
        //        Surname = input.CreateUserDto.Surname,
        //        EmailAddress = input.CreateUserDto.EmailAddress,
        //        PhoneNumber = input.CreateUserDto.PhoneNumber,
        //        Password = input.CreateUserDto.Password,

        //    };
        //await _userManager.CreateAsync(newUser);
        //await _userManager.SetRolesAsync(newUser, input.CreateUserDto.RoleNames);
    }
}
