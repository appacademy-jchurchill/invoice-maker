
create table Client (
	Id int identity(1, 1) not null primary key,
	ClientName nvarchar(255) not null unique,
	IsActivated bit not null
);

go
