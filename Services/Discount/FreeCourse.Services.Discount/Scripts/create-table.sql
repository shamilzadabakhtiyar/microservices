create table discount(
	id serial primary key,
	userId varchar(200) unique not null,
	rate smallint not null,
	code varchar(50) not null,
	createdDate timestamp not null default current_timestamp
)