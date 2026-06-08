create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";

drop table if exists reservations;
create table if not exists reservations(
	id				timeuuid,
	rfcClient		text,
	emailUser		text,
	dateReserved	timestamp,
	idHotel			int,
	nights			smallint,
	entryDate		date,
	numRooms		smallint,
	roomType		list<int>,
	numGuests		smallint,
	checkIn			timestamp,
	checkOut		timestamp,
	payForm			text,
	subTotal		int,
	
	
	primary key(id)
);
drop table if exists reservationsByRFC;
create table if not exists reservationsByRFC(
	id				timeuuid,
	rfcClient		text,
	primary key(rfcClient)
);

select * from users;
select * from clients;
select * from hoteles;

insert into reservations(emailUser, dateReserved, id, rfcClient,idHotel, nights, entryDate) values('admin@GrupoHotelero.com', toTimestamp(now()), now(), 'MATA030503ID8', 2, 6, toDate(now()));

select * from reservations;



/*
drop table if exists reservations; 
*/