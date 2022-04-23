using System.Data;
using System.Data.SqlClient;
using FieldAgent.Core;

namespace FieldAgent.DAL;

public class ReportsRepository : IReportsRepository
{
    private readonly string _connectionString;
    public ReportsRepository(string ConnectionString)
    {
        _connectionString = ConnectionString;
    }
    public Response<List<TopAgentListItem>> GetTopAgents()
    {
        Response<List<TopAgentListItem>> response = new Response<List<TopAgentListItem>>();
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                var sql = "PensionList";
                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                var param = command.Parameters.Add("@AgentID", SqlDbType.Int);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<List<PensionListItem>> GetPensionList(int agencyId)
    {
        Response<List<PensionListItem>> response = new Response<List<PensionListItem>>();
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                var sql = "PensionList";
                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                var param = command.Parameters.Add("@AgencyId", SqlDbType.Int);
                param.Value = agencyId;
                
                connection.Open();
                var ret = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
    {
        throw new NotImplementedException();
    }
}