create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";

create table if not exists clients(
	rfc			text,
	email		text,
	name		text,
	lastname	text,
	country		text,
	city		text,
	state		text,
	phone		text,
	cellPhone	text,
	birthdate	date,
	civilStatus	text,
	
	primary key(rfc)
);
create table if not exists clientsByEmail(
	email text,
	rfc	text,
	primary key(email)
);
create table if not exists clientsByName(
	rfc	text,
	name text,
	lastname text,
	primary key(lastname, name)
);

select * from clients;
select * from clientsByEmail;
select * from clientsByName;

insert into clients(rfc, name, lastname, email, country, city, state, phone, cellphone, birthdate, civilStatus) 
values('MATA030503ID8','Adrian', 'Martinez Treviño', 'adrianmtzcoc@hotmail.com', 'México', 'Nuevo León', 'Apodaca', '8115759579', '8115759580', toDate('2003-05-03'), 'Soltero') if not exists;
/* Clients
insert into clients(rfc, name, lastname, email) values('MATA030503ID8','Adrian', 'Martinez Treviño', 'adrianmtz2003@hotmail.com') if not exists;
  

select * from clients where email = 'adrianmtz2003@hotmail.com';

delete from clients where rfc = 'MATA030503';
*/

/*
create index if not exists nameIndex on clients(name);
create index if not exists emailIndex on clients(email); 

drop index if exists nameIndex;
drop index if exists emailIndex;

drop table if exists clients;
drop table if exists clientsByEmail;
drop table if exists clientsByName;
*/