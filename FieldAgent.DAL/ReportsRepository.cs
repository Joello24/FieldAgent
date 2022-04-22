using FieldAgent.Core;

namespace FieldAgent.DAL;

public class ReportsRepository : IReportsRepository
{
    public Response<List<TopAgentListItem>> GetTopAgents()
    {
        throw new NotImplementedException();
    }

    public Response<List<PensionListItem>> GetPensionList(int agencyId)
    {
        throw new NotImplementedException();
    }

    public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
    {
        throw new NotImplementedException();
    }
}