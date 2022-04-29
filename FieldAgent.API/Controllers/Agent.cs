using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.API.Controllers;

public class AgentController : ControllerBase
{
    private readonly IAgentRepository _agentRepository;

    public AgentController(IAgentRepository agentRepository)
    {
        _agentRepository = agentRepository;
    }

    // [HttpGet]
    // [Route("api/agencies")]
    // public async Task<IActionResult> GetAgencies()
    // {
    //     var agencies = _agentRepository.Get();
    //     return Ok(agencies);
    // }

    [HttpGet]
    [Route("api/agencies/{id}")]
    public async Task<IActionResult> GetAgent(int id)
    {
        var agent =  _agentRepository.Get(id);
        if(agent.Success)  
            return Ok(agent.Data);
        else
            return Ok(agent);
    }

    [HttpPost]
    [Route("api/agencies")]
    public async Task<IActionResult> CreateAgency([FromBody] Agent agent)
    {
        if (agent == null)
        {
            return BadRequest();
        }

        var result = _agentRepository.Insert(agent);
        return CreatedAtAction(nameof(GetAgent), new {id = agent.AgentId}, agent);
    }

    [HttpPut]
    [Route("api/agencies/{id}")]
    public async Task<IActionResult> UpdateAgent(int id, [FromBody] Agent agent)
    {
        if (agent == null || agent.AgentId != id)
        {
            return BadRequest();
        }

        var agentToUpdate = _agentRepository.Get(id);
        if (agentToUpdate == null)
        {
            return NotFound();
        }

        var result = _agentRepository.Update(agent);
        return Ok(result.Message);
    }

    [HttpDelete]
    [Route("api/agencies/{id}")]
    public async Task<IActionResult> DeleteAgent(int id)
    {
        var agent = _agentRepository.Get(id);
        if (agent == null)
        {
            return NotFound();
        }

        var result = _agentRepository.Delete(id);
        return Ok(result.Message);
    }
}