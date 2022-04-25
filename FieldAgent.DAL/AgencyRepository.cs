using System.ComponentModel;
using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL;

public class AgencyRepository : IAgencyRepository
{
    private DBFactory _dbFactory;
    public AgencyRepository(DBFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public Response<Agency> Insert(Agency agency)
    {
        Response<Agency> response = new Response<Agency>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Agency.Add(agency).Entity;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response Update(Agency agency)
    {
        Response response = new Response();
        using(var db= _dbFactory.GetDbContext())
            try
            {
                db.Agency.Update(agency);
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

    public Response Delete(int agencyId)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                //agencyagent, missionagent, mission, location
                // Remove constraints first to avoid foreign key constraint violation
                var agency = db.Agency.Where(i=>i.AgencyId==agencyId).Include(i=>i.Mission).Include(i=>i.AgencyAgent).Include(i=>i.Location).FirstOrDefault();
                var mission = db.Mission.Where(i => i.AgencyId == agencyId).Include(i=>i.MissionAgent).ToList(); 
                
                foreach (var m in mission)
                {
                    db.Mission.Remove(m);
                }
                
                db.Agency.Remove(agency);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<Agency> Get(int agencyId)
    {
        Response<Agency> response = new Response<Agency>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Agency.Find(agencyId);
                if (response.Data == null)
                {
                    response.Success = false;
                    response.Message = "No record found";
                    return response;
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
                return response;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<List<Agency>> GetAll()
    {
        Response<List<Agency>> response = new Response<List<Agency>>();
        List<Agency> data = new List<Agency>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                data = db.Agency.ToList();
                if (data.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No records found";
                    return response;
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
                return response;
            }
        }
        response.Data = data;
        response.Success = true;
        return response;
    }
}