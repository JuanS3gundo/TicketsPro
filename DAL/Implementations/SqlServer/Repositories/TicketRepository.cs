using DAL.Contracts.Repositories;
using DAL.Helpers;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.Implementations.SqlServer.Repositories
{
    public class TicketRepository : IGenericRepository<Ticket>
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public TicketRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
        public void Add(Ticket entity)
        {
            string commandText = @"INSERT INTO Ticket (IdTicket, Titulo, Descripcion, FechaApertura, Categoria, Estado, Ubicacion, TecnicoId) 
                           VALUES (@IdTicket, @Titulo, @Descripcion, @FechaApertura, @Categoria, @Estado, @Ubicacion, @TecnicoId)";

            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdTicket", entity.IdTicket),
                new SqlParameter("@Titulo", entity.Titulo ?? (object)DBNull.Value),
                new SqlParameter("@Descripcion", entity.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@FechaApertura", entity.FechaApertura),
                new SqlParameter("@Categoria", (int)entity.Categoria),
                new SqlParameter("@Estado", (int)entity.Estado),
                new SqlParameter("@Ubicacion", (int)entity.Ubicacion),
                new SqlParameter("@TecnicoId", (object)entity.TecnicoAsignado?.IdTecnico ?? DBNull.Value));
        }

        public IEnumerable<Ticket> Find(Expression<Func<Ticket, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> GetAll()
        {
            var tickets = new List<Ticket>();

            using (var command = _connection.CreateCommand())
            {
                command.Transaction = _transaction;
                command.CommandText = "SELECT * FROM Ticket";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tickets.Add(new Ticket
                        {
                            IdTicket = reader.GetGuid(reader.GetOrdinal("IdTicket")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                            Categoria = (Enums.Categoria)reader.GetInt32(reader.GetOrdinal("Categoria")),
                            Estado = (Enums.Estado)reader.GetInt32(reader.GetOrdinal("Estado")),
                            Ubicacion = (Enums.Ubicacion)reader.GetInt32(reader.GetOrdinal("Ubicacion")),
                            TecnicoAsignado = new Tecnico { IdTecnico = reader.GetInt32(reader.GetOrdinal("TecnicoId")) }
                        });
                    }
                }
            }

            return tickets;
        }

        public Ticket GetById(Guid id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.Transaction = _transaction;
                command.CommandText = "SELECT * FROM Ticket WHERE IdTicket = @IdTicket";
                command.Parameters.AddWithValue("@IdTicket", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Ticket
                        {
                            IdTicket = reader.GetGuid(reader.GetOrdinal("IdTicket")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                            Categoria = (Enums.Categoria)reader.GetInt32(reader.GetOrdinal("Categoria")),
                            Estado = (Enums.Estado)reader.GetInt32(reader.GetOrdinal("Estado")),
                            Ubicacion = (Enums.Ubicacion)reader.GetInt32(reader.GetOrdinal("Ubicacion")),
                            TecnicoAsignado = new Tecnico { IdTecnico = reader.GetInt32(reader.GetOrdinal("TecnicoId")) }
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<Ticket> GetTicketsByCategoria(Enums.Categoria categoria)
        {
            var tickets = new List<Ticket>();

            using (var command = _connection.CreateCommand())
            {
                command.Transaction = _transaction;
                command.CommandText = "SELECT * FROM Ticket WHERE Categoria = @Categoria";
                command.Parameters.AddWithValue("@Categoria", (int)categoria);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tickets.Add(new Ticket
                        {
                            IdTicket = reader.GetGuid(reader.GetOrdinal("IdTicket")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                            Categoria = (Enums.Categoria)reader.GetInt32(reader.GetOrdinal("Categoria")),
                            Estado = (Enums.Estado)reader.GetInt32(reader.GetOrdinal("Estado")),
                            Ubicacion = (Enums.Ubicacion)reader.GetInt32(reader.GetOrdinal("Ubicacion")),
                            TecnicoAsignado = new Tecnico { IdTecnico = reader.GetInt32(reader.GetOrdinal("TecnicoId")) }
                        });
                    }
                }
            }

            return tickets;
        }

        public IEnumerable<Ticket> GetTicketsByEstado(Enums.Estado estado)
        {
            var tickets = new List<Ticket>();

            using (var command = _connection.CreateCommand())
            {
                command.Transaction = _transaction;
                command.CommandText = "SELECT * FROM Ticket WHERE Estado = @Estado";
                command.Parameters.AddWithValue("@Estado", (int)estado);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tickets.Add(new Ticket
                        {
                            IdTicket = reader.GetGuid(reader.GetOrdinal("IdTicket")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                            Categoria = (Enums.Categoria)reader.GetInt32(reader.GetOrdinal("Categoria")),
                            Estado = (Enums.Estado)reader.GetInt32(reader.GetOrdinal("Estado")),
                            Ubicacion = (Enums.Ubicacion)reader.GetInt32(reader.GetOrdinal("Ubicacion")),
                            TecnicoAsignado = new Tecnico { IdTecnico = reader.GetInt32(reader.GetOrdinal("TecnicoId")) }
                        });
                    }
                }
            }

            return tickets;
        }

        public IEnumerable<Ticket> GetTicketsByFecha(DateTime fechaApertura)
        {
            var tickets = new List<Ticket>();

            using (var command = _connection.CreateCommand())
            {
                command.Transaction = _transaction;
                command.CommandText = "SELECT * FROM Ticket WHERE FechaApertura = @FechaApertura";
                command.Parameters.AddWithValue("@FechaApertura", fechaApertura);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tickets.Add(new Ticket
                        {
                            IdTicket = reader.GetGuid(reader.GetOrdinal("IdTicket")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                            Categoria = (Enums.Categoria)reader.GetInt32(reader.GetOrdinal("Categoria")),
                            Estado = (Enums.Estado)reader.GetInt32(reader.GetOrdinal("Estado")),
                            Ubicacion = (Enums.Ubicacion)reader.GetInt32(reader.GetOrdinal("Ubicacion")),
                            TecnicoAsignado = new Tecnico { IdTecnico = reader.GetInt32(reader.GetOrdinal("TecnicoId")) }
                        });
                    }
                }
            }

            return tickets;
        }

        public IEnumerable<Ticket> GetTicketsByTecnico(int tecnicoId)
        {
            var tickets = new List<Ticket>();

            using (var command = _connection.CreateCommand())
            {
                command.Transaction = _transaction;
                command.CommandText = "SELECT * FROM Ticket WHERE TecnicoId = @TecnicoId";
                command.Parameters.AddWithValue("@TecnicoId", tecnicoId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tickets.Add(new Ticket
                        {
                            IdTicket = reader.GetGuid(reader.GetOrdinal("IdTicket")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                            Categoria = (Enums.Categoria)reader.GetInt32(reader.GetOrdinal("Categoria")),
                            Estado = (Enums.Estado)reader.GetInt32(reader.GetOrdinal("Estado")),
                            Ubicacion = (Enums.Ubicacion)reader.GetInt32(reader.GetOrdinal("Ubicacion")),
                            TecnicoAsignado = new Tecnico { IdTecnico = reader.GetInt32(reader.GetOrdinal("TecnicoId")) }
                        });
                    }
                }
            }

            return tickets;
        }

        public void Remove(Ticket entity)
        {
            using (var command = _connection.CreateCommand())
            {
                command.Transaction = _transaction;
                command.CommandText = "DELETE FROM Ticket WHERE IdTicket = @IdTicket";
                command.Parameters.AddWithValue("@IdTicket", entity.IdTicket);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Ticket entity)
        {
            using (var command = _connection.CreateCommand())
            {
                command.Transaction = _transaction;
                command.CommandText = @"UPDATE Ticket 
                                        SET Titulo = @Titulo, Descripcion = @Descripcion, FechaApertura = @FechaApertura, 
                                            Categoria = @Categoria, Estado = @Estado, Ubicacion = @Ubicacion, TecnicoId = @TecnicoId 
                                        WHERE IdTicket = @IdTicket";
                command.Parameters.AddWithValue("@IdTicket", entity.IdTicket);
                command.Parameters.AddWithValue("@Titulo", entity.Titulo);
                command.Parameters.AddWithValue("@Descripcion", entity.Descripcion);
                command.Parameters.AddWithValue("@FechaApertura", entity.FechaApertura);
                command.Parameters.AddWithValue("@Categoria", (int)entity.Categoria);
                command.Parameters.AddWithValue("@Estado", (int)entity.Estado);
                command.Parameters.AddWithValue("@Ubicacion", (int)entity.Ubicacion);
                command.Parameters.AddWithValue("@TecnicoId", entity.TecnicoAsignado?.IdTecnico);

                command.ExecuteNonQuery();
            }
        }
    }
}
