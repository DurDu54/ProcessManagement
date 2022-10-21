using Abp.Domain.Repositories;
using ProcessManagement.CustomMapper;
using ProcessManagement.Profession.Dto;
using ProcessManagement.Professionlar;
using System;
using System.Collections.Generic;
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
            await _repository.InsertOrUpdateAsync(_mapper.Map(input));
        }
    }
}
