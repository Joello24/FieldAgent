using FieldAgent.Core;
using FieldAgent.Core.Entities;

namespace FieldAgent.DAL;

public class SecurityClearanceRepository : ISecurityClearanceRepository
{
    private DBFactory _dbFactory;
    public SecurityClearanceRepository(DBFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public Response<SecurityClearance> Get(int securityClearanceId)
    {
        Response<SecurityClearance> response = new Response<SecurityClearance>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.SecurityClearance.FirstOrDefault(x => x.SecurityClearanceId == securityClearanceId);
                if (response.Data == null)
                {
                    response.Success = false;
                    response.Message = "Security Clearance not found";
                    return response;
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<List<SecurityClearance>> GetAll()
    {
        Response<List<SecurityClearance>> response = new Response<List<SecurityClearance>>();
        List<SecurityClearance> securityClearanceList = new List<SecurityClearance>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                securityClearanceList = db.SecurityClearance.ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
        }
        response.Data = securityClearanceList;
        response.Success = true;
        return response;
    }
}