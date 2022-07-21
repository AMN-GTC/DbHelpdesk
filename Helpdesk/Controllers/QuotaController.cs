using AutoMapper;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
using Helpdesk.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotaController : ControllerBase
    {
        private readonly IQuotaCalculationService _quotaCalculationService;
        private readonly IMapper _mapper;
        public QuotaController(IQuotaCalculationService quotaCalculationSevice, IMapper mapper)
        {
            _quotaCalculationService = quotaCalculationSevice;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<QuotaCalcullationDTO>> Get(CancellationToken cancellationToken = default)
        {
            QuotaCalculationSpecification specification = new QuotaCalculationSpecification();
            List<QuotaCalculation> listquota = await _quotaCalculationService.GetList(specification.Build(), cancellationToken);
            List<QuotaCalcullationDTO> quotadto = _mapper.Map<List<QuotaCalculation>, List<QuotaCalcullationDTO>>(listquota);
            return Ok(quotadto);
        }
        [HttpPost]
        public async Task<ActionResult<QuotaCalcullationDTO>> Insert([FromBody] QuotaCalcullationDTO model, CancellationToken cancellationToken = default)
        {
            QuotaCalculation quota = _mapper.Map<QuotaCalcullationDTO, QuotaCalculation>(model);
            if (await _quotaCalculationService.Insert(quota, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _quotaCalculationService.GetError();
                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorValue in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorValue);

                    }
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
        {

            if (await _quotaCalculationService.Delete(id, cancellationToken) == false)
            {
                Dictionary<string, List<string>> errors = _quotaCalculationService.GetError();
                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorvalue in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorvalue);
                    }
                }
                return BadRequest(ModelState);
            }
            return Ok("berhasil dihapus");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] QuotaCalcullationDTO model, int id, CancellationToken cancellationToken = default)
        {
            QuotaCalculation quota = _mapper.Map<QuotaCalcullationDTO, QuotaCalculation>(model);
            if (await _quotaCalculationService.Update(quota, id, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _quotaCalculationService.GetError();
                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorvalue in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorvalue);
                    }
                }
                return BadRequest(ModelState);
            }
            QuotaCalcullationDTO result = _mapper.Map<QuotaCalculation, QuotaCalcullationDTO>(quota);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<QuotaCalcullationDTO>> Get(int id, CancellationToken cancellationToken = default)
        {

            QuotaCalculation objek = await _quotaCalculationService.GetObject(id, cancellationToken);
            QuotaCalcullationDTO quotaCalcullationDTO = _mapper.Map<QuotaCalcullationDTO>(objek);
            return Ok(quotaCalcullationDTO);
        }

    }
}
