using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Mapping;
using System.Configuration;
using System.Windows.Forms;
using Pantallas_alto_volumen_de_datos;
using System.Net;
using Pantallas_alto_volumen_de_datos.Entidades;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Globalization;
using System.Configuration;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Identity.Client;


namespace CassandraEnlaceServer
{
    class EnlaceCassandra
    {
        static private string _dbServer { set; get; }
        static private string _dbKeySpace { set; get; }
        static private Cluster _cluster;
        static private ISession _session;


        /*  ------------------  DATABASE CONNECTION  ------------------  */
        private static bool conectar()
        {
            // así NO hay que hacerlo....
            /*
            */
            _dbServer = "localhost";
            _dbKeySpace = "GrupoHotelero";

            // así SI:
            //_dbServer = ConfigurationManager.AppSettings["node"].ToString();
            //_dbKeySpace = ConfigurationManager.AppSettings["database"].ToString();
            
            //_cluster = Cluster.Builder().AddContactPoint(_dbServer).WithPoolingOptions(PoolingOptions.Create(ProtocolVersion.V3)
              //                  .SetHeartBeatInterval(1000).SetCoreConnectionsPerHost(HostDistance.Remote, 1)).WithReconnectionPolicy(new ConstantReconnectionPolicy(1000)).Build();


            _cluster = Cluster.Builder().AddContactPoint(_dbServer).Build();
            try
            {
                _cluster.Connect(_dbKeySpace);
                
                _session = _cluster.Connect(_dbKeySpace);
                //.WithReconnectionPolicy(new ConstantReconnectionPolicy(1000))
                return true;
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error de conexión a la base de datos: \n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(MessageBox.Show("Error de conexión a la base de datos \n¿Desea salir de la aplicación?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    Application.Exit();
                }


                return false;
            }
        }
        private static void desconectar()
        {
            _cluster.Dispose();
        }

        /*  ------------------  DATABASE INSERTION  ------------------  */


        public bool InsertUser(EmpleadosCassandra empleado)
        {
            if (string.IsNullOrEmpty(empleado.email) || string.IsNullOrEmpty(empleado.name) || string.IsNullOrEmpty(empleado.password))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            empleado.email = empleado.email.ToLower().Trim();

            try
            {
                
                List<EmpleadosCassandra> users = GetAllUsers();
                int id = 0;

                foreach (var user in users)
                {
                    if (user.email == empleado.email)
                    {
                        MessageBox.Show("El correo ya está registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (user.id > id)
                        id = user.id;
                }

                empleado.id = id + 1;

                conectar();

                string query = $@"
            INSERT INTO users (
                id, email, name, password, phone, cellphone, admin, address, birthdate, status, entryDate
            ) VALUES (
                {empleado.id}, '{empleado.email}', '{empleado.name}', '{empleado.password}', '{empleado.phone}',
                '{empleado.cellphone}', {empleado.admin}, '{empleado.address}', '{empleado.birthdate}', 
                {empleado.status}, '{empleado.entrydate:yyyy-MM-ddTHH:mm:ssZ}'
            );";

                _session.Execute(query);
                _session.Execute($"INSERT INTO usersById (id, email) VALUES ({empleado.id}, '{empleado.email}');");

                MessageBox.Show("Usuario registrado correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al registrar el usuario: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

       
        public bool InsertClient(Client cliente)
        {
            if (string.IsNullOrEmpty(cliente.rfc) || string.IsNullOrEmpty(cliente.name) ||
                string.IsNullOrEmpty(cliente.country) || string.IsNullOrEmpty(cliente.city) ||
                string.IsNullOrEmpty(cliente.state) || string.IsNullOrEmpty(cliente.email))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                List<Client> clients = GetAllClients();
                foreach (Client client in clients)
                {
                    if (client.rfc == cliente.rfc)
                    {
                        MessageBox.Show("El RFC ya está registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                string query = $@"
                 INSERT INTO clients (
                     rfc, name, country, city, state, email, phone, cellPhone,
                     birthdate, civilstatus, lastname, status, user, registro
                 ) VALUES (
                     '{cliente.rfc}', '{cliente.name}', '{cliente.country}', '{cliente.city}', '{cliente.state}', 
                     '{cliente.email}', '{cliente.phone}', '{cliente.cellphone}', '{cliente.birthdate}', 
                     '{cliente.civilstatus}', '{cliente.lastname}', {cliente.status}, '{cliente.user}', '{cliente.registro:yyyy-MM-dd HH:mm:ss}'
                 );";


                string query2 = $"INSERT INTO clientsbyemail (rfc, email) VALUES ('{cliente.rfc}', '{cliente.email}');";
                string query3 = $"INSERT INTO clientsbyname (rfc, name, lastname) VALUES ('{cliente.rfc}', '{cliente.name}', '{cliente.lastname}');";

                conectar();
                _session.Execute(query);
                _session.Execute(query2);
                _session.Execute(query3);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al registrar el cliente: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }

            MessageBox.Show("Cliente registrado correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }


        public bool InsertHotel(Hotel newHotel)
        {
            if (string.IsNullOrEmpty(newHotel.name) || string.IsNullOrEmpty(newHotel.country) || string.IsNullOrEmpty(newHotel.city) || string.IsNullOrEmpty(newHotel.state) || string.IsNullOrEmpty(newHotel.address))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {

                List<Hotel> hotels = GetAllHotels();
                foreach (Hotel hotel in hotels)
                {
                    if (hotel.country == newHotel.country && hotel.city == newHotel.city && hotel.state == newHotel.state && hotel.name == newHotel.name)
                    {
                        MessageBox.Show("El Hotel ya está registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                List<Hotel> allHotels = GetAllHotels();
                //newHotel.id = 0; // Reset the ID to 0 before checking for existing IDs
                foreach (Hotel hotel in allHotels)
                {
                    if (hotel.id > 0 && hotel.id > newHotel.id)
                    {
                        newHotel.id = hotel.id;
                    }
                }
                //string queryId = "SELECT MAX(id) FROM hoteles;"; // Get the maximum ID from the hoteles table
                //conectar();
                //IMapper mapper = new Mapper(_session);
                //Cassandra.RowSet result = _session.Execute(queryId);
                //if (result != null)
                //{
                //    newHotel.id = result.First().GetValue<int>(0); // Get the maximum ID value
                //}

                newHotel.id++; // Increment the ID for the new hotel
                string aditionalServiceCql = newHotel.aditionalService != null ? "[" + string.Join(", ", newHotel.aditionalService.Select(s => $"'{s}'")) + "]" : "[]";
                string roomTypesCql = newHotel.roomTypes != null ? "[" + string.Join(", ", newHotel.roomTypes.Select(s => $"'{s}'")) + "]" : "[]";
                IMapper mapper = new Mapper(_session);
                string query = $"INSERT INTO hoteles(id, name, country, city, state, address, totalFloors, phone, views, turisticZone, eventssalon, numpools, aditionalservice, roomtypes, userid, dateregistered, dateOperation, status) VALUES ({newHotel.id},'{newHotel.name}','{newHotel.country}','{newHotel.city}','{newHotel.state}','{newHotel.address}', {newHotel.totalFloors}, '{newHotel.phone}', '{newHotel.views}', {newHotel.turisticZone}, {newHotel.eventsSalon}, {newHotel.numPools}, {aditionalServiceCql}, {roomTypesCql}, {newHotel.userId}, '{newHotel.dateRegistered}', '{newHotel.dateOperation}', {newHotel.status});";
                conectar();
                mapper = new Mapper(_session);
                _session.Execute(query);
                query = $"INSERT INTO hotelesById(id, name, country, city, state) VALUES ({newHotel.id},\'{newHotel.name}\',\'{newHotel.country}\',\'{newHotel.city}\',\'{newHotel.state}\');";
                //mapper = new Mapper(_session);
                _session.Execute(query);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al registrar el hotel: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }

            //mapper.Insert(new User { email = email, name = name, password = password, phone = phone, cellPhone = cellphone, admin = admin, address = address, birthdate = date });
            //}
            //else
            //{
            //    MessageBox.Show("Error de conexión a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            MessageBox.Show("Hotel registrado correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        internal bool InsertRoomType(roomTypes tipoHabitacion)
        {
            if (string.IsNullOrEmpty(tipoHabitacion.name) || string.IsNullOrEmpty(tipoHabitacion.viewType) || string.IsNullOrEmpty(tipoHabitacion.dimensions) || tipoHabitacion.bedNums <= 0 || tipoHabitacion.maxGuests <= 0 || tipoHabitacion.maxRooms <= 0)
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                List<roomTypes> roomTypesList = GetAllRoomTypes();
                foreach (roomTypes roomType in roomTypesList)
                {
                    if (roomType.name == tipoHabitacion.name && roomType.idHotel == tipoHabitacion.idHotel)
                    {
                        MessageBox.Show("El tipo de habitación ya está registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                string queryId = "SELECT MAX(id) FROM roomtypes;"; // Get the maximum ID from the roomtypes table
                conectar();
                IMapper mapper = new Mapper(_session);
                Cassandra.RowSet result = _session.Execute(queryId);
                if (result != null)
                {
                    var row = result.First();
                    if (!row.IsNull(0))
                        tipoHabitacion.id = row.GetValue<int>(0);
                    else
                        tipoHabitacion.id = 0;
                }
                else
                {
                    tipoHabitacion.id = 0;
                }
                tipoHabitacion.id++; // Increment the ID for the new room type
                string amenitiesCql = tipoHabitacion.amenites != null ? "[" + string.Join(", ", tipoHabitacion.amenites.Select(s => $"'{s}'")) + "]" : "[]";
                string query = $"INSERT INTO roomtypes(id, idHotel, name, bedTypes, bedNums, priceNight, maxGuests, maxRooms, description, viewType, dimensions, level, amenites, habInitNum, habEndNum, emailUser, dateRegistered) VALUES ({tipoHabitacion.id}, {tipoHabitacion.idHotel}, '{tipoHabitacion.name}', '{tipoHabitacion.bedTypes}', {tipoHabitacion.bedNums}, {tipoHabitacion.priceNight}, {tipoHabitacion.maxGuests}, {tipoHabitacion.maxRooms}, '{tipoHabitacion.description}', '{tipoHabitacion.viewType}', '{tipoHabitacion.dimensions}', '{tipoHabitacion.level}', {amenitiesCql}, {tipoHabitacion.habInitNum}, {tipoHabitacion.habEndNum}, '{tipoHabitacion.emailUser}', '{tipoHabitacion.dateRegistered}');";

                _session.Execute(query);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al registrar el tipo de habitación: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

//        public bool GuardarReservacion(Reservation reserva)
//        {
//            try
//            {
//                conectar();

//                string roomTypes = reserva.RoomType != null ? "[" + string.Join(", ", reserva.RoomType.Select(s => $"'{s}'")) + "]" : "[]";


//                string cql = $@"
//                INSERT INTO reservations (
//                    id, rfcClient, emailUser, dateReserved,
//                    idHotel, nights, entryDate, endDate, numRooms,
//                    roomType, numGuests, checkIn, checkOut,
//                    payForm, subTotal, anticipo, total
//                ) VALUES (
//                    {reserva.Id}, '{reserva.RfcClient}', '{reserva.EmailUser}', '{reserva.DateReserved:yyyy-MM-ddTHH:mm:ssZ}', 
//{reserva.IdHotel}, {reserva.Nights}, 
//'{reserva.EntryDate}', '{reserva.EndDate}',{reserva.NumRooms}, {roomTypes}, {reserva.NumGuests}, '{reserva.CheckIn:yyyy-MM-ddTHH:mm:ssZ}', 
//'{reserva.CheckOut:yyyy-MM-ddTHH:mm:ssZ}', '{reserva.PayForm}', {reserva.SubTotal}, {reserva.anticipo}, {reserva.total});";

//                //'{empleado.entrydate:yyyy-MM-ddTHH:mm:ssZ}'
//                _session.Execute(cql);
//                desconectar();
//                conectar();

//                cql = $"INSERT INTO reservationsByRFC (id, rfcClient) VALUES ({reserva.Id}, '{reserva.RfcClient}');";
//                _session.Execute(cql);
//                desconectar();
//                conectar();

//                cql = $"INSERT INTO reservationsByName (id, name) VALUES ({reserva.Id}, '{reserva.name}');";
//                _session.Execute(cql);
//                return true;
//            }
//            catch (Exception ex)
//            {
//                // Manejo de errores, puede ser log o simplemente mostrar el error
//                Console.WriteLine("Error al guardar la reservación: " + ex.Message);
//                return false;
//            }
//            finally
//            {
//                desconectar();
//            }
//        }



        /*  ------------------  DATABASE DELETION  ------------------  */

        public bool deleteRoomTypeFromHotel(int roomtypeID, int idHotel, string roomtype)
        {
            List<Habitaciones> habitaciones = new List<Habitaciones>();
            // Deleting all rooms of a specific type from a hotel
            try
            {
                conectar();
                string query = $"SELECT * FROM habitaciones WHERE hotel = {idHotel} and tipo = '{roomtype}' ALLOW FILTERING;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<Habitaciones> AllHabOfRoomType = mapper.Fetch<Habitaciones>(query);
                habitaciones = AllHabOfRoomType.ToList();
                desconectar();
                if (habitaciones.Count > 0)
                {
                    conectar();
                    foreach (Habitaciones hab in habitaciones)
                    {
                        string deleteQuery = $"DELETE FROM habitaciones WHERE hotel = {hab.hotel} and num = {hab.num};";
                        _session.Execute(deleteQuery);
                    }                      
                }

            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al eliminar las habitaciones {roomtype} del hotel, favor de contactar soporte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(MessageBox.Show($"¿Desea ver el error?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes){
                    MessageBox.Show(e.Message, "Error Detallado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
            finally
            {
                desconectar();
            }
            // Deleting the room type itself
            try
            {
                conectar();
                string query = $"DELETE FROM roomTypes WHERE id = {roomtypeID};";
                _session.Execute(query);
                MessageBox.Show($"Tipo de habitación {roomtype} eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al eliminar el tipo de habitación, favor de contactar soporte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (MessageBox.Show($"¿Desea ver el error?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes){
                    MessageBox.Show(e.Message, "Error Detallado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            finally
            {
                desconectar();
            }


        }
        public static int DarDeBajaUsuario(int idUsuario)
        {
            if (!conectar())
                return 0;

            try
            {
                // Buscar el email y password asociados al ID
                string consulta = $"SELECT email FROM usersById WHERE id = {idUsuario};";
                var fila = _session.Execute(consulta).FirstOrDefault();

                if (fila == null)
                {
                    MessageBox.Show("No se encontró el usuario con ese ID.");
                    return 0;
                }

                string email = fila.GetValue<string>("email");

                // Buscar el password actual del usuario
                string consultaPwd = $"SELECT password FROM users WHERE email = '{email}';";
                var filaPwd = _session.Execute(consultaPwd).FirstOrDefault();

                if (filaPwd == null)
                {
                    MessageBox.Show("No se pudo obtener el password del usuario.");
                    return 0;
                }

                string password = filaPwd.GetValue<string>("password");

                // Realizar baja lógica (actualizar status a false)
                string query = $"UPDATE users SET status = false WHERE email = '{email}' AND password = '{password}';";
                _session.Execute(query);

                return 1; // éxito
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al dar de baja al usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                desconectar();
            }
        }

        internal void EliminarHabitacionesPorTipo(string idTipo, int idHotel)
        {
            try
            {
                conectar();
                string query = $"DELETE FROM habitaciones WHERE hotel = {idHotel} AND tipo = '{idTipo}';";
                _session.Execute(query);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al eliminar habitaciones por tipo: " + e.Message);
            }
            finally
            {
                desconectar();
            }
        }


        public bool HabilitarUsuario(int idUsuario)
        {
            if (!conectar())
                return false;

            try
            {
                // Buscar el email y password asociados al ID
                string consulta = $"SELECT email FROM usersById WHERE id = {idUsuario};";
                var fila = _session.Execute(consulta).FirstOrDefault();

                if (fila == null)
                {
                    MessageBox.Show("No se encontró el usuario con ese ID.");
                    return false;
                }

                string email = fila.GetValue<string>("email");

                // Obtener password
                string consultaPwd = $"SELECT password FROM users WHERE email = '{email}';";
                var filaPwd = _session.Execute(consultaPwd).FirstOrDefault();

                if (filaPwd == null)
                {
                    MessageBox.Show("No se pudo obtener el password del usuario.");
                    return false;
                }

                string password = filaPwd.GetValue<string>("password");

                // Cambiar status a true
                string query = $"UPDATE users SET status = true WHERE email = '{email}' AND password = '{password}';";
                _session.Execute(query);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al habilitar el usuario: " + ex.Message);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

        internal bool DeleteRoomType(int idRoomType, int idHotel)
        {
            try
            {
                conectar();

                string deleteQuery = $"DELETE FROM roomtypes WHERE id = {idRoomType} AND idHotel = {idHotel};";

                _session.Execute(deleteQuery);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al eliminar el tipo de habitación: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }


        public bool DeleteClient(string rfc)
        {
            if (conectar())
            {
                try
                {
                    //UPDATE con cambio de status (baja lógica)
                    string query = $"UPDATE clients SET status = false WHERE rfc = '{rfc}';";

                    _session.Execute(query);

                    MessageBox.Show("Cliente dado de baja correctamente (baja lógica).", "Baja lógica", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al dar de baja el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    desconectar();
                }
            }
            return false;
        }

        public int hotelDardeBaja(int idHotel)
        {
            if (idHotel < 0)
            {
                MessageBox.Show("Por favor, seleccione un hotel", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            hotelsbyid hotelInfo = null;
            if (conectar())
            {
                string query = $"SELECT country, city, state, name FROM hotelesbyid where id = {idHotel} limit 1;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<hotelsbyid> hotels = mapper.Fetch<hotelsbyid>(query);
                hotelInfo = hotels.FirstOrDefault();
                desconectar();

            }

            if (conectar())
            {

                string query = $"UPDATE hoteles SET status = false WHERE country = '{hotelInfo.country}' AND city = '{hotelInfo.city}' AND state = '{hotelInfo.state}' AND name = '{hotelInfo.name}' IF EXISTS;";
                IMapper mapper = new Mapper(_session);
                Cassandra.RowSet exitcode = _session.Execute(query);
                if (exitcode.IsFullyFetched)
                {
                    MessageBox.Show("Hotel dado de baja logica exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                desconectar();
                return 1;
            }
            return 0;
        }

        public int DeleteHotel(int idHotel)
        {
            if (idHotel < 0)
            {
                MessageBox.Show("Por favor, seleccione un hotel", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            hotelsbyid hotelInfo = null;
            if (conectar())
            {
                string query = $"SELECT country, city, state, name FROM hotelesbyid where id = {idHotel} limit 1;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<hotelsbyid> hotels = mapper.Fetch<hotelsbyid>(query);
                hotelInfo = hotels.FirstOrDefault();
                desconectar();

            }

            if (conectar())
            {

                string query = $"delete from hoteles WHERE country = '{hotelInfo.country}' AND city = '{hotelInfo.city}' AND state = '{hotelInfo.state}' AND name = '{hotelInfo.name}';";
                IMapper mapper = new Mapper(_session);
                Cassandra.RowSet exitcode = _session.Execute(query);
                if (exitcode.IsFullyFetched)
                {
                    MessageBox.Show("Hotel eliminado correctamente", "Hotel eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                desconectar();
                return 1;
            }
            return 0;
        }

        /*  ------------------  DATABASE UPDATE  ------------------  */

        public bool UpdateClient(Client c)
        {
            if (!conectar())
                return false;

            try
            {
                // 1. Obtener datos actuales del cliente
                var getQuery = _session.Prepare("SELECT * FROM clients WHERE rfc = ?");
                var clienteActual = _session.Execute(getQuery.Bind(c.rfc)).FirstOrDefault();

                if (clienteActual == null) return false;

                string emailAnterior = clienteActual.GetValue<string>("email");
                string nameAnterior = clienteActual.GetValue<string>("name");
                string lastnameAnterior = clienteActual.GetValue<string>("lastname");

                // 2. Actualizar tabla principal (clients)
                var updateClientQuery = _session.Prepare(@"
            UPDATE clients SET 
                email = ?,
                name = ?,
                lastname = ?,
                country = ?,
                city = ?,
                state = ?,
                phone = ?,
                cellPhone = ?,
                birthdate = ?,
                civilStatus = ?,
                user = ?,
                registro = toTimestamp(now())
            WHERE rfc = ?");

                _session.Execute(updateClientQuery.Bind(
                    c.email,
                    c.name,
                    c.lastname,
                    c.country,
                    c.city,
                    c.state,
                    c.phone,
                    c.cellphone,
                    c.birthdate,
                    c.civilstatus,
                    c.user,
                    c.rfc));

                // 3. Manejar actualización en clientsByEmail
                if (emailAnterior != c.email)
                {
                    // Eliminar registro antiguo si el email cambió
                    var deleteEmailQuery = _session.Prepare("DELETE FROM clientsByEmail WHERE email = ?");
                    _session.Execute(deleteEmailQuery.Bind(emailAnterior));
                }

                // Insertar/actualizar en clientsByEmail
                var insertEmailQuery = _session.Prepare("INSERT INTO clientsByEmail (email, rfc) VALUES (?, ?)");
                _session.Execute(insertEmailQuery.Bind(c.email, c.rfc));

                // 4. Manejar actualización en clientsByName
                if (nameAnterior != c.name || lastnameAnterior != c.lastname)
                {
                    // Eliminar registro antiguo si el nombre o apellido cambiaron
                    var deleteNameQuery = _session.Prepare("DELETE FROM clientsByName WHERE lastname = ? AND name = ?");
                    _session.Execute(deleteNameQuery.Bind(lastnameAnterior, nameAnterior));
                }

                // Insertar/actualizar en clientsByName
                var insertNameQuery = _session.Prepare("INSERT INTO clientsByName (rfc, name, lastname) VALUES (?, ?, ?)");
                _session.Execute(insertNameQuery.Bind(c.rfc, c.name, c.lastname));

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }
        //public bool UpdateClient(Client c)
        //{
        //    try
        //    {
        //        conectar();

        //        // Obtener cliente actual desde la tabla principal
        //        Row clienteActual = _session.Execute($"SELECT * FROM clients WHERE rfc = '{c.rfc}'").FirstOrDefault();
        //        if (clienteActual == null) return false;

        //        string emailAnterior = clienteActual.GetValue<string>("email");
        //        string nameAnterior = clienteActual.GetValue<string>("name");
        //        string lastnameAnterior = clienteActual.GetValue<string>("lastname");

        //        // Actualizar tabla principal
        //        string query = $@"
        //    UPDATE clients SET 
        //        email = '{c.email}',
        //        name = '{c.name}',
        //        lastname = '{c.lastname}',
        //        country = '{c.country}',
        //        city = '{c.city}',
        //        state = '{c.state}',
        //        phone = '{c.phone}',
        //        cellPhone = '{c.cellphone}',
        //        birthdate = '{c.birthdate}',
        //        civilStatus = '{c.civilstatus}',
        //        user = '{c.user}',
        //        registro = toTimestamp(now())
        //    WHERE rfc = '{c.rfc}';";

        //        _session.Execute(query);

        //        // Actualizar tabla clientsByEmail
        //        if (emailAnterior != c.email)
        //        {
        //            _session.Execute($"DELETE FROM clientsByEmail WHERE email = '{emailAnterior}';");
        //        }
        //        _session.Execute($"INSERT INTO clientsByEmail (email, rfc) VALUES ('{c.email}', '{c.rfc}');");

        //        // Actualizar tabla clientsByName
        //        if (nameAnterior != c.name || lastnameAnterior != c.lastname)
        //        {
        //            _session.Execute($"DELETE FROM clientsByName WHERE lastname = '{lastnameAnterior}' AND name = '{nameAnterior}';");
        //        }
        //        _session.Execute($"INSERT INTO clientsByName (rfc, name, lastname) VALUES ('{c.rfc}', '{c.name}', '{c.lastname}');");

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al actualizar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        desconectar();
        //    }
        //}


        //public bool ActualizarUsuario(EmpleadosCassandra usuario)
        //{
        //    if (!conectar())
        //        return false;

        //    try
        //    {
        //        // 1. Obtener email actual desde usersById
        //        string idQuery = $"SELECT email FROM usersById WHERE id = {usuario.id};";
        //        var filaId = _session.Execute(idQuery).FirstOrDefault();
        //        if (filaId == null) return false;

        //        string emailActual = filaId.GetValue<string>("email");

        //        // 2. Obtener password actual (necesario para el WHERE)
        //        string passwordQuery = $"SELECT password FROM users WHERE email = '{emailActual}' LIMIT 1;";
        //        var filaPassword = _session.Execute(passwordQuery).FirstOrDefault();
        //        if (filaPassword == null) return false;

        //        string passwordActual = filaPassword.GetValue<string>("password");

        //        // 3. Ejecutar el UPDATE
        //        string updateQuery = $"UPDATE users SET name = '{usuario.name}', phone = '{usuario.phone}', cellPhone = '{usuario.cellphone}', admin = {usuario.admin}, address = '{usuario.address}', birthdate = '{usuario.birthdate:yyyy-MM-dd}', entryDate = '{usuario.entrydate:yyyy-MM-dd}', status = {usuario.status} WHERE email = '{emailActual}' AND password = '{passwordActual}';";
        //        _session.Execute(updateQuery);

        //        // 4. Si el email cambió, actualizar en usersById
        //        if (emailActual != usuario.email)
        //        {
        //            string updateIdQuery = $"UPDATE usersById SET email = '{usuario.email}' WHERE id = {usuario.id};";
        //            _session.Execute(updateIdQuery);
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al actualizar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        desconectar();
        //    }
        //}

        public bool ActualizarUsuario(EmpleadosCassandra usuario)
        {
           

            try
            {
                conectar();
                   

                // 1. Obtener email actual desde usersById
                string idQuery = $"SELECT email FROM usersById WHERE id = {usuario.id};";
                var filaId = _session.Execute(idQuery).FirstOrDefault();
                if (filaId == null) return false;

                string emailActual = filaId.GetValue<string>("email");

                // 2. Obtener password actual (necesario para DELETE del registro anterior)
                string passwordQuery = $"SELECT password FROM users WHERE email = '{emailActual}' LIMIT 1;";
                var filaPassword = _session.Execute(passwordQuery).FirstOrDefault();
                if (filaPassword == null) return false;

                desconectar();



                string passwordActual = filaPassword.GetValue<string>("password");


                EmpleadosCassandra usuarioActualizar = GetUserById(usuario.id);


                if (usuarioActualizar.old_password1 != null)
                {
                    usuario.old_password2 = usuarioActualizar.old_password1;
                }
                else
                {
                    usuario.old_password2 = "";
                }

                conectar();

                usuario.old_password1 = usuarioActualizar.password;

                if (usuario.password == usuarioActualizar.old_password1 || usuario.password == usuarioActualizar.old_password2)
                {
                    MessageBox.Show("La contraseña no puede ser igual que la anterior", "Usuario error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                string insertQuery1 = $@"
              INSERT INTO users (id, email, password, name, phone, cellPhone, admin, address, birthdate, entryDate, status, old_password1, old_password2) 
              VALUES ({usuario.id}, '{usuario.email}', '{usuario.password}', '{usuario.name}', '{usuario.phone}', '{usuario.cellphone}', {usuario.admin},
               '{usuario.address}', '{usuario.birthdate:yyyy-MM-dd}', '{usuario.entrydate:yyyy-MM-dd}', {usuario.status} , '{usuario.old_password1}',
               '{usuario.old_password2}');";

                // 3. Insertar el nuevo registro con la nueva contraseña (puede ser igual o distinta)
                //    string insertQuery = $@"
                //INSERT INTO users (
                //    id, email, password, name, phone, cellPhone, admin, address, birthdate, entryDate, status
                //) VALUES (
                //    {usuario.id}, '{usuario.email}', '{usuario.password}', '{usuario.name}', 
                //    '{usuario.phone}', '{usuario.cellphone}', {usuario.admin}, '{usuario.address}', 
                //    '{usuario.birthdate:yyyy-MM-dd}', '{usuario.entrydate:yyyy-MM-dd}', {usuario.status}
                //);";
                _session.Execute(insertQuery1);






                // 4. Eliminar el registro anterior (solo si email o password cambió)
                if (usuario.email != emailActual || usuario.password != passwordActual)
                {
                    string deleteQuery = $"DELETE FROM users WHERE email = '{emailActual}' AND password = '{passwordActual}';";
                    _session.Execute(deleteQuery);
                }

                // 5. Si el email cambió, actualizarlo en usersById
                if (usuario.email != emailActual)
                {
                    string updateIdQuery = $"UPDATE usersById SET email = '{usuario.email}' WHERE id = {usuario.id};";
                    _session.Execute(updateIdQuery);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }


        internal bool UpdateRoomType(roomTypes tipoHabitacion)
        {
            if (string.IsNullOrEmpty(tipoHabitacion.name) || string.IsNullOrEmpty(tipoHabitacion.viewType) || string.IsNullOrEmpty(tipoHabitacion.dimensions) || tipoHabitacion.bedNums <= 0 || tipoHabitacion.maxGuests <= 0 || tipoHabitacion.maxRooms <= 0)
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                conectar();

                string amenitiesCql = tipoHabitacion.amenites != null ? "[" + string.Join(", ", tipoHabitacion.amenites.Select(s => $"'{s}'")) + "]" : "[]";

                string updateQuery = $@"
        UPDATE roomtypes SET 
            bedTypes = '{tipoHabitacion.bedTypes}',
            bedNums = {tipoHabitacion.bedNums},
            priceNight = {tipoHabitacion.priceNight},
            maxGuests = {tipoHabitacion.maxGuests},
            maxRooms = {tipoHabitacion.maxRooms},
            description = '{tipoHabitacion.description}',
            viewType = '{tipoHabitacion.viewType}',
            dimensions = '{tipoHabitacion.dimensions}',
            level = '{tipoHabitacion.level}',
            amenites = {amenitiesCql},
            habInitNum = {tipoHabitacion.habInitNum},
            habEndNum = {tipoHabitacion.habEndNum},
            emailUser = '{tipoHabitacion.emailUser}',
            dateRegistered = '{tipoHabitacion.dateRegistered}'
        WHERE id = {tipoHabitacion.id};";

                _session.Execute(updateQuery);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al actualizar el tipo de habitación: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }


        public string ObtenerPasswordActual(int idUsuario)
        {
            if (!conectar()) return "";

            try
            {
                var filaEmail = _session.Execute($"SELECT email FROM usersById WHERE id = {idUsuario};").FirstOrDefault();
                if (filaEmail == null) return "";

                string email = filaEmail.GetValue<string>("email");

                var filaPwd = _session.Execute($"SELECT password FROM users WHERE email = '{email}';").FirstOrDefault();
                return filaPwd?.GetValue<string>("password") ?? "";
            }
            finally
            {
                desconectar();
            }
        }

        public bool UpdateHotel(Hotel hotelMod)
        {
            string aditionalServiceCql = hotelMod.aditionalService != null ? "[" + string.Join(", ", hotelMod.aditionalService.Select(s => $"'{s}'")) + "]" : "[]";
            string roomTypesCql = hotelMod.roomTypes != null ? "[" + string.Join(", ", hotelMod.roomTypes.Select(s => $"'{s}'")) + "]" : "[]";

            string query = $"UPDATE hoteles SET address = '{hotelMod.address}', totalFloors = {hotelMod.totalFloors}, phone = '{hotelMod.phone}', views = '{hotelMod.views}', turisticZone = {hotelMod.turisticZone}, eventsSalon = {hotelMod.eventsSalon}, numPools = {hotelMod.numPools}, aditionalService = {aditionalServiceCql}, roomTypes = {roomTypesCql}, userId = {hotelMod.userId}, dateRegistered = '{hotelMod.dateRegistered}', dateOperation = '{hotelMod.dateOperation}', status = {hotelMod.status} WHERE country = '{hotelMod.country}' AND city = '{hotelMod.city}' AND state = '{hotelMod.state}' AND name = '{hotelMod.name}' IF EXISTS";

            conectar();
            IMapper mapper = new Mapper(_session);
            try
            {
                _session.Execute(query);
                MessageBox.Show("Hotel actualizado correctamente", "Usuario actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al actualizar el hotel: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }

            return true;
        }

        public bool CancelarReservacion(Guid idReserva)
        {
            
            if (!conectar()) return false;

            try
            {
                // Consultar el estado actual de la reservación
                string consultaEstado = $"SELECT status FROM reservations WHERE id = {idReserva};";
                var resultado = _session.Execute(consultaEstado).FirstOrDefault();

                if (resultado == null)
                {
                    MessageBox.Show("No se encontró la reservación.");
                    return false;
                }

                string estadoActual = resultado["status"]?.ToString()?.Trim().ToLower();

                if (estadoActual == "cancelada" || estadoActual == "checkin")
                {
                    MessageBox.Show("No se puede cancelar una reservación que ya está cancelada o con Check-In realizado.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Proceder a cancelar
                string query = $"UPDATE reservations SET status = 'Cancelada' WHERE id = {idReserva};";
                _session.Execute(query);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar la reservación: " + ex.Message);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

        public bool RealizarCheckIn(Guid idReserva)
        {
            if (!conectar()) return false;

            try
            {
                // 1. Obtener datos de la reservación: status y fecha de entrada
                string queryEstado = $"SELECT status, entrydate FROM reservations WHERE id = {idReserva};";
                var resultado = _session.Execute(queryEstado);

                if (resultado.IsExhausted())
                {
                    MessageBox.Show("No se encontró la reservación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                var fila = resultado.First();
                string estadoActual = fila.GetValue<string>("status");
                var entryDate = fila.GetValue<LocalDate>("entrydate").ToDateTimeOffset().Date;

                // 2. Validar estado
                if (estadoActual == "Cancelada")
                {
                    MessageBox.Show("No se puede hacer check-in a una reservación cancelada.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (estadoActual == "CheckIn")
                {
                    MessageBox.Show("La reservación ya se encuentra con check-in realizado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                // 3. Validar que la fecha actual coincida con la fecha de entrada
                DateTime hoy = DateTime.Today;
                if (entryDate != hoy)
                {
                    MessageBox.Show($"El check-in solo se puede realizar el día de entrada.\nFecha de entrada: {entryDate:dd/MM/yyyy}\nHoy: {hoy:dd/MM/yyyy}", "Fecha incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // 4. Cambiar el estado a "CheckIn"
                string queryActualizar = $"UPDATE reservations SET status = 'CheckIn' WHERE id = {idReserva};";
                _session.Execute(queryActualizar);

                MessageBox.Show("Check-In realizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar el check-in: " + ex.Message);
                return false;
            }
            finally
            {
                desconectar();
            }
        }


        public List<Habitaciones> GetAllHabtiationOfType(int hotelId, string typeSelected)
        {
            if (conectar())
            {
                string queryHabitaciones = $"SELECT * FROM habitaciones WHERE hotel = {hotelId} AND tipo = '{typeSelected}' ALLOW FILTERING;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<Habitaciones> habitaciones = mapper.Fetch<Habitaciones>(queryHabitaciones);
                desconectar();
                return habitaciones.ToList();
            }
            return null;
        }

        // En tu clase EnlaceCassandra.cs
        // Método corregido con los nombres de columna correctos

        //public List<Reservation> GetAllReservationsBetweenDaysInHotel(int idHotel, LocalDate startDate, LocalDate endDate)
        //{
        //    if (conectar())
        //    {
        //        try
        //        {
        //            // CORRECCIÓN: Usar "entryDate" y no "enddate" (con minúsculas)
        //            // También asegurarse de usar el formato correcto de LocalDate
        //            string query = $"SELECT * FROM reservations WHERE idHotel = {idHotel} " +
        //                          $"AND entryDate <= '{endDate}' " +
        //                          $"AND entryDate >= '{startDate}' " +
        //                          $"ALLOW FILTERING;";

        //            IMapper mapper = new Mapper(_session);
        //            IEnumerable<Reservation> allReservations = mapper.Fetch<Reservation>(query);
        //            desconectar();

        //            return allReservations.ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            desconectar();
        //            throw new Exception($"Error al obtener reservaciones: {ex.Message}", ex);
        //        }
        //    }
        //    return null;
        //}

        // ALTERNATIVA: Si necesitas filtrar por rango de fechas completo
        // (reservaciones que se solapan con el período buscado)
        public List<Reservation> GetAllReservationsBetweenDaysInHotelAlternative(int idHotel, LocalDate startDate, LocalDate endDate)
        {
            if (conectar())
            {
                try
                {
                    // Esta query encuentra reservaciones que se solapan con el período dado
                    // Una reservación se solapa si:
                    // - Empieza antes o durante el período Y termina durante o después del inicio
                    string query = $"SELECT * FROM reservations WHERE idHotel = {idHotel} ALLOW FILTERING;";

                    IMapper mapper = new Mapper(_session);
                    IEnumerable<Reservation> allReservations = mapper.Fetch<Reservation>(query);

                    // Filtrar en memoria las reservaciones que se solapan
                    var filteredReservations = allReservations.Where(r =>
                        r.EntryDate <= endDate && r.EndDate >= startDate
                    ).ToList();

                    desconectar();
                    return filteredReservations;
                }
                catch (Exception ex)
                {
                    desconectar();
                    throw new Exception($"Error al obtener reservaciones: {ex.Message}", ex);
                }
            }
            return null;
        }
        //public List<Reservation> GetAllReservationsBetweenDaysInHotel(int idHotel, LocalDate startDate, LocalDate endDate)
        //{
        //    if (conectar())
        //    {
        //        string query = $"SELECT * FROM  reservations WHERE idHotel = {idHotel} AND entryDate <= '{startDate}' AND endDate >= '{endDate}' ALLOW FILTERING;";
        //        IMapper mapper = new Mapper(_session);
        //        IEnumerable<Reservation> AllReservations = mapper.Fetch<Reservation>(query);
        //        desconectar();
        //        return AllReservations.ToList();
        //    }
        //    return null;
        //}

        // En tu clase EnlaceCassandra.cs

        public bool GuardarReservacion(Reservation reserva)
        {
            if (conectar())
            {
                try
                {
                    IMapper mapper = new Mapper(_session);

                    // Convertir RoomType a formato CQL
                    string roomTypes = reserva.RoomType != null && reserva.RoomType.Count > 0
                        ? "['" + string.Join("', '", reserva.RoomType) + "']"
                        : "[]";

                    // 1. Insertar en la tabla principal
                    string insertMain = $@"INSERT INTO reservations 
                (id, rfcClient, name, emailUser, dateReserved, idHotel, nights, 
                 entryDate, endDate, numRooms, roomType, numGuests, checkIn, checkOut, 
                 payForm, subTotal, total, anticipo, status) 
                VALUES (
    {reserva.Id},
    '{reserva.RfcClient}',
    '{reserva.name.Replace("'", "''")}',
    '{reserva.EmailUser}',
    '{reserva.DateReserved:yyyy-MM-dd HH:mm:ss}',
    {reserva.IdHotel},
    {reserva.Nights},
    '{reserva.EntryDate:yyyy-MM-dd}',
    '{reserva.EndDate:yyyy-MM-dd}',
    {reserva.NumRooms},
    {roomTypes},
    {reserva.NumGuests},
    '{reserva.CheckIn:yyyy-MM-dd HH:mm:ss}',
    '{reserva.CheckOut:yyyy-MM-dd HH:mm:ss}',
    '{reserva.PayForm}',
    {reserva.SubTotal},
    {reserva.total.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    {reserva.anticipo.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    '{reserva.Status}'
);";
                    _session.Execute(insertMain);

                    // 2. Insertar en reservationsByRFC
                    string insertByRFC = $@"
INSERT INTO reservationsByRFC (rfcClient, id, name) 
VALUES ('{reserva.RfcClient}', {reserva.Id}, '{reserva.name.Replace("'", "''")}');";

                    _session.Execute(insertByRFC);

                    // 3. Insertar en reservationsByName
                    string insertByName = $@"
INSERT INTO reservationsByName (name, id, rfcClient) 
VALUES ('{reserva.name.Replace("'", "''")}', {reserva.Id}, '{reserva.RfcClient}');";

                    _session.Execute(insertByName);

                    // 4. Insertar en reservationsByHotelAndDate
                    string insertByHotel = $@"
INSERT INTO reservationsByHotelAndDate 
(idHotel, entryDate, endDate, id, rfcClient, roomType, status, name) 
VALUES (
    {reserva.IdHotel},
    '{reserva.EntryDate:yyyy-MM-dd}',
    '{reserva.EndDate:yyyy-MM-dd}',
    {reserva.Id},
    '{reserva.RfcClient}',
    {roomTypes},
    '{reserva.Status}',
    '{reserva.name.Replace("'", "''")}'
);";

                    _session.Execute(insertByHotel);

                    // 5. Insertar en reservationsByClient
                    string insertByClient = $@"
INSERT INTO reservationsByClient 
(rfcClient, id, idHotel, entryDate, endDate, status, total, name) 
VALUES (
    '{reserva.RfcClient}',
    {reserva.Id},
    {reserva.IdHotel},
    '{reserva.EntryDate:yyyy-MM-dd}',
    '{reserva.EndDate:yyyy-MM-dd}',
    '{reserva.Status}',
    {reserva.total.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    '{reserva.name.Replace("'", "''")}'
);";
                    _session.Execute(insertByClient);

                //    var preparedMain = _session.Prepare(insertMain);
                //    var boundMain = preparedMain.Bind(
                //        reserva.Id,
                //        reserva.RfcClient,
                //        reserva.name,
                //        reserva.EmailUser,
                //        reserva.DateReserved,
                //        reserva.IdHotel,
                //        reserva.Nights,
                //        reserva.EntryDate,
                //        reserva.EndDate,  // Importante: incluir EndDate
                //        reserva.NumRooms,
                //        reserva.RoomType,
                //        reserva.NumGuests,
                //        reserva.CheckIn,
                //        reserva.CheckOut,
                //        reserva.PayForm,
                //        reserva.SubTotal,
                //        reserva.total,
                //        reserva.anticipo,
                //        reserva.Status
                //    );

                //    _session.Execute(boundMain);

                //    // 2. Insertar en tabla por hotel y fecha (para consultas optimizadas)
                //    string insertByHotel = $@"INSERT INTO reservationsByHotelAndDate 
                //(idHotel, entryDate, id, endDate, rfcClient, roomType, status, nights) 
                //VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

                //    var preparedHotel = _session.Prepare(insertByHotel);
                //    var boundHotel = preparedHotel.Bind(
                //        reserva.IdHotel,
                //        reserva.EntryDate,
                //        reserva.Id,
                //        reserva.EndDate,
                //        reserva.RfcClient,
                //        reserva.RoomType,
                //        reserva.Status,
                //        reserva.Nights
                //    );

                //    _session.Execute(boundHotel);

                //    // 3. Insertar en tabla por cliente (para historial)
                //    string insertByClient = $@"INSERT INTO reservationsByClient 
                //(rfcClient, id, idHotel, entryDate, endDate, status, total) 
                //VALUES (?, ?, ?, ?, ?, ?, ?)";

                //    var preparedClient = _session.Prepare(insertByClient);
                //    var boundClient = preparedClient.Bind(
                //        reserva.RfcClient,
                //        reserva.Id,
                //        reserva.IdHotel,
                //        reserva.EntryDate,
                //        reserva.EndDate,
                //        reserva.Status,
                //        reserva.total
                //    );

                //    _session.Execute(boundClient);

                    desconectar();
                    return true;
                }
                catch (Exception ex)
                {
                    desconectar();
                    System.Windows.Forms.MessageBox.Show(
                        $"Error al guardar reservación: {ex.Message}\n\nStackTrace: {ex.StackTrace}",
                        "Error de Base de Datos",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error
                    );
                    return false;
                }
            }
            return false;
        }

        // Método auxiliar para obtener reservaciones por hotel (optimizado)
        public List<Reservation> GetReservationsByHotelOptimized(int idHotel, LocalDate startDate, LocalDate endDate)
        {
            if (conectar())
            {
                try
                {
                    // Usar la tabla optimizada para consultas por hotel
                    string query = $@"SELECT * FROM reservationsByHotelAndDate 
                            WHERE idHotel = {idHotel} 
                            AND entryDate >= '{startDate}' 
                            AND entryDate <= '{endDate}'";

                    IMapper mapper = new Mapper(_session);

                    // Obtener IDs de las reservaciones
                    var reservationsIds = mapper.Fetch<dynamic>(query).ToList();

                    // Luego obtener los detalles completos de cada reservación
                    List<Reservation> fullReservations = new List<Reservation>();

                    foreach (var res in reservationsIds)
                    {
                        string detailQuery = $"SELECT * FROM reservations WHERE id = {res.id}";
                        var fullRes = mapper.First<Reservation>(detailQuery);
                        fullReservations.Add(fullRes);
                    }

                    desconectar();
                    return fullReservations;
                }
                catch (Exception ex)
                {
                    desconectar();
                    throw new Exception($"Error al obtener reservaciones: {ex.Message}", ex);
                }
            }
            return null;
        }



        //string query = "UPDATE users SET name = {UserName}, phone = ?, cellPhone = ?, admin = ?, address = ?, birthdate = ?, state = ? WHERE email = ? and password = ?";


        /*  ------------------  DATABASE EXTRACTION  ------------------  */


        public List<additionalService> GetAllAditionalService()
        {
            if (conectar())
            {
                string query = "SELECT id, name, price FROM additionalservice;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<additionalService> services = mapper.Fetch<additionalService>(query);
                desconectar();
                return services.ToList();
            }
            return null;
        }

        internal List<amenities> GetAllAmenities()
        {
            if (conectar())
            {
                string query = "SELECT id, name, price FROM Amenities;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<amenities> AllAmenites = mapper.Fetch<amenities>(query);
                desconectar();
                return AllAmenites.ToList();
            }
            return null;
        }
        public List<EmpleadosCassandra> GetAllUsers()
        {
            if (conectar())
            {
                string query = "SELECT * FROM users;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<EmpleadosCassandra> users1 = mapper.Fetch<EmpleadosCassandra>(query);
                desconectar();
                return users1.ToList();
            }
            return null;
        }

        public List<Habitaciones> GetAllRoomsByHotelId(int idHotel)
        {
            if (conectar())
            {
                string query = $"SELECT * FROM habitaciones WHERE hotel = {idHotel};";
                IMapper mapper = new Mapper(_session);
                IEnumerable<Habitaciones> rooms = mapper.Fetch<Habitaciones>(query);
                desconectar();
                return rooms.ToList();
            }
            return null;
        }

        public List<Client> GetAllClients()
        {
            if (conectar())
            {
                string query = "SELECT * FROM clients;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<Client> clients = mapper.Fetch<Client>(query);
                desconectar();

                //Filtro en memoria por status == true
                return clients.Where(c => c.status == true).ToList();
            }
            return null;
        }


        public List<Hotel> GetAllHotels()
        {
            if (conectar())
            {
                string query = "SELECT * FROM hoteles;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<Hotel> hotels = mapper.Fetch<Hotel>(query);
                desconectar();
                return hotels.ToList();
            }
            return null;
        }

        public Hotel GetHotel(string country, string city, string state, string name)
        {
            if (conectar())
            {
                string query = $"SELECT * FROM hoteles WHERE country = \'{country}\' and city = \'{city}\' and state = \'{state}\' and name = \'{name}\';";
                IMapper mapper = new Mapper(_session);
                Hotel hotelData = mapper.SingleOrDefault<Hotel>(query);
                desconectar();
                return hotelData;
            }
            return null;
        }

        //public Hotel GetHotelById(int id)
        //{
        //    if (conectar())
        //    {
        //        string query = $"SELECT * FROM hotelesbyid WHERE id = {id}";
        //        IMapper mapper = new Mapper(_session);
        //        Hotel hotelData = mapper.SingleOrDefault<Hotel>(query);
        //        desconectar();
        //        hotelData = GetHotel(hotelData.country, hotelData.city, hotelData.state, hotelData.name);
        //        return hotelData;
        //    }
        //    return null;
        //}
        public Hotel GetHotelById(int id)
        {
            if (conectar())
            {
                try
                {
                    // Primero obtener la información básica de hotelesbyid
                    string query = $"SELECT country, city, state, name FROM hotelesbyid WHERE id = {id};";
                    IMapper mapper = new Mapper(_session);

                    // Usar hotelsbyid en lugar de Hotel para el mapeo inicial
                    var hotelInfo = mapper.SingleOrDefault<hotelsbyid>(query);

                    if (hotelInfo == null)
                    {
                        desconectar();
                        return null;
                    }

                    desconectar();

                    // Ahora obtener los datos completos del hotel
                    Hotel hotelCompleto = GetHotel(hotelInfo.country, hotelInfo.city, hotelInfo.state, hotelInfo.name);

                    if (hotelCompleto != null)
                    {
                        hotelCompleto.id = id; // Asegurarse de que tenga el ID correcto
                    }

                    return hotelCompleto;
                }
                catch (Exception ex)
                {
                    desconectar();
                    MessageBox.Show($"Error al obtener hotel por ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            return null;
        }

        public Client GetClientByRFC(string rfc)
        {
            if (conectar())
            {
                string query = $"SELECT * FROM clients WHERE rfc = ?;";
                IMapper mapper = new Mapper(_session);
                Client clientData = mapper.SingleOrDefault<Client>(query, rfc);
                desconectar();
                return clientData;
            }
            return null;
        }

        public EmpleadosCassandra GetUserById(int id)
        {
            if (conectar())
            {
                string query = "SELECT * FROM users WHERE id = ? ALLOW FILTERING;";
                IMapper mapper = new Mapper(_session);
                EmpleadosCassandra userData = mapper.SingleOrDefault<EmpleadosCassandra>(query, id);
                desconectar();
                return userData;
            }
            return null;
        }

        internal List<roomTypes> GetAllRoomTypes()
        {
            if (conectar())
            {
                string query = "SELECT * FROM roomtypes;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<roomTypes> AllRoomTypes = mapper.Fetch<roomTypes>(query);
                desconectar();
                return AllRoomTypes.ToList();
            }
            return null;
        }

        public EmpleadosCassandra UserLogIn(string email, string passwd, int id = -1)
        {
            if (string.IsNullOrEmpty(email) && id < 0)
            {
                MessageBox.Show("Por favor, ingrese su usuario y contraseña", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (conectar())
            {
                string query;
                IMapper mapper = new Mapper(_session);
                EmpleadosCassandra userData = null;
                if (id < 0)
                {
                    query = "SELECT email, password, admin FROM users WHERE email = ? AND password = ?;";
                    userData = mapper.SingleOrDefault<EmpleadosCassandra>(query, email, passwd);
                }
                else
                {
                    query = "SELECT email FROM usersById WHERE id = ?;";
                    userData = mapper.SingleOrDefault<EmpleadosCassandra>(query, id);

                    if (userData != null)
                    {
                        //MessageBox.Show("Email encontrado: " + userData.email, "Yay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        email = userData.email;
                        query = "SELECT email, password, admin FROM users WHERE email = ? AND password = ?;";

                        userData = mapper.SingleOrDefault<EmpleadosCassandra>(query, email, passwd);
                    }
                }

                if (userData != null) {
                    //MessageBox.Show("Usuario encontrado", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    MessageBox.Show("Usuario ó contraseña incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                desconectar();
                return userData;
            }
            return null;
        }

        public additionalService GetAdditionalServiceByName(string name)
        {
            if (conectar())
            {
                string query = $"SELECT id, name, price FROM additionalService WHERE name = '{name}'";
                IMapper mapper = new Mapper(_session);
                additionalService service = mapper.SingleOrDefault<additionalService>(query);
                desconectar();
                return service;
            }
            return null;
        }

        public List<roomTypes> GetRoomTypesByHotelId(int hotelId)
        {
            try
            {
                conectar();
                var query = $"SELECT * FROM roomTypes WHERE idHotel = {hotelId}";
                var rows = _session.Execute(query);

                List<roomTypes> resultados = new List<roomTypes>();
                foreach (var row in rows)
                {
                    resultados.Add(new roomTypes
                    {
                        id = row.GetValue<int>("id"),
                        name = row.GetValue<string>("name"),
                        idHotel = row.GetValue<int>("idHotel"),
                        // ... mapear todas las propiedades ...
                    });
                }
                return resultados;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener tipos de habitación: " + ex.Message);
                return new List<roomTypes>();
            }
            finally
            {
                desconectar();
            }
        }
        public int testConn()
        {
            _dbServer = "localhost";
            _dbKeySpace = "GrupoHotelero";

            // así SI:
            //_dbServer = ConfigurationManager.AppSettings["node"].ToString();
            //_dbKeySpace = ConfigurationManager.AppSettings["database"].ToString();


            _cluster = Cluster.Builder().AddContactPoint(_dbServer).Build();
            try
            {
                _cluster.Connect(_dbKeySpace);
                _session = _cluster.Connect(_dbKeySpace);
                return 0;
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error de conexión a la base de datos: \n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (MessageBox.Show("Error de conexión a la base de datos \n¿Desea salir de la aplicación?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    return 2;
                }
                return 1;
            }
            finally
            {
                desconectar();
            }

        }

        //public IEnumerable<HotelesXCiudad> Get_One(int dato)
        //{
        //    string query = "SELECT campo1, campo2 FROM ejemplo WHERE campo1 = ?;";
        //    conectar();
        //    IMapper mapper = new Mapper(_session);
        //    IEnumerable<HotelesXCiudad> users = mapper.Fetch<HotelesXCiudad>(query, dato);

        //    desconectar();
        //    return users.ToList();
        //}

        //public List<HotelesXCiudad> Get_All()
        //{
        //    string query = "SELECT campo1, campo2 FROM ejemplo;";
        //    conectar();

        //    IMapper mapper = new Mapper(_session);
        //    IEnumerable<HotelesXCiudad> users = mapper.Fetch<HotelesXCiudad>(query);

        //    desconectar();
        //    return users.ToList();

        //}

        internal List<string> GetAllAditionalServiceByHotel(int idHotelSeleccionado)
        {
            if (conectar())
            {
                string query = $"SELECT country, city, state, name FROM hotelesbyid WHERE id = {idHotelSeleccionado};";
                IMapper mapper = new Mapper(_session);
                IEnumerable<hotelsbyid> hotels = mapper.Fetch<hotelsbyid>(query);

                if (hotels == null)
                {
                    MessageBox.Show("No se encontraron hoteles con el ID especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                hotelsbyid hotelInfo = hotels.FirstOrDefault();

                if (hotelInfo == null)
                {
                    MessageBox.Show("No se encontró información del hotel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                string query2 = $"SELECT aditionalService FROM hoteles WHERE country='{hotelInfo.country}' AND city='{hotelInfo.city}' AND state='{hotelInfo.state}' AND name='{hotelInfo.name}';";
                IMapper mapper2 = new Mapper(_session);
                IEnumerable<Hotel> hotels2 = mapper2.Fetch<Hotel>(query2);

                desconectar();
                if (hotels2 != null)
                {
                    Hotel hotel = hotels2.FirstOrDefault();
                    return hotel != null ? hotel.aditionalService : null;
                }
            }
            //string updateQuery = $"UPDATE users SET password = '{usuario.password}', name = '{usuario.name}', phone = '{usuario.phone}', cellPhone = '{usuario.cellphone}', admin = {usuario.admin}, address = '{usuario.address}', birthdate = '{usuario.birthdate:yyyy-MM-dd}', entryDate = '{usuario.entrydate:yyyy-MM-dd}', status = {usuario.status} WHERE email = '{emailActual}' AND password = '{passwordActual}';";
            return null;
        }

        internal List<string> GetAllAmenitiesByRoomType(int idTipo)
        {
            if (conectar())
            {
                string query = $"SELECT amenites FROM roomtypes WHERE id = {idTipo};";
                IMapper mapper = new Mapper(_session);
                IEnumerable<roomTypes> rooms = mapper.Fetch<roomTypes>(query);

                desconectar();
                if (rooms != null)
                {
                    roomTypes room = rooms.FirstOrDefault();
                    return room.amenites;
                }
            }
            return null;
        }

        public List<Hotel> ObtenerHotelesPorCiudad(string ciudad)
        {
            List<Hotel> hoteles = new List<Hotel>();

            if (!conectar())
                return hoteles;

            try
            {
                // Filtrar por ciudad y estado activo (status = true)
                string query = $"SELECT * FROM hoteles WHERE city = '{ciudad}' AND status = true ALLOW FILTERING;";
                var filas = _session.Execute(query);

                foreach (var row in filas)
                {
                    hoteles.Add(new Hotel
                    {
                        id = row.GetValue<int>("id"),
                        name = row.GetValue<string>("name"),
                        country = row.GetValue<string>("country"),
                        city = row.GetValue<string>("city"),
                        state = row.GetValue<string>("state"),
                        address = row.GetValue<string>("address"),
                        phone = row.GetValue<string>("phone"),
                        views = row.GetValue<string>("views"),
                        turisticZone = row.GetValue<bool>("turisticzone"),
                        eventsSalon = row.GetValue<bool>("eventssalon"),
                        numPools = row.GetValue<int>("numpools"),
                        aditionalService = row.GetValue<List<string>>("aditionalservice"),
                        roomTypes = row.GetValue<List<string>>("roomtypes"),
                        userId = row.GetValue<int>("userid"),
                        dateRegistered = row.GetValue<LocalDate>("dateregistered"),
                        dateOperation = row.GetValue<LocalDate>("dateoperation"),
                        status = row.GetValue<bool>("status")
                    });
                }

                return hoteles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar hoteles: " + ex.Message);
                return hoteles;
            }
            finally
            {
                desconectar();
            }
        }

        public List<string> ObtenerServiciosAdicionalesPorHotel(int hotelId)
        {
            List<string> servicios = new List<string>();

            if (!conectar())
                return servicios;

            try
            {
                string query = $"SELECT aditionalservice FROM hoteles WHERE id = {hotelId} ALLOW FILTERING;";
                var row = _session.Execute(query).FirstOrDefault();

                if (row != null)
                {
                    servicios = row.GetValue<List<string>>("aditionalservice");
                }

                return servicios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener servicios adicionales: " + ex.Message);
                return servicios;
            }
            finally
            {
                desconectar();
            }
        }

        public bool DeleteAllRoomByTypeFromHotel(roomTypes room)
        {
            conectar();
            string query = $"DELETE FROM habitaciones WHERE hotel = {room.idHotel} AND tipo = '{room.name}';";
            IMapper mapper = new Mapper(_session);
            _session.Execute(query);
            desconectar();
            return true;
        }

        public List<Habitaciones> GetAllHabitaciones()
        {
            if (conectar())
            {
                string query = "SELECT * FROM habitaciones;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<Habitaciones> hotels = mapper.Fetch<Habitaciones>(query);
                desconectar();
                return hotels.ToList();
            }
            return null;
        }

        public bool UpdateRoomsByType(Habitaciones habitacion)
        {
            conectar();
            string queryId = "SELECT MAX(id) FROM habitaciones;"; // Get the maximum ID from the hoteles table
            IMapper mapper = new Mapper(_session);
            Cassandra.RowSet result = _session.Execute(queryId);
            int id = 0;

            desconectar();

            List<Habitaciones> allHotels = GetAllHabitaciones();
            //newHotel.id = 0; // Reset the ID to 0 before checking for existing IDs
            foreach (Habitaciones hotel in allHotels)
            {
                if (hotel.id > 0 && hotel.id > habitacion.id)
                {
                    habitacion.id = hotel.id;
                }
            }
            habitacion.id++;
         
            conectar();
            string queryHabt = $"INSERT INTO habitaciones (id, num, tipo, status, hotel) VALUES ({habitacion.id}, {habitacion.num}, '{habitacion.tipo}', '{habitacion.status}', {habitacion.hotel});";
            //IMapper mapper = new Mapper(_session);
            _session.Execute(queryHabt);
            desconectar();
            return true;
        }

       

        public List<roomTypes> ObtenerTiposHabitacionPorHotel(int idHotel)
        {
            List<roomTypes> habitaciones = new List<roomTypes>();

            if (!conectar())
                return habitaciones;

            try
            {
                string query = $"SELECT * FROM roomTypes WHERE idHotel = {idHotel} ALLOW FILTERING;";
                var rows = _session.Execute(query);

                foreach (var row in rows)
                {
                    habitaciones.Add(new roomTypes
                    {
                        id = row.GetValue<int>("id"),
                        idHotel = row.GetValue<int>("idhotel"),
                        name = row.GetValue<string>("name"),
                        bedTypes = row.GetValue<string>("bedtypes"),
                        bedNums = row.GetValue<int>("bednums"),
                        priceNight = row.GetValue<decimal>("pricenight"),
                        maxGuests = row.GetValue<int>("maxguests"),
                        maxRooms = row.GetValue<int>("maxrooms"),
                        description = row.GetValue<string>("description"),
                        viewType = row.GetValue<string>("viewtype"),
                        dimensions = row.GetValue<string>("dimensions"),
                        level = row.GetValue<string>("level"),
                        amenites = row.GetValue<List<string>>("amenites"),
                        habInitNum = row.GetValue<int>("habinitnum"),
                        habEndNum = row.GetValue<int>("habendnum"),
                        emailUser = row.GetValue<string>("emailuser"),
                        dateRegistered = row.GetValue<LocalDate>("dateregistered")
                    });
                }

                return habitaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener habitaciones: " + ex.Message);
                return habitaciones;
            }
            finally
            {
                desconectar();
            }
        }

        internal roomTypes GetRoomTypeById(int id)
        {
            if (conectar())
            {
                string query = $"SELECT * FROM roomtypes WHERE id = {id};";
                IMapper mapper = new Mapper(_session);
                roomTypes roomTypeData = mapper.SingleOrDefault<roomTypes>(query); // Aquí deberías pasar el ID correcto
                desconectar();
                return roomTypeData;
            }
            return null;
        }

        internal int GetLastHabitacionId()
        {
            if (conectar())
            {
                string query = $"SELECT MAX(id) FROM habitaciones;";
                IMapper mapper = new Mapper(_session);
                Cassandra.RowSet result = _session.Execute(query);
                desconectar();
                if (result != null)
                {
                    var row = result.First();
                    if (!row.IsNull(0))
                        return row.GetValue<int>(0);
                    else
                        return 0; // No hay tipos de habitación registrados para este hotel
                }
                return 0; // No hay tipos de habitación registrados para este hotel
            }
            return -1; // Error al conectar a la base de datos
        }

        public List<string> GetAllClientsRFC()
        {
            if (conectar())
            {
                string query = "SELECT rfc, status FROM clients;";
                IMapper mapper = new Mapper(_session);
                IEnumerable<Client> clients = mapper.Fetch<Client>(query);
                desconectar();

                List<string> rfcs = new List<string>();
                foreach (var client in clients)
                {
                    if (client.status == true) // Solo agregar RFCs de clientes activos
                    {
                        rfcs.Add(client.rfc);
                    }
                }

                //Filtro en memoria por status == true
                return rfcs;
            }
            return null;
        }

        public void InsertHabitaciones(Habitaciones habitacion)
        {
            if (conectar())
            {
                try
                {
                    string query = $"INSERT INTO habitaciones (id, num, tipo, status, hotel) VALUES ({habitacion.id}, {habitacion.num}, '{habitacion.tipo}', '{habitacion.status}', {habitacion.hotel});";
                    IMapper mapper = new Mapper(_session);
                    _session.Execute(query);
                    //MessageBox.Show("Habitación registrada correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al registrar la habitación: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    desconectar();
                }
            }
            else
            {
                MessageBox.Show("Error de conexión a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Habitaciones> ObtenerHabitacionesDisponibles(int hotelId, string tipoHabitacion, LocalDate fechaInicio, LocalDate fechaFin)
        {
            if (!conectar()) return new List<Habitaciones>();

            try
            {
                // 1. Obtener habitaciones de ese hotel y tipo
                string queryHabitaciones = $"SELECT * FROM habitaciones WHERE hotel = {hotelId} ALLOW FILTERING;";
                var todas = _session.Execute(queryHabitaciones)
                                    .Where(h => h.GetValue<string>("tipo") == tipoHabitacion)
                                    .ToList();

                List<Habitaciones> disponibles = new List<Habitaciones>();

                foreach (var fila in todas)
                {
                    int num = fila.GetValue<int>("num");

                    // 2. Comprobar si hay reserva para esa habitación en ese rango
                    string queryReserva = $@"
                SELECT * FROM  reservations
                WHERE idHotel = {hotelId} AND numRooms = {num} 
                AND entryDate <= '{fechaFin:yyyy-MM-dd}' 
                AND endDate >= '{fechaInicio:yyyy-MM-dd}'
                ALLOW FILTERING;";

                    var reservas = _session.Execute(queryReserva);

                    // Si no hay reservas que choquen, se considera disponible
                    if (!reservas.Any())
                    {
                        disponibles.Add(new Habitaciones
                        {
                            id = fila.GetValue<int>("id"),
                            num = num,
                            tipo = tipoHabitacion,
                            status = fila.GetValue<string>("status"),
                            hotel = hotelId
                        });
                    }
                }

                return disponibles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar habitaciones disponibles: " + ex.Message);
                return new List<Habitaciones>();
            }
            finally
            {
                desconectar();
            }
        }


        public Reservation ObtenerReservacionPorCodigo(Guid codigo)
        {
            if (!conectar()) return null;

            try
            {
                string query = $"SELECT * FROM reservations WHERE id = {codigo} LIMIT 1;";
                var row = _session.Execute(query).FirstOrDefault();

                if (row == null)
                    return null;

                return new Reservation
                {
                    Id = row.GetValue<Guid>("id"),
                    RfcClient = row.GetValue<string>("rfcclient"),
                    anticipo = row.GetValue<decimal>("anticipo"),
                    total = row.GetValue<decimal>("total"),
                    EntryDate = row.GetValue<LocalDate>("entrydate"),
                    EndDate = row.GetValue<LocalDate>("enddate")
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar reservación: " + ex.Message);
                return null;
            }
            finally
            {
                desconectar();
            }
        }

        public Reservation ObtenerReservacionPorCodigo2(Guid id)
        {
            if (!conectar())
                return null;

            try
            {
                string query = $"SELECT * FROM reservations WHERE id = {id};";
                var row = _session.Execute(query).FirstOrDefault();

                if (row == null)
                    return null;

                return new Reservation
                {
                    Id = row.GetValue<Guid>("id"),
                    RfcClient = row.GetValue<string>("rfcclient"),
                    name = row.GetValue<string>("name"),
                    EmailUser = row.GetValue<string>("emailuser"),
                    DateReserved = row.GetValue<DateTime>("datereserved"),
                    IdHotel = row.GetValue<int>("idhotel"),
                    Nights = row.GetValue<short>("nights"),
                    EntryDate = row.GetValue<LocalDate>("entrydate"),
                    EndDate = row.GetValue<LocalDate>("enddate"),
                    NumRooms = row.GetValue<short>("numrooms"),
                    RoomType = row.GetValue<List<string>>("roomtype"),
                    NumGuests = row.GetValue<short>("numguests"),
                    CheckIn = row.GetValue<DateTime>("checkin"),
                    CheckOut = row.GetValue<DateTime>("checkout"),
                    PayForm = row.GetValue<string>("payform"),
                    SubTotal = row.GetValue<int>("subtotal"),
                    anticipo = row.GetValue<decimal>("anticipo"),
                    total = row.GetValue<decimal>("total"),
                    Status = row.GetValue<string>("status")
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener reservación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }

        public Reservation ObtenerReservacionPorNombre(string name)
        {
            if (!conectar())
                return null;

            try
            {
                // Buscar en la tabla auxiliar por nombre
                string query = $"SELECT id FROM reservationsByName WHERE name = '{name.Replace("'", "''")}' LIMIT 1;";
                var row = _session.Execute(query).FirstOrDefault();

                if (row == null)
                {
                    MessageBox.Show("No se encontró reservación con ese nombre.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }

                Guid id = row.GetValue<Guid>("id");
                desconectar();

                // Obtener los detalles completos
                return ObtenerReservacionPorCodigo2(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar reservación por nombre: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }

        public Reservation ObtenerReservacionPorRFC(string rfc)
        {
            if (!conectar())
                return null;

            try
            {
                // Buscar en la tabla auxiliar por RFC
                string query = $"SELECT id FROM reservationsByRFC WHERE rfcClient = '{rfc}' LIMIT 1;";
                var row = _session.Execute(query).FirstOrDefault();

                if (row == null)
                {
                    MessageBox.Show("No se encontró reservación con ese RFC.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }

                Guid id = row.GetValue<Guid>("id");
                desconectar();

                // Obtener los detalles completos
                return ObtenerReservacionPorCodigo2(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar reservación por RFC: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public bool GuardarCheckOut(CHECKOUT2 checkout)
        {
            try
            {
                conectar();

                // Convertir listas a formato CQL
                //string habitacionesCql = checkout.Habitaciones != null
                //    ? "[" + string.Join(", ", checkout.Habitaciones.Select(h => $"'{h}'")) + "]"
                //    : "[]";

                //string serviciosCql = checkout.Servicios != null
                //    ? "[" + string.Join(", ", checkout.Servicios.Select(s => $"'{s}'")) + "]"
                //    : "[]";
                string habitacionesCql = checkout.Habitaciones != null && checkout.Habitaciones.Count > 0
          ? "['" + string.Join("', '", checkout.Habitaciones) + "']"
          : "[]";

                string serviciosCql = checkout.Servicios != null && checkout.Servicios.Count > 0
                    ? "['" + string.Join("', '", checkout.Servicios) + "']"
                    : "[]";

                string cql = $@"
INSERT INTO checkout (
    ID_factura, ID_Reserva, ID_Hotel, Fecha_Check_Out, RFCliente,
    Habitaciones, Total, Anticipo, Formadepago, Servicios,
    TotalServicios, FormapagoServicios, TotalCobrado, RUTAPDF
)
VALUES (
    {checkout.ID_factura},
    {checkout.ID_Reserva},
    {checkout.ID_Hotel},
    '{checkout.Fecha_Check_Out:yyyy-MM-dd}',
    '{checkout.RFCliente}',
    {habitacionesCql},
    {checkout.Total.ToString(CultureInfo.InvariantCulture)},
    {checkout.Anticipo.ToString(CultureInfo.InvariantCulture)},
    '{checkout.Formadepago}',
    {serviciosCql},
    {checkout.TotalServicios.ToString(CultureInfo.InvariantCulture)},
    '{checkout.FormapagoServicios}',
    {checkout.TotalCobrado.ToString(CultureInfo.InvariantCulture)},
    '{checkout.RUTAPDF}'
);";

                _session.Execute(cql);

                MessageBox.Show("Se guardó el Check-Out correctamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el Check-Out: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

        //public bool GuardarCheckOut(CHECKOUT2 checkout)
        //{
        //    try
        //    {
        //        //string roomTypes = reserva.RoomType != null ? "[" + string.Join(", ", reserva.RoomType.Select(s => $"'{s}'")) + "]" : "[]";
        //        conectar();


        //        // string habitacionesCql = checkout.Habitaciones != null ? "[" + string.Join(", ", checkout.Habitaciones.Select(s => $"'{s}'")) + "]" : "[]";
        //        string serviciosCql = checkout.Servicios != null ? "[" + string.Join(", ", checkout.Servicios.Select(s => $"'{s}'")) + "]" : "[]";

        //        string cql = $@"INSERT INTO checkout (ID_factura, ID_Reserva, ID_Hotel, Fecha_Check_Out, RFCliente, Habitaciones, Total, Anticipo, Servicios, TotalServicios, FormapagoServicios, TotalCobrado, RUTAPDF) 
        //    VALUES ({checkout.ID_factura}, {checkout.ID_Reserva}, {checkout.ID_Hotel}, '{checkout.Fecha_Check_Out}', {checkout.Habitaciones}, {checkout.Total}, {checkout.Anticipo}, '{checkout.Formadepago}', {serviciosCql}, {checkout.TotalServicios}, '{checkout.FormapagoServicios}', {checkout.TotalCobrado}, '{checkout.RUTAPDF}');";

        //        //'{empleado.entrydate:yyyy-MM-ddTHH:mm:ssZ}'

        //        _session.Execute(cql);
        //        MessageBox.Show("Se guardo el check out" , "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de errores, puede ser log o simplemente mostrar el error
        //        Console.WriteLine("Error al guardar el checkOut: " + ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        desconectar();
        //    }
        //}


        // Ejemplo de leer row x row
        //internal Reservation ObtenerReservacionPorNombre(string name)
        //{
        //    if (!conectar())
        //        return null;

        //    try
        //    {
        //        string query = $"SELECT * FROM reservationsByName WHERE name = '{name}';";
        //        var row = _session.Execute(query).FirstOrDefault();

        //        if (row == null)
        //            return null;
        //        Guid id = row.GetValue<Guid>("id");

        //        return ObtenerReservacionPorCodigo2(id);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Error al buscar reservación por nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //    finally
        //    {
        //        desconectar();
        //    }
        //}
        //internal Reservation ObtenerReservacionPorRFC(string rfc)
        //{
        //    if (!conectar())
        //        return null;

        //    try
        //    {
        //        string query = $"SELECT * FROM reservationsByRFC WHERE rfcClient = '{rfc}';";
        //        var row = _session.Execute(query).FirstOrDefault();

        //        if (row == null)
        //            return null;
        //        Guid id = row.GetValue<Guid>("id");

        //        return ObtenerReservacionPorCodigo2(id);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Error al buscar reservación por RFC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //    finally
        //    {
        //        desconectar();
        //    }
        //}

        public List<string>  GetAllCountryFromHotels()
        {
            conectar();
            try
            {
                var paises = new List<string> { "Todos" };
                var result = _session.Execute("SELECT DISTINCT country FROM hoteles;");
                foreach (var row in result)
                {
                    paises.Add(row.GetValue<string>("country"));
                }
                return paises;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string>();
            }
            finally
            {
                desconectar();
            }

        }


        public HashSet<int> GetAllYearsFromHotels()
        {
            conectar();
            try
            {
                var anios = new HashSet<int>(); // Evita duplicados
                var result = _session.Execute("SELECT dateregistered FROM hoteles");
                foreach (var row in result)
                {
                    var fecha = row.GetValue<LocalDate>("dateregistered");
                    anios.Add(fecha.Year);
                }
                return anios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new HashSet<int>();
            }
            finally
            {
                desconectar();
            }

        }

        public HashSet<string> GetAllCitiesFromHotels(string country)
        {
            conectar();
            try
            {
                var ciudades = new HashSet<string> { "Todos" }; // Evita duplicados
                string query = $"SELECT city FROM hoteles WHERE country = '{country}';";
                var result = _session.Execute(query);
                foreach (var row in result)
                {
                    ciudades.Add(row.GetValue<string>("city"));
                }
                return ciudades;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new HashSet<string>();
            }
            finally
            {
                desconectar();
            }
        }

        public List<Hotel> GetAllHotelesFromCity(string country, string city)
        {
            conectar();
            try
            {
                var hoteles = new List<Hotel>();

                var query = $"SELECT id, name FROM hoteles WHERE country = '{country}' AND city = '{city}';";
                var result = _session.Execute(query);

                foreach (var row in result)
                {
                    var hotel = new Hotel
                    {
                        id = row.GetValue<int>("id"),
                        name = row.GetValue<string>("name")
                    };
                    hoteles.Add(hotel);
                }

                return hoteles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener hoteles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Hotel>();
            }
            finally
            {
                desconectar();
            }
        }

        public List<ReporteOcupacion> ObtenerReporteOcupacion(int idHotel, int year)
        {
            conectar();
            try
            {
                // Contar habitaciones totales en ese hotel
                var resultHab = _session.Execute($"SELECT COUNT(*) AS total FROM habitaciones WHERE hotel = {idHotel};");
                int totalHabitaciones = Convert.ToInt32(resultHab.First()["total"]);

                // Contar las habitaciones ocupadas con check-out en ese año/mes
                string consultaOcupadas = $@"
                SELECT COUNT(*) AS ocupadas 
                FROM checkout 
                WHERE id_hotel = {idHotel} 
                    AND fecha_check_out >= '{year}-01-01' 
                    AND fecha_check_out < '{year}-12-30' ALLOW FILTERING;";

                var resultOcupadas = _session.Execute(consultaOcupadas);
                int habitacionesOcupadas = Convert.ToInt32(resultOcupadas.First()["ocupadas"]);

                double porcentaje = totalHabitaciones > 0
                    ? (habitacionesOcupadas * 100.0) / totalHabitaciones
                    : 0;

                return new List<ReporteOcupacion> {
                    new ReporteOcupacion
                    {
                        HotelId = idHotel,
                        NombreHotel = GetHotelById(idHotel).name, // deberías tener una función para esto
                        TotalHabitaciones = totalHabitaciones,
                        HabitacionesOcupadas = habitacionesOcupadas,
                        PorcentajeOcupacion = Math.Round(porcentaje, 2)
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte de ocupación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<ReporteOcupacion>();
            }
            finally
            {
                desconectar();
            }
        }

        public List<Hotel> GetHotelesFiltrados(string pais, string ciudad, string state, string nombre)
        {
            conectar();
            try
            {
                var query = "SELECT id, name, city, country FROM hoteles";
                var where = new List<string>();
                if (!string.IsNullOrEmpty(pais))
                    where.Add($"country = '{pais}'");
                if (!string.IsNullOrEmpty(ciudad))
                    where.Add($"city = '{ciudad}'");
                if (!string.IsNullOrEmpty(nombre))
                    where.Add($"name = '{nombre}'");

                if (where.Count > 0)
                    query += " WHERE " + string.Join(" AND ", where) + " ALLOW FILTERING;";
                //query += " ALLOW FILTERING;";
                var result = _session.Execute(query);
                var hoteles = new List<Hotel>();
                foreach (var row in result)
                {
                    hoteles.Add(new Hotel
                    {
                        id = row.GetValue<int>("id"),
                        name = row.GetValue<string>("name"),
                        city = row.GetValue<string>("city"),
                        country = row.GetValue<string>("country")
                    });
                }
                return hoteles;
            }
            finally { desconectar(); }
        }

        public List<Reservation> GetReservacionesPorHotelYAnio(int hotelId, int? anio)
        {
            conectar();
            try
            {
                string query = $"SELECT * FROM reservations WHERE idhotel = {hotelId} ALLOW FILTERING;";
                var result = _session.Execute(query);
                var lista = new List<Reservation>();
                foreach (var row in result)
                {
                    var fecha = row.GetValue<LocalDate>("entrydate");
                    
                    if (anio == null || fecha.Year == anio)
                    {
                        lista.Add(new Reservation
                        {
                            Id = row.GetValue<Guid>("id"),
                            EntryDate = fecha,
                            total = row.GetValue<decimal>("total"),
                            RfcClient = row.GetValue<string>("rfcclient"),
                            IdHotel = row.GetValue<int>("idhotel"),
                            NumRooms = row.GetValue<short>("numrooms"),
                            RoomType = row.GetValue<List<string>>("roomtype"),
                            NumGuests = row.GetValue<short>("numguests"),
                            CheckIn = row.GetValue<DateTime>("checkin"),
                            CheckOut = row.GetValue<DateTime>("checkout"),
                            PayForm = row.GetValue<string>("payform"),
                            SubTotal = row.GetValue<int>("subtotal"),
                            EndDate = row.GetValue<LocalDate>("enddate"),
                            anticipo = row.GetValue<decimal>("anticipo"),
                            
                            EmailUser = row.GetValue<string>("emailuser"),
                            DateReserved = row.GetValue<DateTime>("datereserved")


                        });
                    }
                }
                return lista;
            }
            finally { desconectar(); }
            //return result != null && result.Any() ? result.First().GetValue<decimal>("totalservicios") : 0;
        }

        public decimal GetTotalServiciosPorReserva(Guid idReserva)
        {
            conectar();
            try
            {
                string query = $"SELECT totalservicios FROM checkout WHERE id_reserva = {idReserva} ALLOW FILTERING";
                var result = _session.Execute(query);
                var row = result.FirstOrDefault();
                return row != null ? row.GetValue<decimal>("totalservicios") : 0;
            }
            finally { desconectar(); }
            
        }

        //public List<HistorialCliente> ObtenerHistorialCliente(string rfc, int anio)
        //{
        //    conectar();
        //    try
        //    {
        //        LocalDate fechaInicio = new LocalDate(anio, 1, 1);
        //        LocalDate fechaFin = new LocalDate(anio + 1, 1, 1);
        //        //DateTime fechaInicio = new DateTime(anio, 1, 1);
        //        //DateTime fechaFin = new DateTime(anio + 1, 1, 1);


        //        string consulta = $@"
        //    SELECT rfcliente, id_hotel, fecha_check_out, totalcobrado 
        //    FROM checkout 
        //    WHERE rfcliente = '{rfc}' 
        //      AND fecha_check_out >= '{fechaInicio:yyyy-MM-dd}' 
        //      AND fecha_check_out < '{fechaFin:yyyy-MM-dd}' 
        //    ALLOW FILTERING;";

        //        var result = _session.Execute(consulta);

        //        List<HistorialCliente> historial = new List<HistorialCliente>();

        //        foreach (var row in result)
        //        {
        //            historial.Add(new HistorialCliente
        //            {
        //                RFC = row["rfcliente"]?.ToString(),
        //                Hotel = GetHotelById(Convert.ToInt32(row["id_hotel"])).name.ToString(),
        //                FechaCheckOut = ((LocalDate)row["fecha_check_out"]).ToDateTimeOffset().Date,
        //                Total = Convert.ToDecimal(row["totalcobrado"])
        //            });
        //        }

        //        return historial;
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al obtener el historial del cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return new List<HistorialCliente>();
        //    }
        //    finally
        //    {
        //        desconectar();
        //    }
        //}

        public List<HistorialCliente> ObtenerHistorialCliente(string rfc, int anio)
        {
            if (!conectar())
                return new List<HistorialCliente>();

            try
            {
                LocalDate fechaInicio = new LocalDate(anio, 1, 1);
                LocalDate fechaFin = new LocalDate(anio + 1, 1, 1);

                // CORRECCIÓN: usar 'rfcliente' en lugar de 'rfclient'
                string consulta = $@"
        SELECT rfcliente, id_hotel, fecha_check_out, totalcobrado 
        FROM checkout 
        WHERE rfcliente = '{rfc}' 
          AND fecha_check_out >= '{fechaInicio:yyyy-MM-dd}' 
          AND fecha_check_out < '{fechaFin:yyyy-MM-dd}' 
        ALLOW FILTERING;";

                var result = _session.Execute(consulta);

                List<HistorialCliente> historial = new List<HistorialCliente>();

                foreach (var row in result)
                {
                    historial.Add(new HistorialCliente
                    {
                        RFC = row.GetValue<string>("rfcliente"), // CORREGIDO
                        Hotel = GetHotelById(row.GetValue<int>("id_hotel"))?.name ?? "Desconocido",
                        FechaCheckOut = row.GetValue<LocalDate>("fecha_check_out").ToDateTimeOffset().Date,
                        Total = row.GetValue<decimal>("totalcobrado")
                    });
                }

                return historial;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el historial del cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<HistorialCliente>();
            }
            finally
            {
                desconectar();
            }
        }
        //public List<HistorialCliente> ObtenerHistorialClienteCompleto(string rfc, int anio)
        //{
        //    if (!conectar())
        //        return new List<HistorialCliente>();

        //    try
        //    {
        //        string consulta = $@"
        //SELECT * FROM checkout 
        //WHERE RFCliente = '{rfc}' 
        //  AND anio = {anio}";

        //        var result = _session.Execute(consulta);
        //        List<HistorialCliente> historial = new List<HistorialCliente>();

        //        foreach (var row in result)
        //        {
        //            historial.Add(new HistorialCliente
        //            {
        //                RFC = row.GetValue<string>("RFCliente"),
        //                NombreCliente = row.GetValue<string>("nombrecliente"),
        //                Ciudad = row.GetValue<string>("ciudad"),
        //                IdHotel = row.GetValue<int>("idhotel"),
        //                NombreHotel = row.GetValue<string>("nombrehotel"),
        //                TipoHabitacion = row.GetValue<string>("tipohabitacion"),
        //                NumeroHabitacion = row.GetValue<string>("numerohabitacion"),
        //                NumPersonasHospedadas = row.GetValue<int>("numpersonashospedadas"),
        //                CodigoReservacion = row.GetValue<Guid>("codigoreservacion"),
        //                FechaReservacion = row.GetValue<DateTime>("fechareservacion"),
        //                FechaCheckIn = row.GetValue<DateTime>("fechacheckin"),
        //                FechaCheckOut = row.GetValue<LocalDate>("fechacheckout").ToDateTimeOffset().Date,
        //                EstatusReservacion = row.GetValue<string>("estatusreservacion"),
        //                Anticipo = row.GetValue<decimal>("anticipo"),
        //                MontoHospedaje = row.GetValue<decimal>("montohospedaje"),
        //                MontoServiciosAdicionales = row.GetValue<decimal>("montoserviciosadicionales"),
        //                TotalFactura = row.GetValue<decimal>("totalfactura")
        //            });
        //        }

        //        return historial;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error al obtener historial del cliente: {ex.Message}",
        //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return new List<HistorialCliente>();
        //    }
        //    finally
        //    {
        //        desconectar();
        //    }
        //}


        public List<HistorialCliente> ObtenerHistorialClienteCompleto(string rfc, int anio)
        {
            if (!conectar())
                return new List<HistorialCliente>();

            try
            {
                string consulta = $@"
SELECT * FROM historialClienteCompleto 
WHERE rfcCliente = '{rfc}' 
  AND anio = {anio};";

                var result = _session.Execute(consulta);
                List<HistorialCliente> historial = new List<HistorialCliente>();

                foreach (var row in result)
                {
                    historial.Add(new HistorialCliente
                    {
                        RFC = row.GetValue<string>("rfccliente"),
                        NombreCliente = row.GetValue<string>("nombrecliente"),
                        Ciudad = row.GetValue<string>("ciudad"),
                        IdHotel = row.GetValue<int>("idhotel"),
                        NombreHotel = row.GetValue<string>("nombrehotel"),
                        TipoHabitacion = row.GetValue<string>("tipohabitacion"),
                        NumeroHabitacion = row.GetValue<string>("numerohabitacion"),
                        NumPersonasHospedadas = row.GetValue<int>("numpersonashospedadas"),
                        CodigoReservacion = row.GetValue<Guid>("codigoreservacion"),
                        FechaReservacion = row.GetValue<DateTime>("fechareservacion"),
                        FechaCheckIn = row.GetValue<DateTime>("fechacheckin"),
                        FechaCheckOut = row.GetValue<LocalDate>("fechacheckout").ToDateTimeOffset().Date,
                        EstatusReservacion = row.GetValue<string>("estatusreservacion"),
                        Anticipo = row.GetValue<decimal>("anticipo"),
                        MontoHospedaje = row.GetValue<decimal>("montohospedaje"),
                        MontoServiciosAdicionales = row.GetValue<decimal>("montoserviciosadicionales"),
                        TotalFactura = row.GetValue<decimal>("totalfactura")
                    });
                }

                return historial;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener historial: {ex.Message}\n\nStackTrace: {ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<HistorialCliente>();
            }
            finally
            {
                desconectar();
            }
        }

        //        public List<HistorialCliente> ObtenerHistorialClienteCompleto(string rfc, int anio)
        //        {
        //            if (!conectar())
        //                return new List<HistorialCliente>();

        //            try
        //            {
        //                // CORRECCIÓN: Usar la partition key correcta (rfcCliente y anio)
        //                string consulta = $@"
        //SELECT * FROM historialClienteCompleto 
        //WHERE rfcCliente = '{rfc}' 
        //  AND anio = {anio};";

        //                var result = _session.Execute(consulta);
        //                List<HistorialCliente> historial = new List<HistorialCliente>();

        //                foreach (var row in result)
        //                {
        //                    historial.Add(new HistorialCliente
        //                    {
        //                        RFC = row.GetValue<string>("rfccliente"),
        //                        NombreCliente = row.GetValue<string>("nombrecliente"),
        //                        Ciudad = row.GetValue<string>("ciudad"),
        //                        IdHotel = row.GetValue<int>("idhotel"),
        //                        NombreHotel = row.GetValue<string>("nombrehotel"),
        //                        TipoHabitacion = row.GetValue<string>("tipohabitacion"),
        //                        NumeroHabitacion = row.GetValue<string>("numerohabitacion"),
        //                        NumPersonasHospedadas = row.GetValue<int>("numpersonashospedadas"),
        //                        CodigoReservacion = row.GetValue<Guid>("codigoreservacion"),
        //                        FechaReservacion = row.GetValue<DateTime>("fechareservacion"),
        //                        FechaCheckIn = row.GetValue<DateTime>("fechacheckin"),
        //                        FechaCheckOut = row.GetValue<LocalDate>("fechacheckout").ToDateTimeOffset().Date,
        //                        EstatusReservacion = row.GetValue<string>("estatusreservacion"),
        //                        Anticipo = row.GetValue<decimal>("anticipo"),
        //                        MontoHospedaje = row.GetValue<decimal>("montohospedaje"),
        //                        MontoServiciosAdicionales = row.GetValue<decimal>("montoserviciosadicionales"),
        //                        TotalFactura = row.GetValue<decimal>("totalfactura")
        //                    });
        //                }

        //                return historial;
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show($"Error al obtener historial del cliente: {ex.Message}\n\nDetalles: {ex.StackTrace}",
        //                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return new List<HistorialCliente>();
        //            }
        //            finally
        //            {
        //                desconectar();
        //            }
        //        }

      

        public List<HistorialCliente> ObtenerHistorialBasicoDesdeCheckOut(string rfc, int anio)
        {
            if (!conectar())
                return new List<HistorialCliente>();

            try
            {
                LocalDate fechaInicio = new LocalDate(anio, 1, 1);
                LocalDate fechaFin = new LocalDate(anio + 1, 1, 1);

                string consulta = $@"
        SELECT * FROM checkOut 
        WHERE rfcliente = '{rfc}' 
          AND fecha_check_out >= '{fechaInicio:yyyy-MM-dd}' 
          AND fecha_check_out < '{fechaFin:yyyy-MM-dd}' 
        ALLOW FILTERING;";

                var result = _session.Execute(consulta);
                List<HistorialCliente> historial = new List<HistorialCliente>();

                foreach (var row in result)
                {
                    try
                    {
                        var hotelId = row.GetValue<int>("id_hotel");
                        var hotel = GetHotelById(hotelId);
                        var cliente = GetClientByRFC(rfc);

                        historial.Add(new HistorialCliente
                        {
                            RFC = rfc,
                            NombreCliente = cliente?.name + " " + cliente?.lastname ?? "Cliente no encontrado",
                            Ciudad = cliente?.city ?? "No especificada",
                            NombreHotel = hotel?.name ?? "Hotel no encontrado",
                            FechaCheckOut = row.GetValue<LocalDate>("fecha_check_out").ToDateTimeOffset().Date,
                            TotalFactura = row.GetValue<decimal>("totalcobrado"),
                            Anticipo = row.GetValue<decimal>("anticipo"),
                            // Puedes agregar más campos según lo que necesites
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error procesando fila básica: {ex.Message}");
                        continue;
                    }
                }

                return historial;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener historial básico: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<HistorialCliente>();
            }
            finally
            {
                desconectar();
            }
        }
        public bool GuardarCheckOutCompleto(CHECKOUT2 checkout)
        {
            if (!conectar())
            {
                MessageBox.Show("No se pudo conectar a la base de datos.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                // 1. Convertir listas a formato CQL
                string habitacionesCql = checkout.Habitaciones != null && checkout.Habitaciones.Count > 0
                    ? "['" + string.Join("', '", checkout.Habitaciones.Select(h => h.Replace("'", "''"))) + "']"
                    : "[]";

                string serviciosCql = checkout.Servicios != null && checkout.Servicios.Count > 0
                    ? "['" + string.Join("', '", checkout.Servicios.Select(s => s.Replace("'", "''"))) + "']"
                    : "[]";

                // 2. Insertar en checkout
                string cqlCheckout = $@"
INSERT INTO checkout (
    id_factura, id_reserva, id_hotel, fecha_check_out, rfcliente,
    habitaciones, total, anticipo, formadepago, servicios,
    totalservicios, formapagoservicios, totalcobrado, rutapdf
) VALUES (
    {checkout.ID_factura},
    {checkout.ID_Reserva},
    {checkout.ID_Hotel},
    '{checkout.Fecha_Check_Out:yyyy-MM-dd}',
    '{checkout.RFCliente}',
    {habitacionesCql},
    {checkout.Total.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    {checkout.Anticipo.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    '{checkout.Formadepago.Replace("'", "''")}',
    {serviciosCql},
    {checkout.TotalServicios.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    '{checkout.FormapagoServicios.Replace("'", "''")}',
    {checkout.TotalCobrado.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    '{checkout.RUTAPDF.Replace("'", "''")}'
);";

                _session.Execute(cqlCheckout);
                Console.WriteLine("✓ Checkout guardado");

                // 3. Actualizar estado de la reservación
                string updateStatus = $"UPDATE reservations SET status = 'CheckOut' WHERE id = {checkout.ID_Reserva};";
                _session.Execute(updateStatus);
                Console.WriteLine("✓ Estado de reservación actualizado");

                // 4. Obtener datos para el historial (SIN DESCONECTAR)
                var reserva = ObtenerReservacionPorCodigo2SinConexion(checkout.ID_Reserva);
                if (reserva == null)
                {
                    MessageBox.Show("No se encontró la reservación para el historial.");
                    return false;
                }
                Console.WriteLine($"✓ Reserva encontrada: {reserva.Id}");

                var cliente = GetClientByRFCSinConexion(checkout.RFCliente);
                if (cliente == null)
                {
                    MessageBox.Show("No se encontró el cliente para el historial.");
                    return false;
                }
                Console.WriteLine($"✓ Cliente encontrado: {cliente.name} {cliente.lastname}");

                var hotel = GetHotelByIdSinConexion(checkout.ID_Hotel);
                if (hotel == null)
                {
                    MessageBox.Show("No se encontró el hotel para el historial.");
                    return false;
                }
                Console.WriteLine($"✓ Hotel encontrado: {hotel.name}");

                // 5. Extraer tipo y número de habitación
                string tipoHabitacion = "No especificado";
                string numeroHabitacion = "No especificado";

                if (checkout.Habitaciones != null && checkout.Habitaciones.Count > 0)
                {
                    string primerHabitacion = checkout.Habitaciones[0];
                    int indexGuion = primerHabitacion.IndexOf('-');

                    if (indexGuion > 0)
                    {
                        numeroHabitacion = primerHabitacion.Substring(0, indexGuion).Trim();
                        tipoHabitacion = primerHabitacion.Substring(indexGuion + 1).Trim();
                    }
                    else
                    {
                        numeroHabitacion = primerHabitacion.Trim();
                    }
                }

                // 6. Insertar en historial completo
                int anio = checkout.Fecha_Check_Out.Year;
                decimal montoHospedaje = checkout.Total - checkout.TotalServicios;

                string insertHistorial = $@"
INSERT INTO historialClienteCompleto (
    rfcCliente, anio, fechaCheckOut, codigoReservacion,
    nombreCliente, ciudad, idHotel, nombreHotel,
    tipoHabitacion, numeroHabitacion, numPersonasHospedadas,
    fechaReservacion, fechaCheckIn, estatusReservacion,
    anticipo, montoHospedaje, montoServiciosAdicionales, totalFactura
) VALUES (
    '{checkout.RFCliente}',
    {anio},
    '{checkout.Fecha_Check_Out:yyyy-MM-dd}',
    {checkout.ID_Reserva},
    '{cliente.name.Replace("'", "''")} {cliente.lastname.Replace("'", "''")}',
    '{cliente.city.Replace("'", "''")}',
    {checkout.ID_Hotel},
    '{hotel.name.Replace("'", "''")}',
    '{tipoHabitacion.Replace("'", "''")}',
    '{numeroHabitacion.Replace("'", "''")}',
    {reserva.NumGuests},
    '{reserva.DateReserved:yyyy-MM-dd HH:mm:ss}',
    '{reserva.CheckIn:yyyy-MM-dd HH:mm:ss}',
    'Completada',
    {checkout.Anticipo.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    {montoHospedaje.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    {checkout.TotalServicios.ToString(System.Globalization.CultureInfo.InvariantCulture)},
    {checkout.TotalCobrado.ToString(System.Globalization.CultureInfo.InvariantCulture)}
);";

                _session.Execute(insertHistorial);
                Console.WriteLine("✓ Historial insertado correctamente");

                MessageBox.Show("Check-Out y historial guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar checkout completo: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

        // Estos métodos asumen que ya hay una conexión activa
        private Reservation ObtenerReservacionPorCodigo2SinConexion(Guid id)
        {
            try
            {
                string query = $"SELECT * FROM reservations WHERE id = {id};";
                var row = _session.Execute(query).FirstOrDefault();

                if (row == null)
                    return null;

                return new Reservation
                {
                    Id = row.GetValue<Guid>("id"),
                    RfcClient = row.GetValue<string>("rfcclient"),
                    name = row.GetValue<string>("name"),
                    EmailUser = row.GetValue<string>("emailuser"),
                    DateReserved = row.GetValue<DateTime>("datereserved"),
                    IdHotel = row.GetValue<int>("idhotel"),
                    Nights = row.GetValue<short>("nights"),
                    EntryDate = row.GetValue<LocalDate>("entrydate"),
                    EndDate = row.GetValue<LocalDate>("enddate"),
                    NumRooms = row.GetValue<short>("numrooms"),
                    RoomType = row.GetValue<List<string>>("roomtype"),
                    NumGuests = row.GetValue<short>("numguests"),
                    CheckIn = row.GetValue<DateTime>("checkin"),
                    CheckOut = row.GetValue<DateTime>("checkout"),
                    PayForm = row.GetValue<string>("payform"),
                    SubTotal = row.GetValue<int>("subtotal"),
                    anticipo = row.GetValue<decimal>("anticipo"),
                    total = row.GetValue<decimal>("total"),
                    Status = row.GetValue<string>("status")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerReservacionPorCodigo2SinConexion: {ex.Message}");
                return null;
            }
        }

        private Client GetClientByRFCSinConexion(string rfc)
        {
            try
            {
                string query = $"SELECT * FROM clients WHERE rfc = '{rfc}';";
                IMapper mapper = new Mapper(_session);
                Client clientData = mapper.SingleOrDefault<Client>(query);
                return clientData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetClientByRFCSinConexion: {ex.Message}");
                return null;
            }
        }

        private Hotel GetHotelByIdSinConexion(int id)
        {
            try
            {
                string query = $"SELECT country, city, state, name FROM hotelesbyid WHERE id = {id};";
                IMapper mapper = new Mapper(_session);
                var hotelInfo = mapper.SingleOrDefault<hotelsbyid>(query);

                if (hotelInfo == null)
                    return null;

                string query2 = $"SELECT * FROM hoteles WHERE country = '{hotelInfo.country}' AND city = '{hotelInfo.city}' AND state = '{hotelInfo.state}' AND name = '{hotelInfo.name}';";
                Hotel hotelCompleto = mapper.SingleOrDefault<Hotel>(query2);

                if (hotelCompleto != null)
                {
                    hotelCompleto.id = id;
                }

                return hotelCompleto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetHotelByIdSinConexion: {ex.Message}");
                return null;
            }
        }
        //internal CHECKOUT2 ObtenerCheckOutPorId(int idFactura)
        //{
        //    conectar();
        //    string query = $"SELECT * FROM checkout WHERE id_factura = {idFactura};";
        //    var row = _session.Execute(query).FirstOrDefault();

        //    if (row == null)
        //    {
        //        desconectar();
        //        return null;
        //    }

        //    CHECKOUT2 checkout = new CHECKOUT2
        //    {
        //        ID_factura = row.GetValue<Guid>("id_factura"), // Corrected type from int to Guid  
        //        ID_Reserva = row.GetValue<Guid>("id_reserva"),
        //        ID_Hotel = row.GetValue<int>("id_hotel"),
        //        Fecha_Check_Out = row.GetValue<LocalDate>("fecha_check_out").ToDateTimeOffset().Date,
        //        RFCliente = row.GetValue<string>("rfcliente"),
        //        Habitaciones = row.GetValue<List<string>>("habitaciones"),
        //        Total = row.GetValue<decimal>("total"),
        //        Anticipo = row.GetValue<decimal>("anticipo"),
        //        Formadepago = row.GetValue<string>("formadepago"),
        //        Servicios = row.GetValue<List<string>>("servicios"),
        //        TotalServicios = row.GetValue<decimal>("totalservicios"),
        //        FormapagoServicios = row.GetValue<string>("formapagoservicios"),
        //        TotalCobrado = row.GetValue<decimal>("totalcobrado"),
        //        RUTAPDF = row.GetValue<string>("rutapdf")
        //    };

        //    desconectar();
        //    return checkout;
        //}


        //private void CalcularYMostrarOcupacionPorTipo(int idHotel, DateTime fechaInicio, DateTime fechaFin)
        //{
        //    var disponiblesPorTipo = new Dictionary<string, int>();
        //    var ocupadasPorTipo = new Dictionary<string, int>();

        //    // 1. Obtener habitaciones disponibles por tipo
        //    var queryHabitaciones = "SELECT tipo FROM habitaciones WHERE id_hotel = ?";
        //    var stmtHab = session.Prepare(queryHabitaciones);
        //    var resultHab = session.Execute(stmtHab.Bind(idHotel));

        //    foreach (var row in resultHab)
        //    {
        //        string tipo = row.GetValue<string>("tipo");
        //        if (!disponiblesPorTipo.ContainsKey(tipo))
        //            disponiblesPorTipo[tipo] = 0;
        //        disponiblesPorTipo[tipo]++;
        //    }

        //    // 2. Obtener habitaciones ocupadas desde checkouts
        //    var queryCheckouts = "SELECT habitaciones FROM checkouts WHERE id_hotel = ? AND fecha_check_out >= ? AND fecha_check_out <= ?";
        //    var stmtCheckouts = session.Prepare(queryCheckouts);
        //    var resultCheckouts = session.Execute(stmtCheckouts.Bind(idHotel, fechaInicio, fechaFin));

        //    foreach (var row in resultCheckouts)
        //    {
        //        var habitaciones = row.GetValue<List<string>>("habitaciones"); // o List<int> si ya es entero

        //        foreach (var hab in habitaciones)
        //        {
        //            // Obtener tipo de la habitación
        //            var tipoQuery = "SELECT tipo FROM habitaciones WHERE id_hotel = ? AND numero_habitacion = ?";
        //            int numeroHabitacion;
        //            if (!int.TryParse(hab, out numeroHabitacion)) continue;

        //            var stmtTipo = session.Prepare(tipoQuery);
        //            var resultTipo = session.Execute(stmtTipo.Bind(idHotel, numeroHabitacion));

        //            string tipo = resultTipo.FirstOrDefault()?.GetValue<string>("tipo");
        //            if (tipo == null) continue;

        //            if (!ocupadasPorTipo.ContainsKey(tipo))
        //                ocupadasPorTipo[tipo] = 0;
        //            ocupadasPorTipo[tipo]++;
        //        }
        //    }

        //    // 3. Calcular ocupación y llenar el gráfico
        //    chartOcupacion.Series.Clear();
        //    var serie = chartOcupacion.Series.Add("Ocupación");
        //    serie.ChartType = SeriesChartType.Column;

        //    foreach (var tipo in disponiblesPorTipo.Keys)
        //    {
        //        int disponibles = disponiblesPorTipo[tipo];
        //        int ocupadas = ocupadasPorTipo.ContainsKey(tipo) ? ocupadasPorTipo[tipo] : 0;

        //        double porcentaje = disponibles > 0 ? (ocupadas * 100.0 / disponibles) : 0;
        //        serie.Points.AddXY(tipo, porcentaje);
        //    }
        //}

    }
}
