
create table WorkDone (
    Id int identity(1, 1) not null primary key,
    ClientId int not null references Client (Id),
    WorkTypeId int not null references WorkType (Id),
    StartedOn datetimeoffset not null,
    EndedOn datetimeoffset null
);

go
