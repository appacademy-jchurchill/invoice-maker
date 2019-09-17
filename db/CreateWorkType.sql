
create table WorkType (
	Id int identity(1, 1) not null primary key,
	WorkTypeName nvarchar(255) not null unique,
	Rate decimal(5, 2) not null
);

go
