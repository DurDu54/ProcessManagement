using ProcessManagement.CustomerAppService.CustomerDtos;
using ProcessManagement.Deveoper.Dto;
using ProcessManagement.Enums;
using ProcessManagement.Manager.Dto;
using ProcessManagement.Missions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Project.Dto
{
    public class GetProjectDto
    {
        public int Id { get; set; }
        public StatusProject Status{ get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public GetCustomerDto CustomerDto{ get; set; }
        public GetManagerDto ManagerDto { get; set; }
        public List<GetDeveloperDto> Developers{ get; set; }
        public List<GetMissionDto> Missions{ get; set; }

    }
}
