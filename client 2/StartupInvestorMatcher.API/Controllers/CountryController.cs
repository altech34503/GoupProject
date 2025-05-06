using Microsoft.AspNetCore.Mvc;
using StartupInvestorMatcher.Model.Entities;
using StartupInvestorMatcher.Model.Repositories;
using System.Collections.Generic;

namespace StartupInvestorMatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _repository;

        public CountryController(CountryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetCountries()
        {
            var countries = _repository.GetCountries();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public ActionResult<Country> GetCountry(int id)
        {
            var country = _repository.GetCountryById(id);
            if (country == null)
                return NotFound();

            return Ok(country);
        }
    }
}
