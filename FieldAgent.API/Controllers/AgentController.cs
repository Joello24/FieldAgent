using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ViewModels;

namespace FieldAgent.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentController : ControllerBase
{
    private readonly IAgentRepository _agentRepository;
    private readonly IReportsRepository _reportsRepository;

    public AgentController(IAgentRepository agentRepository, IReportsRepository reportsRepository)
    {
        _reportsRepository = reportsRepository;
        _agentRepository = agentRepository;
    }
    
    [HttpGet]
    [Route("/api/[controller]",Name = "GetAgents")]
    public IActionResult GetAgents()
    {
        var agent = _agentRepository.GetAll();
        if(agent.Success)
        {
            return Ok(agent.Data);
        }
        else
        {
            return BadRequest(agent.Message);
        }
    }

    [HttpGet]
    [Route("/api/[controller]/{id}",Name = "GetAgent")]
    public IActionResult GetAgent(int id)
    {
        var agent =  _agentRepository.Get(id);
        if(agent.Success)
        {
            return Ok(agent.Data);
        }
        else
        {
            return BadRequest(agent.Message);
        }
    }
    [HttpGet]
    [Route("/api/[controller]/missions/{id}",Name = "GetMissionsByAgent")]
    public IActionResult GetMissionsByAgent(int id)
    {
        var missions = _agentRepository.GetMissions(id);
        if(missions.Success)
        {
            if(missions.Data.Count == 0)
            {
                return NotFound("Agent has no missions");
            }
            return Ok(missions.Data);
        }
        else
        {
            return BadRequest(missions.Message);
        }
    }
    

    [HttpPost]
    public IActionResult AddAgent([FromBody] ViewAgent viewAgent)
    {
        if(ModelState.IsValid)
        {
            var agent = new Agent()
            {
                FirstName = viewAgent.FirstName,
                LastName = viewAgent.LastName,
                DateOfBirth = viewAgent.DateOfBirth,
                Height = viewAgent.Height
            };
            var result = _agentRepository.Insert(agent);
            if(result.Success)
            {
                return CreatedAtAction(nameof(GetAgent), new {id = agent.AgentId}, agent);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPut, Authorize]
    public IActionResult UpdateAgent([FromBody] ViewAgent viewAgent)
    {
        if(ModelState.IsValid && viewAgent.agentId > 0)
        {
            var agent = new Agent()
            {
                AgentId = viewAgent.agentId,
                FirstName = viewAgent.FirstName,
                LastName = viewAgent.LastName,
                DateOfBirth = viewAgent.DateOfBirth,
                Height = viewAgent.Height
            };
            
            var agentToUpdate = _agentRepository.Get(agent.AgentId);
            if (agentToUpdate == null)
            {
                return NotFound();
            }

            var result = _agentRepository.Update(agent);
            if (result.Success)
            {
                return Ok("Agent updated successfully");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        else
        {
            if(viewAgent.agentId < 1)
                ModelState.AddModelError("agentId", "Invalid Agent Id");
            return BadRequest(ModelState);
        }
    }

    [HttpDelete("{agentId}"), Authorize]
    public IActionResult DeleteAgent(int agentId)
    {
        var agent = _agentRepository.Get(agentId);
        if (!agent.Success)
        {
            return NotFound(agent.Message);
        }
        
        var result = _agentRepository.Delete(agentId);
        if(result.Success)
            return Ok("Deleted successfully!");
        else
        {
            return BadRequest(result.Message);
        }
    }
    // Search by term and return agents
    [HttpGet]
    [Route("/api/[controller]/search/{term}", Name = "SearchAgents")]
    public IActionResult SearchAgents(string term)
    {
        var agents = _reportsRepository.AgentSearch(term);
        if(agents.Success)
        {
            if(agents.Data.Count == 0)
            {
                return NotFound("No agents found");
            }
            return Ok(agents.Data);
        }
        else
        {
            return BadRequest(agents.Message);
        }
    }
    // Response<List<TopAgentListItem>> GetTopAgents();
    //Response<List<PensionListItem>> GetPensionList(int agencyId);
    //Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId);
    [HttpGet]
    [Route("/api/[controller]/topagents", Name = "GetTopAgents")]
    public IActionResult GetTopAgents()
    {
        var agents = _reportsRepository.GetTopAgents();
        if(agents.Success)
        {
            if(agents.Data.Count == 0)
            {
                return NotFound("No agents found");
            }
            return Ok(agents.Data);
        }
        else
        {
            return BadRequest(agents.Message);
        }
    }

    [HttpGet]
    [Route("/api/[controller]/pensionlist/{agencyId}", Name = "GetPensionList")]
    public IActionResult GetPensionList(int agencyId)
    {
        var pensions = _reportsRepository.GetPensionList(agencyId);
        if(pensions.Success)
        {
            if(pensions.Data.Count == 0)
            {
                return NotFound("No pensions found");
            }
            return Ok(pensions.Data);
        }
        else
        {
            return BadRequest(pensions.Message);
        }
    }

    [HttpGet]
    [Route("/api/[controller]/clearanceaudit/{securityClearanceId}/{agencyId}", Name = "AuditClearance")]
    public IActionResult AuditClearance(int securityClearanceId, int agencyId)
    {
        var audit = _reportsRepository.AuditClearance(securityClearanceId, agencyId);
        if(audit.Success)
        {
            if(audit.Data.Count == 0)
            {
                return NotFound("No audit found");
            }
            return Ok(audit.Data);
        }
        else
        {
            return BadRequest(audit.Message);
        }
    }
}