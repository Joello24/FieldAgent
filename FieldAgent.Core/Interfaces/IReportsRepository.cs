
using FieldAgent.Core.Entities;

namespace FieldAgent.Core;

public interface IReportsRepository
{
    Response<List<TopAgentListItem>> GetTopAgents();
    Response<List<PensionListItem>> GetPensionList(int agencyId);
    Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId);
    Response<List<Agent>> AgentSearch(string term); 
}