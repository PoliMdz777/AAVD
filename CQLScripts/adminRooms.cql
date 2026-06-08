create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";


create table if not exists roomsHotel(
	idHotel		int,
	typeRoom	int,
	typeBed		int,
	numBeds		int,
	priceGuest	int,
	maxGuests	int,
	maxRooms	int,
	description	text,
	roomLevel	int,
	viewType	int,
	dimentions	text,
	
	primary key (idHotel, typeRoom)
);

