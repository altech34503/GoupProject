using Microsoft.AspNetCore.Mvc;
using StartupInvestorMatcher.Model.Entities;
using StartupInvestorMatcher.Model.Repositories;
using System.Collections.Generic;

namespace StartupInvestorMatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentSizeController : ControllerBase
    {
        private readonly InvestmentSizeRepository _repository;

        public InvestmentSizeController(InvestmentSizeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InvestmentSize>> GetInvestmentSizes()
        {
            return Ok(_repository.GetInvestmentSizes());
        }

        [HttpGet("{id}")]
        public ActionResult<InvestmentSize> GetInvestmentSize(int id)
        {
            var size = _repository.GetInvestmentSizeById(id);
            if (size == null)
                return NotFound();
            return Ok(size);
        }
    }
}
