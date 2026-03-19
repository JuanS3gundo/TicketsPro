using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class UbicacionRepository : BaseRepository<Ubicacion>, IUbicacionRepository
    {
        private const string SelectAll = "SELECT Id, Nombre, Descripcion FROM Ubicacion";
        public UbicacionRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(Ubicacion entity)
        {
            ExecuteNonQuery("INSERT INTO Ubicacion (Id, Nombre, Descripcion) VALUES (@Id, @Nombre, @Descripcion);",
                cmd =>
                {
                    AddParam(cmd, "@Id", entity.Id);
                    AddParam(cmd, "@Nombre", entity.Nombre);
                    AddParam(cmd, "@Descripcion", entity.Descripcion);
                });
        }
        public void Update(Ubicacion entity)
        {
            ExecuteNonQuery("UPDATE Ubicacion SET Nombre=@Nombre, Descripcion=@Descripcion WHERE Id=@Id;",
                cmd =>
                {
                    AddParam(cmd, "@Id", entity.Id);
                    AddParam(cmd, "@Nombre", entity.Nombre);
                    AddParam(cmd, "@Descripcion", entity.Descripcion);
                });
        }
        public Ubicacion GetById(Guid id)
        {
            return QuerySingle(SelectAll + " WHERE Id = @Id;",
                cmd => AddParam(cmd, "@Id", id), MapFromReader);
        }
        public Ubicacion GetByNombre(string nombre)
        {
            return QuerySingle(SelectAll + " WHERE Nombre = @Nombre;",
                cmd => AddParam(cmd, "@Nombre", nombre), MapFromReader);
        }
        public IEnumerable<Ubicacion> GetAll()
        {
            return QueryList(SelectAll + " ORDER BY Nombre ASC;", MapFromReader);
        }
        public void Remove(Ubicacion entity)
        {
            ExecuteNonQuery("DELETE FROM Ubicacion WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        private Ubicacion MapFromReader(SqlDataReader reader)
        {
            return new Ubicacion
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = ReadString(reader, "Nombre"),
                Descripcion = reader["Descripcion"] as string
            };
        }
    }
}
