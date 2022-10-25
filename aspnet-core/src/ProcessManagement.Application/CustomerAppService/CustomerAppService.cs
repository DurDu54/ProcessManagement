using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Authorization.Users;
using ProcessManagement.CustomerAppService.CustomerDtos;
using ProcessManagement.Customers;
using ProcessManagement.Project.Dto;
using ProcessManagement.Projects;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ProcessManagement.CustomerAppService
{
    public class CustomerAppService : IApplicationService
    {
        private readonly UserManager _userManager;
        private readonly UserCreateManager _userCreateManager;
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<User, long> _userRepository;
        private readonly CustomMapperManager _mapper;
        private readonly IRepository<Projects.Project> _projectRepository;
        public CustomerAppService(
            UserManager userManager,
            IRepository<User, long> userRepository,
            IRepository<Customer> repository,
            UserCreateManager userCreateManager,
            CustomMapperManager mapper,
            IRepository<Projects.Project> projectRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _repository = repository;
            _userCreateManager = userCreateManager;
            _mapper = mapper;
            _projectRepository = projectRepository;
        }


        /// <summary>
        /// RoleNamese boşta olsa değer yolla yoksa patlarsın
        /// </summary>
        public async Task Create(CustomerDto input)
        {

            var newUserId = _userCreateManager.CreateUser("Customer", input.CreateUserDto);
            var newUser = _userRepository.GetAsync(newUserId.Result).Result;
            if (newUser == null)
            {
                throw new UserFriendlyException("Sistem tarafindfaan hata oluştu");
            }
            var newCustomer = new Customer
            {
                User = newUser,
            };
            await _repository.InsertAsync(newCustomer);
        }
        public async Task Update(int id, GetCustomerDto input)
        {
            var customerentity = _repository.GetAll().Include(q => q.User).Where(a => a.Id == id).FirstOrDefault();
            await _userCreateManager.UpdateUser(customerentity.User, input.UserDto);
        }
        public async Task<List<GetCustomerDto>> GetList()
        {
            var entityList = await _repository.GetAll().Include(q => q.User).ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetCustomerDto>> GetPaginatedList(int pageSize = 10, int pageNumber = 1)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _repository.GetAll().Include(q => q.User).Skip((int)PageShow).Take((int)PageSize).ToListAsync();
            return entityList.Select(q => new GetCustomerDto
            {
                Id = q.Id,
                UserDto = _mapper.Map(q.User),
            }).ToList();
        }
        public async Task<GetCustomerDto> GetById(int id)
        {
            var entity = await _repository.GetAll().Where(q => q.Id == id).Include(q => q.User).FirstOrDefaultAsync();
            var Dto = new GetCustomerDto();
            Dto.Id = entity.Id;
            Dto.UserDto = _mapper.Map(entity.User);
            return Dto;
        }
        public async Task Delete(int id)
        {
            var entity = await _repository.GetAll().Include(q => q.User).Where(a => a.Id == id).FirstOrDefaultAsync();
            await _userRepository.DeleteAsync(entity.User.Id);
            await _repository.DeleteAsync(id);
        }
        public async Task<List<GetProjectDto>> GetMyProject(long id, int pageSize = 10, int pageNumber = 1)
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var projects = await _projectRepository.GetAll()
                .Include(q => q.Manager).ThenInclude(q => q.User)
                .Include(q => q.Customer).ThenInclude(q => q.User)
                .Include(q => q.Developers).ThenInclude(q => q.User)
                .Include(q => q.Missions)
                .Skip((int)PageShow).Take((int)PageSize)
                .Where(q => q.Customer.Id == id).ToListAsync();
            return projects.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<(string fileName, string path)>> UploadAsync(IFormFileCollection files)
        {
            throw new UserFriendlyException("BURASI DAHA SONRA EKLENECEK");
            //MANAGER KULLANILARAK YAPILACAK VE TÜM FONKSİYONLAR ORAYI ÇAĞIRARAK YAZILACAK
        }
    }
}
