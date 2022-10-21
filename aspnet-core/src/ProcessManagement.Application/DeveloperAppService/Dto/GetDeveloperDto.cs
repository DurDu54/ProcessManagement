using ProcessManagement.Missions;
using ProcessManagement.Missions.Dto;
using ProcessManagement.Profession.Dto;
using ProcessManagement.Professionlar;
using ProcessManagement.Project.Dto;
using ProcessManagement.Projects;
using ProcessManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Deveoper.Dto
{
    public class GetDeveloperDto
    {
        public int Id { get; set; }
        public virtual UserDto UserDto { get; set; }
        public virtual GetProfessionDto ProfessionDto { get; set; }
        public List<GetProjectDto> Projects { get; set; }
        public List<GetMissionDto> Missions { get; set; }

    }
}
