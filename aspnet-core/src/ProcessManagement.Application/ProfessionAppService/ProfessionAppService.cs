using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ProcessManagement.AlManagerslar;
using ProcessManagement.Profession.Dto;
using ProcessManagement.Professionlar;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.ProfessionAppService
{
    public class ProfessionAppService :IProfessionAppService
    {
        private readonly IRepository<Professionlar.Profession> _repository;
        private readonly CustomMapperManager _mapper;
        public ProfessionAppService(
            IRepository<Professionlar.Profession> repository
, CustomMapperManager mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Crete(GetProfessionDto input)
        {
            await _repository.InsertAsync(_mapper.Map(input));
        }
        public async Task Update(int id, string text)
        {
            var profession = await _repository.GetAsync(id);
            profession.Text = text;
            await _repository.UpdateAsync(profession);
        }
        public async Task<List<GetProfessionDto>> List()
        {
            var entityList = await _repository.GetAllListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task<List<GetProfessionDto>> PaginatedList (int pageNumber,int pageSize )
        {
            var PageSize = pageSize;
            var PageShow = (pageNumber - 1) * PageSize;
            var entityList = await _repository.GetAll()
                .Skip((int)PageShow).Take((int)PageSize)
                .ToListAsync();
            return entityList.Select(q => _mapper.Map(q)).ToList();
        }
        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
