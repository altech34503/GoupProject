using StartupInvestorMatcher.Model.Entities;
using StartupInvestorMatcher.Model.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace StartupInvestorMatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorController : ControllerBase
    {
        protected InvestorRepository InvestorRepository { get; }
        protected StartupRepository StartupRepository { get; }

        // Constructor to inject both repositories
        public InvestorController(InvestorRepository investorRepository, StartupRepository startupRepository)
        {
            InvestorRepository = investorRepository;
            StartupRepository = startupRepository;
        }

        // GET: api/investor/{id}
        [HttpGet("{id}")]
        public ActionResult<Investor> GetInvestor([FromRoute] int id)
        {
            Investor investor = InvestorRepository.GetInvestorById(id);
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
            return Ok(InvestorRepository.GetInvestors());
        }

        // POST: api/investor
        [HttpPost]
        public ActionResult Post([FromBody] Investor investor)
        {
            if (investor == null)
            {
                return BadRequest("Investor info not correct");
            }

            // Automatically assign a member_id if not provided
            if (investor.MemberId == 0)
            {
                // Fetch all member IDs from both Investors and Startups
                var investorIds = InvestorRepository.GetInvestors().Select(i => i.MemberId);
                var startupIds = StartupRepository.GetStartups().Select(s => s.MemberId);

                // Combine all member IDs and find the maximum
                var allMemberIds = investorIds.Concat(startupIds);
                investor.MemberId = allMemberIds.Any() ? allMemberIds.Max() + 1 : 1;
            }

            bool status = InvestorRepository.InsertInvestor(investor);
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

            Investor existing = InvestorRepository.GetInvestorById(investor.MemberId);
            if (existing == null)
            {
                return NotFound($"Investor with ID {investor.MemberId} not found");
            }

            bool status = InvestorRepository.UpdateInvestor(investor);
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
            Investor existing = InvestorRepository.GetInvestorById(id);
            if (existing == null)
            {
                return NotFound($"Investor with ID {id} not found");
            }

            bool status = InvestorRepository.DeleteInvestor(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Unable to delete investor with ID {id}");
        }

        // GET: api/investor/next-member-id
        [HttpGet("next-member-id")]
        public ActionResult<int> GetNextMemberId()
        {
            // Fetch all member IDs from both Investors and Startups
            var investorIds = InvestorRepository.GetInvestors().Select(i => i.MemberId);
            var startupIds = StartupRepository.GetStartups().Select(s => s.MemberId);

            // Combine all member IDs and find the maximum
            var allMemberIds = investorIds.Concat(startupIds);
            int nextMemberId = allMemberIds.Any() ? allMemberIds.Max() + 1 : 1;

            // Return the next available member_id
            return Ok(nextMemberId);
        }
    }
}
