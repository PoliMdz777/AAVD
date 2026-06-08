

drop table if exists additionalService;
create table additionalService(
	id		int,
	name	text,
	price	int,
	
	
	primary key(name)
);

insert into additionalService(id, name, price) values(1, 'Wi-Fi', 100);
insert into additionalService(id, name, price) values(2, 'Masajes', 100);
insert into additionalService(id, name, price) values(3, 'Cafeteria', 100);
insert into additionalService(id, name, price) values(4, 'Servicio al cuarto', 100);
insert into additionalService(id, name, price) values(5, 'Guias turisticos', 100);
insert into additionalService(id, name, price) values(1, 'Salón de belleza', 100);
insert into additionalService(id, name, price) values(1, 'Clases de zumba', 100);
insert into additionalService(id, name, price) values(1, 'Comedor', 100); 

select * from additionalService;