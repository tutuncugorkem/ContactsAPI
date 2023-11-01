using AutoMapper;
using Contacts.Core.DTOs;
using Contacts.Core.Models;
using Contacts.Core.Services;
using ContactsAPI.Filters;
using ContactsAPI.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ContactsAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class C1 : CustomBaseController
    {
        //generic repository kullandıgımız için maplemeyi şu an burada yapacağız. ileride service katmanında olacak
        private readonly IMapper _mapper;
        private readonly IService<BaseEntity> _service;
        private readonly ILogger<C1> _logger;
        private readonly IOptions<MyOptions> _options;
        public C1(IMapper mapper, IService<BaseEntity> service, ILogger<C1> logger, IOptions<MyOptions> options
            )
        {
            _mapper = mapper;
            _service = service;
            _logger = logger;
            _options = options;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var baseEntities = await _service.GetAllAsync();

            var a = _options.Value.Name;

            if (_options.Value.Environment == "PROD")
            {
                // Mail gönder
            }

            var baseDtos = _mapper.Map<List<BaseDto>>(baseEntities.ToList());
            //return Ok( CustomResponseDto<List<BaseDto>>.Success(200, baseDtos));

            return CreateActionResult(CustomResponseDto<List<BaseDto>>.Success(200, baseDtos));

        }

        [ServiceFilter(typeof(NotFoundFilter<BaseEntity>))]        //notfoundfilter, bir atribute sınıfını miras almıyor,   VE ctro'unda parametre geçiyorsak direkt [] ile kullanamayız burada filter'ı!!!!!   
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            _logger.LogTrace("LogTrace get by id is called");
            _logger.LogDebug("LogDebug get by id is called");
            _logger.LogInformation("LogInformation get by id is called");
            _logger.LogWarning("LogWarning get by id is called");
            _logger.LogError("LogError get by id is called");



            var baseEntity = await _service.GetByIdAsync(id);

            var baseDto = _mapper.Map<BaseDto>(baseEntity);
            //return Ok( CustomResponseDto<List<BaseDto>>.Success(200, baseDtos));

            return CreateActionResult(CustomResponseDto<BaseDto>.Success(200, baseDto));

        }
        [HttpPost]    //201, created dönmek..
        public async Task<IActionResult> Save(BaseDto baseDtos)
        {
            var baseEntity = await _service.AddAsync(_mapper.Map<BaseEntity>(baseDtos));     //dtoyu entitiye dönüştür

            var baseDto = _mapper.Map<BaseDto>(baseEntity);                                   //entitiyi dönerken dtoya donustur


            return CreateActionResult(CustomResponseDto<BaseDto>.Success(201, baseDto));

        }
        [HttpPut]
        public async Task<IActionResult> Update(BaseUpdateDto baseDtos)
        {
            await _service.UpdateAsync(_mapper.Map<BaseEntity>(baseDtos));     //update bişey dönmüyor

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
        [HttpDelete("{id}")]   //DELETE 
        public async Task<IActionResult> Remove(int id)
        {
            var baseEntity = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(baseEntity);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}