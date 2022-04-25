CREATE procedure AuditList(@AgencyId int, @SecurityClearanceId int)
as
begin
    select
        *
    from Agent a
             join AgencyAgent aa on aa.AgentId = a.AgentId
             join SecurityClearance sc on sc.SecurityClearanceId = aa.SecurityClearanceId
    where aa.AgencyId = @AgencyId and sc.SecurityClearanceId = @SecurityClearanceId
end
go



CREATE procedure PensionList(@AgencyId int)
AS
BEGIN
    select *
    from Agent a
             join AgencyAgent aa on a.AgentId = aa.AgentId
             join Agency on aa.AgencyId = Agency.AgencyId
    where Agency.AgencyId = @AgencyId and aa.SecurityClearanceId = 2
end
go



CREATE procedure TopAgents
as
begin
    select
        Top(3)
        a.AgentId,
        a.FirstName,
        a.LastName,
        a.DateOfBirth,
        Count(a.AgentId) as MissionsCompleted
    from Agent a
             join MissionAgent ma on ma.AgentId = a.AgentId
             join Mission m on m.MissionId = ma.MissionId
    where m.ActualEndDate is not null
    group by a.AgentId, a.FirstName, a.LastName, a.DateOfBirth
    order by MissionsCompleted desc
end
go