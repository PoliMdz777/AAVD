create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";

create table if not exists History(
	idClient		text,
	idHotel			int,
	idReservation	timeuuid,
	idBill			int,
	dateReservation	date,
	primary key((idClient, idHotel), idReservation)
);

create table if not exists ClientHistory(
	idClient		text,
	idHotel			int,
	idReservation	timeuuid,
	idBill			int,
	dateReservation	date,
	primary key(idClient, idReservation)
);

create table if not exists HotelHistory(
	idClient		text,
	idHotel			int,
	idReservation	timeuuid,
	idBill			int,
	dateReservation	date,
	primary key(idHotel, idReservation)
);



select * from clients;
select * from hoteles;

insert into hotelHistory(idClient,idHotel,idReservation) values('MATA030503ID8',2,now());

select * from hotelHistory where idClient = 'MATA030503ID8' and idHotel = 2;



/*
drop table if exists hotelHistory; 
*/