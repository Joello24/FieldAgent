using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL;

public class AgentRepository : IAgentRepository
{
    
    private DBFactory _dbFactory;
    public AgentRepository(DBFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public Response<Agent> Insert(Agent agent)
    {
        Response<Agent> response = new Response<Agent>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Add(agent).Entity;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;

                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response Update(Agent agent)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                db.Update(agent);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;

                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response Delete(int agentId)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                var agent = db.Agent.Where(x => x.AgentId == agentId).Include(x=>x.Alias).Include(x=>x.MissionAgent).Include(i=>i.AgencyAgent).FirstOrDefault();
                db.Remove(agent);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<Agent> Get(int agentId)
    {
        Response<Agent> response = new Response<Agent>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Agent.FirstOrDefault(x => x.AgentId == agentId);
                if(response.Data == null)
                {
                    response.Message = "Agent not found";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception e)
            {
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;

                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<List<Mission>> GetMissions(int agentId)
    {
        Response<List<Mission>> response = new Response<List<Mission>>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                var missions = (from m in db.Mission
                        join ma in db.MissionAgent on m.MissionId equals ma.MissionId
                        join a in db.Agent on ma.AgentId equals a.AgentId
                        where a.AgentId == agentId
                        select m
                    ).ToList();
                response.Data = missions;
                if(missions.Count == 0)
                {
                    response.Message = "No missions found";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception e)
            {
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;

                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }
}