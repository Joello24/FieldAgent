using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ViewModels;

namespace FieldAgent.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MissionController : Controller
{
    private readonly IMissionRepository _missionRepository;
    public MissionController(IMissionRepository missionRepository)
    {
        _missionRepository = missionRepository;
    }
    
    // Response<Mission> Get(int missionId);
    [HttpGet]
    [Route("/api/[controller]/{id}",Name = "GetMission")]
    public IActionResult GetMission(int id)
    {
        var mission =  _missionRepository.Get(id);
        if(mission.Success)
        {
            return Ok(mission.Data);
        }
        else
        {
            return BadRequest(mission.Message);
        }
    }
    // Response<Mission> Insert(Mission mission);
    [HttpPost]
    public IActionResult AddMission(ViewMission viewMission)
    {
        if(ModelState.IsValid)
        {
            var mission = new Mission()
            {
                AgencyId = viewMission.AgencyId,
                CodeName = viewMission.CodeName,
                StartDate = viewMission.StartDate,
                ProjectedEndDate = viewMission.ProjectedEndDate,
                ActualEndDate = viewMission.ActualEndDate,
                OperationalCost = viewMission.OperationalCost,
                Notes = viewMission.Notes
            };
            var result = _missionRepository.Insert(mission);
            if(result.Success)
            {
                return CreatedAtAction(nameof(GetMission), new {id = mission.MissionId}, mission);
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
    
    // Response Update(Mission mission);
    [HttpPut, Authorize]
    public IActionResult UpdateMission(ViewMission viewMission)
    {
        if(ModelState.IsValid)
        {
            var mission = new Mission()
            {
                MissionId = viewMission.MissionId,
                AgencyId = viewMission.AgencyId,
                CodeName = viewMission.CodeName,
                StartDate = viewMission.StartDate,
                ProjectedEndDate = viewMission.ProjectedEndDate,
                ActualEndDate = viewMission.ActualEndDate,
                OperationalCost = viewMission.OperationalCost,
                Notes = viewMission.Notes
            };
            var result = _missionRepository.Update(mission);
            if(result.Success)
            {
                return Ok("Mission updated successfully");
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
    
    [HttpDelete("{id}"), Authorize]
    public IActionResult DeleteMission(int id)
    {
        var findMission = _missionRepository.Get(id);
        if (!findMission.Success)
        {
            return NotFound(findMission.Message);
        }

        var result = _missionRepository.Delete(id);
        if(result.Success)
        {
            return Ok("Mission deleted successfully");
        }
        else
        {
            return BadRequest(result.Message);
        }
    }

    // Response<List<Mission>> GetByAgency(int agencyId);
    [HttpGet]
    [Route("/api/[controller]/agency/{agencyId}",Name = "GetByAgency")]
    public IActionResult GetByAgency(int agencyId)
    {
        var missions = _missionRepository.GetByAgency(agencyId);
        if(missions.Success)
        {
            if(missions.Data.Count == 0)
            {
                return NotFound("No missions found for this agency");
            }
            else
            {
                return Ok(missions.Data);
            }
        }
        else
        {
            return BadRequest(missions.Message);
        }
    }
    // Response<List<Mission>> GetByAgent(int agentId);
    [HttpGet]
    [Route("/api/[controller]/agent/{agentId}",Name = "GetByAgent")]
    public IActionResult GetByAgent(int agentId)
    {
        var missions = _missionRepository.GetByAgent(agentId);
        if(missions.Success)
        {
            if(missions.Data.Count == 0)
            {
                return NotFound("No missions found for this agent");
            }
            else
            {
                return Ok(missions.Data);
            }
        }
        else
        {
            return BadRequest(missions.Message);
        }
    }
    
    [HttpPut, Authorize]
    [Route("/api/[controller]/agent/{agentId}/mission/{missionId}")]
    public IActionResult AddAgentToMission(int agentId, int missionId)
    {
        var findMission = _missionRepository.Get(missionId);
        if(!findMission.Success)
        {
            return NotFound("Mission not found");
        }
        
        var ma = new List<MissionAgent>();
        ma.Add(new MissionAgent()
        {
            MissionId = missionId,
            AgentId = agentId
        });
        findMission.Data.MissionAgent = ma;
        var result = _missionRepository.Update(findMission.Data, false);
        
        if(result.Success)
        {
            return Ok("Agent added to mission successfully");
        }
        else
        {
            return BadRequest(result.Message);
        }
    }
    
    [HttpDelete, Authorize]
    [Route("/api/[controller]/agent/{agentId}/mission/{missionId}")]
    public IActionResult RemoveAgentFromMission(int agentId, int missionId)
    {
        var findMission = _missionRepository.Get(missionId);
        if(!findMission.Success)
        {
            return NotFound("Mission not found");
        }
        var ma = new List<MissionAgent>();
        ma.Add(new MissionAgent()
        {
            MissionId = missionId,
            AgentId = agentId
        });
        findMission.Data.MissionAgent = ma;
        var result = _missionRepository.Update(findMission.Data, true);
        
        if(result.Success)
        {
            return Ok("Agent removed from mission successfully");
        }
        else
        {
            return BadRequest(result.Message);
        }
    }
}