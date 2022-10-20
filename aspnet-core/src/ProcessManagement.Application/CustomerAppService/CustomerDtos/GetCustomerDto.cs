using ProcessManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.CustomerAppService.CustomerDtos
{
    public class GetCustomerDto
    {
        public int Id { get; set; }
        public UserDto UserDto { get; set; }
    }
}
