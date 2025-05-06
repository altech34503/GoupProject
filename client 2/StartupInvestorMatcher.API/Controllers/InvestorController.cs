using StartupInvestorMatcher.Model.Entities;
using StartupInvestorMatcher.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StartupInvestorMatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorController : ControllerBase
    {
        protected InvestorRepository Repository { get; }

        // Constructor to inject the InvestorRepository
        public InvestorController(InvestorRepository repository)
        {
            Repository = repository;
        }

        // GET: api/investor/{id}
        [HttpGet("{id}")]
        public ActionResult<Investor> GetInvestor([FromRoute] int id)
        {
            Investor investor = Repository.GetInvestorById(id);
            if (investor == null)
            {
                return NotFound();
            }
            return Ok(investor);
        }

        // GET: api/investor
        [HttpGet]
        public ActionResult<IEnumerable<Investor>> GetInvestors()
        {
            return Ok(Repository.GetInvestors());
        }

        // POST: api/investor
        [HttpPost]
        public ActionResult Post([FromBody] Investor investor)
        {
            if (investor == null)
            {
                return BadRequest("Investor info not correct");
            }

            bool status = Repository.InsertInvestor(investor);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }

        // PUT: api/investor
        [HttpPut]
        public ActionResult UpdateInvestor([FromBody] Investor investor)
        {
            if (investor == null)
            {
                return BadRequest("Investor info not correct");
            }

            Investor existing = Repository.GetInvestorById(investor.MemberId);
            if (existing == null)
            {
                return NotFound($"Investor with ID {investor.MemberId} not found");
            }

            bool status = Repository.UpdateInvestor(investor);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }

        // DELETE: api/investor/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteInvestor([FromRoute] int id)
        {
            Investor existing = Repository.GetInvestorById(id);
            if (existing == null)
            {
                return NotFound($"Investor with ID {id} not found");
            }

            bool status = Repository.DeleteInvestor(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Unable to delete investor with ID {id}");
        }
    }
}
