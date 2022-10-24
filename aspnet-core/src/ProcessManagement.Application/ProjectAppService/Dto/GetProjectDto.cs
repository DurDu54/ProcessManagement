using AutoMapper;
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
        public string Name { get; set; }
        public StatusProject Status{ get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? CustomerId{ get; set; }
        public int? ManagerId { get; set; }

    }
    public class CreateProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatusProject Status { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? CustomerId { get; set; }
        public int? ManagerId { get; set; }
    }
}
