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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.ObjectMapping;

namespace ProcessManagement.CustomMapper
{
    public class CustomMapperManager : IDomainService
    {
        private readonly IObjectMapper _mapper;
        public CustomMapperManager(IObjectMapper mapper)
        {
            _mapper = mapper;
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
                Profession = Map(e.ProfessionDto),
                //Projects = Map(e.Projects),
                Missions = Map(e.Missions)

            };
        }
        public GetDeveloperDto Map(Developer e)
        {
            return new GetDeveloperDto
            {
                Id = e.Id,
                UserDto = Map(e.User),
                ProfessionDto = Map(e.Profession),
                Missions = Map(e.Missions),
                //Projects=Map(e.Projects),

            };
        }
        #endregion
        #region ListDeveloper
        public ICollection<Developer> Map(List<GetDeveloperDto> developers)
        {
            return _mapper.Map<ICollection<Developer>>(developers);
        }
        public List<GetDeveloperDto> Map(ICollection<Developer> developers)
        {
            return _mapper.Map<List<GetDeveloperDto>>(developers);

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
                Status = e.Status,
                BeginTime = e.BeginTime,
                EndTime = e.EndTime,
                Customer = Map(e.CustomerDto),
                Manager = Map(e.ManagerDto),
                Developers = Map(e.Developers),
                Missions = Map(e.Missions)
            };
        }
        public GetProjectDto Map(Projects.Project e)
        {
            return new GetProjectDto
            {
                Id = e.Id,
                Status = e.Status,
                BeginTime = e.BeginTime,
                EndTime = e.EndTime,
                CustomerDto = Map(e.Customer),
                ManagerDto = Map(e.Manager),
                Developers = Map(e.Developers),
                Missions = Map(e.Missions),
            };
        }

        #endregion
        #region ListProject
        public ICollection<Projects.Project> Map(List<GetProjectDto> p)
        {
            return _mapper.Map<ICollection<Projects.Project>>(p);
        }
        public List<GetProjectDto> Map(ICollection<Projects.Project> p)
        {
            return _mapper.Map<List<GetProjectDto>>(p);
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
                User = Map(m.User),
                Projects = Map(m.Projects)
            };
        }



        public GetManagerDto Map(Managers.Manager m)
        {
            return new GetManagerDto
            {
                Id = m.Id,
                User = Map(m.User),
                Projects = Map(m.Projects),
            };
        }


        #endregion
        #region ListMission
        public ICollection<Mission> Map(List<GetMissionDto> m)
        {
            return _mapper.Map<ICollection<Mission>>(m);
        }
        public List<GetMissionDto> Map(ICollection<Mission> m)
        {
            return _mapper.Map<List<GetMissionDto>>(m);
        }
        #endregion
    }
}
