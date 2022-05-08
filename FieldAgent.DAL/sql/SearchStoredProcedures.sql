CREATE procedure AuditList(@searchTerm varchar(50))
as
begin
    select
        *
    from Agent a
    where
        a.firstName like '%' + @searchTerm + '%'
end
go

