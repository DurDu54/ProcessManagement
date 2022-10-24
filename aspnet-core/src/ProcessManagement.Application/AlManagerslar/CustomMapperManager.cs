using Abp.Domain.Services;
using ProcessManagement.Authorization.Users;
using ProcessManagement.CustomerAppService.CustomerDtos;
using ProcessManagement.Customers;
using ProcessManagement.Developers;
using ProcessManagement.Deveoper.Dto;
using ProcessManagement.Manager.Dto;
using ProcessManagement.Missions.Dto;
using ProcessManagement.Missions;
using ProcessManagement.Profession.Dto;
using ProcessManagement.Project.Dto;
using ProcessManagement.Users.Dto;
using System;
using System.Linq;

namespace ProcessManagement.AlManagerslar
{
    public class CustomMapperManager : IDomainService
    {
        public CustomMapperManager()
        {
        }
        #region User
        public UserDto Map(User e)
        {
            return new UserDto
            {
                Id = e.Id,
                UserName = e.UserName,
                Surname = e.Surname,
                CreationTime = e.CreationTime,
                EmailAddress = e.EmailAddress,
                PhoneNumber = e.PhoneNumber,
                FullName = e.FullName,
                Name = e.Name,
                IsActive = e.IsActive,
            };
        }
        public User Map(UserDto e)
        {
            return new User
            {
                Id = e.Id,
                UserName = e.UserName,
                Surname = e.Surname,
                CreationTime = e.CreationTime,
                EmailAddress = e.EmailAddress,
                PhoneNumber = e.PhoneNumber,
                Name = e.Name,
                IsActive = e.IsActive
            };
        }

        #endregion
        #region Developer
        public Developer Map(GetDeveloperDto e)
        {
            return new Developer
            {
                Id = e.Id,
                User = Map(e.UserDto),
                ProfessionId = e.ProfessionId
            };
        }
        public GetDeveloperDto Map(Developer e)
        {
            return new GetDeveloperDto
            {
                Id = e.Id,
                UserDto = Map(e.User),
                ProfessionId = e.Profession.Id,
                Missions = e.Missions.Select(q => new GetMissionDto
                {
                    Id = q.Id,
                    Text = q.Text,
                    BeginTime = q.BeginTime,
                    Status = q.Status,
                    ProjectId = q.ProjectId
                }).ToList(),
                Projects = e.Projects.Select(q => new GetProjectDto
                {
                    Id = q.Id,
                    Name = q.Name,
                    BeginTime = q.BeginTime,
                    CustomerId = q.CustomerId,
                    EndTime = q.EndTime,
                    ManagerId = q.ManagerId,
                    Status = q.Status,
                }).ToList(),

            };
        }
        #endregion
        #region Professionlar
        public Professionlar.Profession Map(GetProfessionDto e)
        {
            return new Professionlar.Profession
            {
                Id = e.Id,
                Text = e.Text,

            };
        }
        public GetProfessionDto Map(Professionlar.Profession e)
        {
            return new GetProfessionDto
            {
                Id = e.Id,
                Text = e.Text,
            };
        }
        #endregion
        #region Preject
        public Projects.Project Map(GetProjectDto e)
        {
            return new Projects.Project
            {
                Id = e.Id,
                Name = e.Name,
                BeginTime = e.BeginTime,
                EndTime = e.EndTime,
                Status = e.Status,
                CustomerId = e.CustomerId,
                ManagerId = e.ManagerId,
            };
        }
        public GetProjectDto Map(Projects.Project e)
        {
            return new GetProjectDto
            {
                Id = e.Id,
                Name = e.Name,
                BeginTime = e.BeginTime,
                EndTime = e.EndTime,
                Status = e.Status,
                CustomerId = e.CustomerId,
                ManagerId = e.ManagerId,

            };
        }
        public Projects.Project Map(CreateProjectDto e)
        {
            return new Projects.Project
            {
                Id = e.Id,
                Name = e.Name,
                Status = e.Status,
                BeginTime = DateTime.Now,
                EndTime = e.EndTime,
                CustomerId = e.CustomerId,
                ManagerId = e.ManagerId,
            };
        }

        #endregion
        #region Customer
        public Customer Map(GetCustomerDto c)
        {
            return new Customer
            {
                Id = c.Id,
                User = Map(c.UserDto),
            };
        }
        public GetCustomerDto Map(Customer c)
        {
            return new GetCustomerDto
            {
                Id = c.Id,
                UserDto = Map(c.User),
            };
        }
        #endregion
        #region Manager
        public Managers.Manager Map(GetManagerDto m)
        {
            return new Managers.Manager
            {
                Id = m.Id,
                User = Map(m.UserDto),
            };
        }

        public GetManagerDto Map(Managers.Manager m)
        {
            return new GetManagerDto
            {
                Id = m.Id,
                UserDto = Map(m.User),
                Projects = m.Projects.Select(q => new GetProjectDto
                {
                    Id = q.Id,
                    Name = q.Name,
                    Status = q.Status,
                    BeginTime = q.BeginTime,
                    CustomerId = q.Customer.Id,
                    ManagerId = q.Manager.Id,
                    EndTime = q.EndTime,
                }).ToList(),
            };
        }


        #endregion
        #region Commit
        public Commit Map(GetCommitDto e)
        {
            return new Commit
            {
                Id = e.Id,
                Text = e.Text,
                MissionId = e.MissionId,
                CreationTime = DateTime.Now,
            };
        }
        public GetCommitDto Map(Commit e)
        {
            return new GetCommitDto
            {
                Id = e.Id,
                Text = e.Text,
                MissionId = e.MissionId,
            };
        }

        #endregion
    }
}
