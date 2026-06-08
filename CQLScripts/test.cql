create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";

create table if not exists Users(
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
	state		boolean,
	
	primary key(email, password)
);
create index if not exists idIndex on users(id);

create table if not exists Hoteles(
	id					int ,
	name				text,
	country				text,
	city				text,
	state				text,
	address				text,
	totalFloors			text,
	phone				text,
	views				text,
	turisticZone		text, 
	eventsSalon			int,
	numPools			int,
	aditionalService 	list<int>,
	roomTypes			list<int>, 
	userId				int,
	dateRegistered		date,
	primary key(country, city, state, id)
);

create table if not exists roomTypes(
	id			int,
	name		text,
	bedTypes	text,
	bedNums		int,
	priceNight 	int,
	maxGuests	int,
	maxRooms	int,
	description	text,
	viewType	text,
	dimensions	text,
	amenites	list<text>, 
	
	primary key(id)
);

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

create table if not exists reservations(
	id				timeuuid,
	idClient		text,
	idHotel			int,
	nights			smallint,
	entryDate		date,
	numRooms		smallint,
	roomType		list<int>,
	numGuests		smallint,
	checkIn			date,
	checkOut		date,
	payForm			text,
	subTotal		int,
	
	primary key(id)
);

create table if not exists Clients(
	rfc			text,
	name		text,
	country		text,
	city		text,
	state		text,
	email		text,
	phone		text,
	cellPhone	text,
	birthdate	text,
	civilState	text,
	
	primary key(rfc, email)
);
create index if not exists nameIndex on clients(name);
create index if not exists emailIndex on clients(email); 

create table if not exists Amenities(
	id		int,
	name	text,
	price	int,
	
	primary key(id)
);

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



