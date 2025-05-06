using Microsoft.AspNetCore.Mvc;
using StartupInvestorMatcher.Model.Entities;
using StartupInvestorMatcher.Model.Repositories;
using System.Collections.Generic;

namespace StartupInvestorMatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly IndustryRepository _repository;

        public IndustryController(IndustryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Industry>> GetIndustries()
        {
            return Ok(_repository.GetIndustries());
        }

        [HttpGet("{id}")]
        public ActionResult<Industry> GetIndustry(int id)
        {
            var industry = _repository.GetIndustryById(id);
            if (industry == null)
                return NotFound();
            return Ok(industry);
        }
    }
}
