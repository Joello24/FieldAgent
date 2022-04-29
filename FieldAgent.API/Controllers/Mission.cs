using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MissionController : ControllerBase
{
    private readonly IMissionRepository _missionRepository;

    public MissionController(IMissionRepository missionRepository)
    {
        _missionRepository = missionRepository;
    }

    // [HttpGet]
    // [Route("api/agencies")]
    // public async Task<IActionResult> GetAgencies()
    // {
    //     var agencies = _missionRepository.Get();
    //     return Ok(agencies);
    // }

    [HttpGet]
    [Route("/{id}")]
    public async Task<IActionResult> GetMission(int id)
    {
        var mission =  _missionRepository.Get(id);
        if(mission.Success)  
            return Ok(mission.Data);
        else
            return Ok(mission);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMission([FromBody] Mission mission)
    {
        if (mission == null)
        {
            return BadRequest();
        }

        var result = _missionRepository.Insert(mission);
        return CreatedAtAction(nameof(GetMission), new {id = mission.MissionId}, mission);
    }

    [HttpPut]
    [Route("api/agencies/{id}")]
    public async Task<IActionResult> UpdateMission(int id, [FromBody] Mission mission)
    {
        if (mission == null || mission.MissionId != id)
        {
            return BadRequest();
        }

        var missionToUpdate = _missionRepository.Get(id);
        if (missionToUpdate == null)
        {
            return NotFound();
        }

        var result = _missionRepository.Update(mission);
        return Ok(result.Message);
    }

    [HttpDelete]
    [Route("api/agencies/{id}")]
    public async Task<IActionResult> DeleteMission(int id)
    {
        var mission = _missionRepository.Get(id);
        if (mission == null)
        {
            return NotFound();
        }

        var result = _missionRepository.Delete(id);
        return Ok(result.Message);
    }
}