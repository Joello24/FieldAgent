using FieldAgent.Core.Entities;

namespace FieldAgent.Core;

// expose no way to add or edit clearance records, they should be seeded by EF
public interface ISecurityClearanceRepository
{
    Response<SecurityClearance> Get(int securityClearanceId);
    Response<List<SecurityClearance>> GetAll();
}