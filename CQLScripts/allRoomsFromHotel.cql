create keyspace if not exists "GrupoHotelero" with replication = {'class':'SimpleStrategy','replication_factor':1};
use "GrupoHotelero";

// tabla para todas las habitaciones de cada hotel