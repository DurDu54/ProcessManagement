using ProcessManagement.Deveoper.Dto;
using ProcessManagement.Enums;
using ProcessManagement.Project.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Missions.Dto
{
    public class GetMissionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public StatusMission Status{ get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<GetCommitDto> Commits { get; set; }
        public int? DeveloperId { get; set; }
        public int ProjectId { get; set; }

    }

    public class CreateMissionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public StatusMission Status { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ProjectId { get; set; }
        public int? DeveloperId { get; set; }
    }
}
