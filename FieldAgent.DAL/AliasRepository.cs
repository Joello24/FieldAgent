using FieldAgent.Core;
using FieldAgent.Core.Entities;

namespace FieldAgent.DAL;

public class AliasRepository : IAliasRepository
{
    private DBFactory _dbFactory;
    public AliasRepository(DBFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public Response<Alias> Insert(Alias alias)
    {
        Response<Alias> response = new Response<Alias>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Alias.Add(alias).Entity;
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

    public Response Update(Alias alias)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                db.Alias.Update(alias);
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

    public Response Delete(int aliasId)
    {
        Response response = new Response();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                var alias = db.Alias.FirstOrDefault(x => x.AliasId == aliasId);
                db.Alias.Remove(alias);
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

    public Response<Alias> Get(int aliasId)
    {
        Response<Alias> response = new Response<Alias>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                response.Data = db.Alias.FirstOrDefault(x => x.AliasId == aliasId);
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

    public Response<List<Alias>> GetByAgent(int agentId)
    {
        Response<List<Alias>> response = new Response<List<Alias>>();
        List<Alias> aliasList = new List<Alias>();
        using (var db = _dbFactory.GetDbContext())
        {
            try
            {
                aliasList = db.Alias.Where(x => x.AgentId == agentId).ToList();
                response.Data = aliasList;
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