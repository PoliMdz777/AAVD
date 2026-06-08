create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";


create table if not exists Amenities(
	id		int,
	name	text,
	price	int,
	
	primary key(id)
);

insert into amenities(id,name,price) values(1,'Microondas',50);
insert into amenities(id,name,price) values(2,'Caja fuerte',50);
insert into amenities(id,name,price) values(3,'Refrigerador',50);
insert into amenities(id,name,price) values(4,'Secadora',50);
insert into amenities(id,name,price) values(5,'Toallas',50);
insert into amenities(id,name,price) values(6,'Clima',50);
insert into amenities(id,name,price) values(7,'Plancha',50);
insert into amenities(id,name,price) values(8,'Burro de planchar',50);
insert into amenities(id,name,price) values(9,'Ganchos',50);
insert into amenities(id,name,price) values(10,'Frigobar', 100);
insert into amenities(id,name,price) values(11,'Mesa de billar',70);
insert into amenities(id,name,price) values(12,'Cremas',50);
insert into amenities(id,name,price) values(13,'Acondicionador y shampoo', 50);
insert into amenities(id,name,price) values(14,'Juegos de mesa', 100);
insert into amenities(id,name,price) values(15,'Cafetera', 70);
insert into amenities(id,name,price) values(16,'Tostadora', 50);
insert into amenities(id,name,price) values(17,'Sartenes y toallas', 50);

select * from amenities;