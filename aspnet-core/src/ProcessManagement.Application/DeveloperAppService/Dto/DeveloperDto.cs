using ProcessManagement.Missions;
using ProcessManagement.Projects;
using ProcessManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Deveoper.Dto
{
    public class DeveloperDto
    {
        public virtual CreateUserDto CreateUserDto { get; set; }
        public int ProfessionId { get; set; }


    }
}
