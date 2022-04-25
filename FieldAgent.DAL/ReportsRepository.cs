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
        List<TopAgentListItem> topAgents = new List<TopAgentListItem>();
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                var sql = "TopAgents";
                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TopAgentListItem topAgent = new TopAgentListItem();
                        topAgent.NameLastFirst = reader["FirstName"] + " " + reader["LastName"];
                        topAgent.CompletedMissionCount = int.Parse(reader["MissionsCompleted"].ToString());
                        topAgent.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        topAgents.Add(topAgent);
                    }
                }

                if (topAgents.Count==0)
                {
                    response.Success = false;
                    response.Message = "No Agents Found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
        response.Data = topAgents;
        response.Success = true;
        return response;
    }

    public Response<List<PensionListItem>> GetPensionList(int AgencyId)
    {
        Response<List<PensionListItem>> response = new Response<List<PensionListItem>>();
        List<PensionListItem> pensionList = new List<PensionListItem>();
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                var sql = ($"PensionList");
                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sql;
                var param = command.Parameters.Add("@AgencyId", SqlDbType.Int);
                param.Value = AgencyId;
                
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PensionListItem pension = new PensionListItem();
                        pension.AgencyName = reader["ShortName"].ToString();
                        pension.BadgeId = Guid.Parse(reader["BadgeId"].ToString());
                        pension.NameLastFirst = reader["FirstName"] + " " + reader["LastName"];
                        pension.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        if (string.IsNullOrEmpty(reader["DeactivationDate"].ToString()))
                        {
                            pension.DeactivationDate = null;
                        }
                        else
                        {
                            pension.DeactivationDate = DateTime.Parse(reader["DeactivationDate"].ToString());
                        }

                        pensionList.Add(pension);
                    }
                }
                if(pensionList.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No Agents Found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
        response.Data = pensionList;
        response.Success = true;
        return response;
    }

    public Response<List<ClearanceAuditListItem>> AuditClearance(int SecurityClearanceId, int AgencyId)
    {
        Response<List<ClearanceAuditListItem>> response = new Response<List<ClearanceAuditListItem>>();
        List<ClearanceAuditListItem> auditList = new List<ClearanceAuditListItem>();
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                var sql = ($"AuditList");
                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sql;
                var param = command.Parameters.Add("@AgencyId", SqlDbType.Int);
                param.Value = AgencyId;
                var param2 = command.Parameters.Add("@SecurityClearanceId", SqlDbType.Int);
                param2.Value = SecurityClearanceId;
                
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClearanceAuditListItem auditItem = new ClearanceAuditListItem();
                        auditItem.BadgeId = Guid.Parse(reader["BadgeId"].ToString());
                        auditItem.NameLastFirst = reader["FirstName"] + " " + reader["LastName"];
                        auditItem.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        auditItem.ActivationDate = DateTime.Parse(reader["ActivationDate"].ToString());
                        if (string.IsNullOrEmpty(reader["DeactivationDate"].ToString()))
                        {
                            auditItem.DeactivationDate = null;
                        }
                        else
                        {
                            auditItem.DeactivationDate = DateTime.Parse(reader["DeactivationDate"].ToString());
                        }

                        auditList.Add(auditItem);
                    }
                }

                if (auditList.Count ==0)
                {
                    response.Success = false;
                    response.Message = "No Agents Found";
                    return response;              
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
        response.Data = auditList;
        response.Success = true;
        return response;
    }
}