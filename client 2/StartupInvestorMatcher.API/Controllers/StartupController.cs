using Microsoft.AspNetCore.Mvc;
using StartupInvestorMatcher.Model.Entities;
using StartupInvestorMatcher.Model.Repositories;
using System.Collections.Generic;

namespace StartupInvestorMatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartupController : ControllerBase
    {
        protected StartupRepository Repository { get; }

        public StartupController(StartupRepository repository)
        {
            Repository = repository;
        }

        // GET: api/startup
        [HttpGet]
        public ActionResult<IEnumerable<Startup>> GetAllStartups()
        {
            var startups = Repository.GetStartups();
            return Ok(startups);
        }

        // GET: api/startup/{id}
        [HttpGet("{id}")]
        public ActionResult<Startup> GetStartupById(int id)
        {
            var startup = Repository.GetStartupById(id);
            if (startup == null)
            {
                return NotFound($"Startup with Member ID {id} not found.");
            }
            return Ok(startup);
        }

        // POST: api/startup
        [HttpPost]
        public ActionResult AddStartup([FromBody] Startup startup)
        {
            if (startup == null)
            {
                return BadRequest("Startup data is required.");
            }

            var success = Repository.InsertStartup(startup);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Could not insert startup.");
        }

        // PUT: api/startup
        [HttpPut]
        public ActionResult UpdateStartup([FromBody] Startup startup)
        {
            if (startup == null)
            {
                return BadRequest("Startup data is required.");
            }

            var existing = Repository.GetStartupById(startup.MemberId);
            if (existing == null)
            {
                return NotFound($"Startup with Member ID {startup.MemberId} not found.");
            }

            var success = Repository.UpdateStartup(startup);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Could not update startup.");
        }

        // DELETE: api/startup/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteStartup(int id)
        {
            var existing = Repository.GetStartupById(id);
            if (existing == null)
            {
                return NotFound($"Startup with Member ID {id} not found.");
            }

            var success = Repository.DeleteStartup(id);
            if (success)
            {
                return NoContent();
            }
            return BadRequest($"Could not delete startup with Member ID {id}.");
        }
    }
}
