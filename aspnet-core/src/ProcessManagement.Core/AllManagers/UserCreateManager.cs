using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.AllManagers
{
    public interface IUserCreateManager : IDomainService
    {

    }
    public class UserCreateManager : IUserCreateManager
    {
        private readonly IRepository<User, long> _repository;
        private readonly UserManager _userManager;

        public UserCreateManager(IRepository<User, long> repository, UserManager userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public User CreateUser(string roleName, User user)
        {
            _userManager.AddToRoleAsync(user,roleName);
            var createdUser = _repository.GetAll().Where(q => q.UserName == user.UserName).FirstOrDefaultAsync();
            return createdUser.Result;
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
