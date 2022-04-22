using System.ComponentModel;
using FieldAgent.Core;
using FieldAgent.Core.Entities;

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
                var agency = db.Agency.Find(agencyId);
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