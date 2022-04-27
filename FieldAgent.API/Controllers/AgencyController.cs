using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class AgencyController : ControllerBase
{
    private readonly IAgencyRepository _agencyRepository;

    public AgencyController(IAgencyRepository agencyRepository)
    {
        _agencyRepository = agencyRepository;
    }

    [HttpGet]
    [Route("api/agencies")]
    public async Task<IActionResult> GetAgencies()
    {
        var agencies = _agencyRepository.GetAll();
        return Ok(agencies);
    }

    [HttpGet]
    [Route("api/agencies/{id}")]
    public async Task<IActionResult> GetAgency(int id)
    {
        var agency =  _agencyRepository.Get(id);
        if(agency.Success)  
            return Ok(agency.Data);
        else
            return Ok(agency);
    }

    [HttpPost]
    [Route("api/agencies")]
    public async Task<IActionResult> CreateAgency([FromBody] Agency agency)
    {
        if (agency == null)
        {
            return BadRequest();
        }

        var result = _agencyRepository.Insert(agency);
        return CreatedAtAction(nameof(GetAgency), new {id = agency.AgencyId}, agency);
    }

    [HttpPut]
    [Route("api/agencies/{id}")]
    public async Task<IActionResult> UpdateAgency(int id, [FromBody] Agency agency)
    {
        if (agency == null || agency.AgencyId != id)
        {
            return BadRequest();
        }

        var agencyToUpdate = _agencyRepository.Get(id);
        if (agencyToUpdate == null)
        {
            return NotFound();
        }

        var result = _agencyRepository.Update(agency);
        return Ok(result.Message);
    }

    [HttpDelete]
    [Route("api/agencies/{id}")]
    public async Task<IActionResult> DeleteAgency(int id)
    {
        var agency = _agencyRepository.Get(id);
        if (agency == null)
        {
            return NotFound();
        }

        var result = _agencyRepository.Delete(id);
        return Ok(result.Message);
    }
}