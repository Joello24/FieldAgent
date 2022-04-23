select
    Top(3)
    a.AgentId,
    a.FirstName,
    a.LastName,
    Count(a.AgentId) as MissionsCompleted
from Agent a
         join MissionAgent ma on ma.AgentId = a.AgentId
         join Mission m on m.MissionId = ma.MissionId
group by a.AgentId, a.FirstName, a.LastName
order by MissionsCompleted desc


create procedure PensionList(@agencyId int)
AS
BEGIN
    select *
    from Agent a
             join AgencyAgent aa on a.AgentId = aa.AgentId
             join Agency on aa.AgencyId = Agency.AgencyId
    where Agency.AgencyId = @agencyId and aa.SecurityClearanceId = 2
end