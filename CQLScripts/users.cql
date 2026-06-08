create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";

create table if not exists users(
	id			int ,
	password	text,
	name		text,
	email		text,
	phone		text,
	cellPhone	text,
	admin		boolean,
	address		text,
	birthdate	date,
	entryDate	date,
	status		boolean,
	
	primary key(email, password)
);
create table if not exists usersById(
	id	int,
	email text,
	primary key(id)
);

insert into users(id, name, email, password, admin, status) values(4, 'Administrator', 'admin@GrupoHotelero.com', 'admin', true, true) ;
delete from users where email='fatima@hotmail.com' and password='Fatima Salazar Peña';
select id, email, password from users;

insert into usersbyid(id, email) values(1, 'admin@GrupoHotelero.com');
insert into usersbyid(id, email) values(5, 'fatima@hotmail.com');
insert into usersbyid(id, email) values(4, 'edgar@hotmail.com');
insert into usersbyid(id, email) values(2, 'adrianmtz2003@hotmail.com');
select * from users;

INSERT INTO users(id, email, name, password, phone, cellphone, admin, address, birthdate, status, entryDate) VALUES (3,'fatima@hotmail.com', 'Fatima Salazar', '1234', '2424324', '41418989', False, 'Av. Pino Suárez 135, Centro, 64000 Monterrey, N.L.', '2007-02-06', True, '2025-05-09');



UPDATE users SET password = '1234', name = 'Fatima segunda', phone = '1', cellPhone = '{usuario.cellphone}', admin = {usuario.admin}, address = '{usuario.address}', birthdate = '{usuario.birthdate:yyyy-MM-dd}', entryDate = '{usuario.entrydate:yyyy-MM-dd}', status = {usuario.status} WHERE email = '{emailActual}' AND password = '{passwordActual}';
/*	Users


select writetime(id) from Users;

select * from users where email = 'adrianmtz2003@hotmail.com' and password = 'tamarindo443';
select * from users where id=1;

create index if not exists idIndex on users(id);
*/
  


/* 
drop index if exists idIndex;
drop table if exists Users;
drop table if exists usersById;
*/