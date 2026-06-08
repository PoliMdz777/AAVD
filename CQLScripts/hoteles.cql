create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";

drop table if exists hoteles;
create table if not exists hoteles(
	id					int ,
	name				text,
	country				text,
	city				text,
	state				text,
	address				text,
	totalFloors			int,
	phone				text,
	views				text,
	turisticZone		boolean, 
	eventsSalon			boolean,
	numPools			int,
	aditionalService 	list<text>,
	roomTypes			list<text>,
	userId				int,
	dateRegistered		date,
	dateOperation		date,
	status				boolean,
	primary key(country, city, state, name)
);
drop table if exists hotelesById;
create table if not exists hotelesById(
	id		int,
	country text,
	city 	text,
	state	text,
	name 	text,
	primary key(id)
);
SELECT country, city, state, name FROM hotelesbyid where id = 1 limit 2;


/*
	 Hoteles
insert into Hoteles(id, name, country, city, state) values(1, 'The Palace', 'México', 'Oakland', 'Monterrey') if not exists;
insert into Hoteles(id, name, country, city, state) values(2, 'Holiday Inn', 'México', 'Nuevo León', 'Santiago') if not exists;
insert into Hoteles(id, name, country, city, state) values(3, 'El Presidencial', 'México', 'Nuevo León', 'Santiago') ;


*/

// the one before need to be restricted = smth
select * from hoteles where country = 'México' and city = 'Nuevo León' and state > 'M';
select * from hoteles where country = 'México' and city = 'Nuevo León' and state > 'Mz';

select MAX(id) from hotelesbyid;
select aditionalService from hoteles where country='a' and city='a' and state='a' and name='a';
SELECT country, city, state, name FROM hotelesbyid WHERE id = 2;

/*
*/