using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ViewModels;

namespace FieldAgent.API.Controllers;

[Route("api/[controller]")]
public class AliasController : Controller
{
    private readonly IAliasRepository _aliasRepository;
    public AliasController(IAliasRepository aliasRepository)
    {
        _aliasRepository = aliasRepository;
    }
    
    // Response<Alias> Get(int aliasId);
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var result = _aliasRepository.Get(id);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }

    [HttpPost]
    public IActionResult AddAlias([FromBody]ViewAlias viewAlias)
    {
        if (ModelState.IsValid)
        {
            var alias = new Alias()
            {
                AgentId = viewAlias.AgentId,
                AliasName = viewAlias.AliasName,
                Persona = viewAlias.Persona,
                InterpolId = viewAlias.InterpolId,
            };
            var result = _aliasRepository.Insert(alias);
            if (result.Success)
            {
                return Ok(result.Data);
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
    public IActionResult UpdateAlias([FromBody]ViewAlias viewAlias)
    {
        if (ModelState.IsValid)
        {
            var alias = new Alias()
            {
                AgentId = viewAlias.AgentId,
                AliasId = viewAlias.AliasId,
                AliasName = viewAlias.AliasName,
                Persona = viewAlias.Persona,
                InterpolId = viewAlias.InterpolId,
            };
            var result = _aliasRepository.Update(alias);
            if (result.Success)
            {
                return Ok("Alias updated successfully");
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
    public IActionResult DeleteAlias(int id)
    {

        var findAlias = _aliasRepository.Get(id);
        if (!findAlias.Success)
        {
            return NotFound(findAlias.Message);
        }
        
        var result = _aliasRepository.Delete(id);
        if (result.Success)
        {
            return Ok("Alias deleted successfully");
        }
        else
        {
            return BadRequest(result.Message);
        }
    }
    
    [HttpGet]
    [Route("/api/alias/agent/{agentId}")]
    public IActionResult GetAliasByAgentId(int agentId)
    {
        var result = _aliasRepository.GetByAgent(agentId);
        if (result.Success)
        {
            if(result.Data.Count == 0)
            {
                return NotFound("No aliases found for this agent");
            }
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
}