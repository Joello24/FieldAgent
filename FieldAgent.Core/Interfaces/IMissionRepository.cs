using FieldAgent.Core.Entities;

namespace FieldAgent.Core;

public interface IMissionRepository
{
    Response<Mission> Insert(Mission mission);
    Response Update(Mission mission);
    Response Update(Mission mission, bool remove);
    Response Delete(int missionId);
    Response<Mission> Get(int missionId);
    Response<List<Mission>> GetByAgency(int agencyId);
    Response<List<Mission>> GetByAgent(int agentId);
}