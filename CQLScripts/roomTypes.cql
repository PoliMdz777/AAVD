create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";

drop table if exists roomtypes;
create table if not exists roomTypes(
	id			int,
	idHotel		int,
	name		text,
	bedTypes	text,
	bedNums		int,
	priceNight 	decimal,
	maxGuests	int,
	maxRooms	int,
	description	text,
	viewType	text,
	dimensions	text,
	level		text,
	amenites	list<text>,
	habInitNum	int,
	habEndNum	int, 
	emailUser	text,
	dateRegistered date,
	
	primary key(id)
);

drop table habitaciones;
create table habitaciones(
	id 	int,
	num	int,
	tipo	text,
	status	text,
	hotel	int,
	
	primary key(hotel, num)
);

select * from habitaciones;
select * from hoteles;

insert into roomTypes(id, name,priceNight, maxGuests, amenites) values(1, 'Luxury', 500, 3, ['Wi-Fi', 'Tv', 'Balcon', 'Frente al mar']);

select * from roomTypes;
truncate habitaciones;
SELECT * FROM habitaciones;

/*
drop table if exists roomTypes; 
*/