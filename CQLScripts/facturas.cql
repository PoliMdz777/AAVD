create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";


create table if not exists checkOut(
	id				int,
	idReservation	text,
	clienName		text,
	hotel			text,
	numRooms		int,
	typeRooms		list<text>,
	Services 		list<int>,
	totalPrice		int,
	anticipo		int,	
	priceNight		int,
	numGuests		int,
	checkIn			date,
	checkOut		date,
	
	primary key(idReservation)
);

