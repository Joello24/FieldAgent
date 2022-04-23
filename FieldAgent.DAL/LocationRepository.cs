using FieldAgent.Core;
using FieldAgent.Core.Entities;

namespace FieldAgent.DAL;

public class LocationRepository : ILocationRepository
{
    private DBFactory _dbFactory;
    public LocationRepository(DBFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public Response<Location> Insert(Location location)
    {
        Response<Location> response = new Response<Location>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data= db.Location.Add(location).Entity;
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

    public Response Update(Location location)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                db.Location.Update(location);
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

    public Response Delete(int locationId)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                var location = db.Location.FirstOrDefault(x => x.LocationId == locationId);
                db.Remove(location);
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

    public Response<Location> Get(int locationId)
    {
        Response<Location> response = new Response<Location>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Location.FirstOrDefault(x => x.LocationId == locationId);
                if (response.Data == null)
                {
                    response.Message = "Location not found";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
        }
        response.Success = true;
        return response;
    }

    public Response<List<Location>> GetByAgency(int agencyId)
    {
        Response<List<Location>> response = new Response<List<Location>>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Location.Where(x => x.AgencyId == agencyId).ToList();
                if(response.Data.Count==0)
                {
                    response.Message = "No Locations found";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
        }
        response.Success = true;
        return response;
    }
}