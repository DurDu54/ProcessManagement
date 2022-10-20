using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using ProcessManagement.Authorization.Users;
using ProcessManagement.Customers;
using ProcessManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.CustomerAppService.CustomerDtos
{
    public class CustomerDto 
    {
        public virtual CreateUserDto CreateUserDto { get; set; }
    }
}
