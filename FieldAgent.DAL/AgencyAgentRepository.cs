using FieldAgent.Core;
using FieldAgent.Core.Entities;

namespace FieldAgent.DAL;

public class AgencyAgentRepository : IAgencyAgentRepository
{
    private DBFactory _dbFactory;
    public AgencyAgentRepository(DBFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
    {
        Response<AgencyAgent> response = new Response<AgencyAgent>();
        using (var db = _dbFactory.GetDbContext())
        {
            try 
            {
                db.AgencyAgent.Add(agencyAgent);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
            response.Success = true;
            return response;
        }
    }

    public Response Update(AgencyAgent agencyAgent)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                db.AgencyAgent.Update(agencyAgent);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
                return response;
            }
            response.Success = true;
            return response;
        }
    }

    public Response Delete(int agencyid, int agentid)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                var agencyAgent = db.AgencyAgent.FirstOrDefault(x => x.AgencyId == agencyid && x.AgentId == agentid);
                db.AgencyAgent.Remove(agencyAgent);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
                return response;
            }
            response.Success = true;
            return response;
        }
    }

    public Response<AgencyAgent> Get(int agencyid, int agentid)
    {
        Response<AgencyAgent> response = new Response<AgencyAgent>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.AgencyAgent.FirstOrDefault(x => x.AgencyId == agencyid && x.AgentId == agentid);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
            response.Success = true;
            return response;
        }
    }

    public Response<List<AgencyAgent>> GetByAgency(int agencyId)
    {
        Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();
        List<AgencyAgent> data = new List<AgencyAgent>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                data = db.AgencyAgent.Where(x => x.AgencyId == agencyId).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
            response.Data = data;
            response.Success = true;
            return response;
        }
    }

    public Response<List<AgencyAgent>> GetByAgent(int agentId)
    {
        Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();
        List<AgencyAgent> data = new List<AgencyAgent>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                data = db.AgencyAgent.Where(x => x.AgentId == agentId).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
            response.Data = data;
            response.Success = true;
            return response;
        }
    }
}