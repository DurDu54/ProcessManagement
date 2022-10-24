using ProcessManagement.Authorization.Users;
using ProcessManagement.Project.Dto;
using ProcessManagement.Users.Dto;
using System.Collections.Generic;

namespace ProcessManagement.Manager.Dto
{
    public class GetManagerDto
    {
        public int Id { get; set; }
        public virtual UserDto UserDto { get; set; }
        public List<GetProjectDto> Projects { get; set; }
    }
}
