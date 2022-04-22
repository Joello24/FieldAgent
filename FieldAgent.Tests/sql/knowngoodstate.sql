CREATE PROCEDURE [SetKnownGoodState]
AS
BEGIN
    delete from AgencyAgent where 1=1;

    delete from Agency where 1=1;
    DBCC CHECKIDENT (Agency, RESEED, 0);
    delete from SecurityClearance where 1=1;
    DBCC CHECKIDENT (SecurityClearance, RESEED, 0);
    delete from Agent where 1=1;
    DBCC CHECKIDENT (Agent, RESEED, 0);



    insert into Agency ( ShortName, LongName)
    values ('FBI', 'Federal Bureau of Investigation');

    insert into Agent (FirstName, LastName, DateOfBirth, Height)
    values ('Jim', 'Halpert', '1995-01-01', 72);

    insert into SecurityClearance (SecurityClearanceName)
    values ('None'), ('Retired'),('Secret'),('Top Secret'),('Black Ops');

    insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive)
    values (1,1,1,'8bf8a3ac-3427-4788-9642-e73dfd2da162', '2022-04-21',null, 1);
END;
go
