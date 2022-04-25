using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL;

public class MissionRepository : IMissionRepository
{
    private DBFactory _dbFactory;
    public MissionRepository(DBFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public Response<Mission> Insert(Mission mission)
    {
        Response<Mission> response = new Response<Mission>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Mission.Add(mission).Entity;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response Update(Mission mission)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                db.Mission.Update(mission);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response Delete(int missionId)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                var mission = db.Mission.Where(m=>m.MissionId==missionId).Include(ma => ma.MissionAgent).FirstOrDefault();
                db.Mission.Remove(mission);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<Mission> Get(int missionId)
    {
        Response<Mission> response = new Response<Mission>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Mission.Find(missionId);
                if (response.Data == null)
                {
                    response.Success = false;
                    response.Message = "No record found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<List<Mission>> GetByAgency(int agencyId)
    {
        Response<List<Mission>> response = new Response<List<Mission>>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Mission.Where(m => m.AgencyId == agencyId).ToList();
                if (response.Data == null)
                {
                    response.Success = false;
                    response.Message = "No record found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<List<Mission>> GetByAgent(int agentId)
    {
        Response<List<Mission>> response = new Response<List<Mission>>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = (from m in db.Mission
                                join ma in db.MissionAgent on m.MissionId equals ma.MissionId
                                where ma.AgentId == agentId
                                select m).ToList();
                if (response.Data.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No records found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }
}