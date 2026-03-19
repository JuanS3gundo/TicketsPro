using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class UbicacionEquipoRepository : BaseRepository<UbicacionEquipo>, IUbicacionEquipoRepository
    {
        private const string SelectAll = "SELECT Id, Nombre, Descripcion FROM UbicacionEquipo";
        public UbicacionEquipoRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public IEnumerable<UbicacionEquipo> GetAll()
        {
            return QueryList(SelectAll + " ORDER BY Nombre ASC", MapFromReader);
        }
        public UbicacionEquipo GetById(Guid id)
        {
            return QuerySingle(SelectAll + " WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", id), MapFromReader);
        }
        public UbicacionEquipo GetByNombre(string nombre)
        {
            return QuerySingle(SelectAll + " WHERE Nombre = @Nombre",
                cmd => AddParam(cmd, "@Nombre", nombre), MapFromReader);
        }
        public void Add(UbicacionEquipo entity)
        {
            ExecuteNonQuery("INSERT INTO UbicacionEquipo (Id, Nombre, Descripcion) VALUES (@Id, @Nombre, @Descripcion);",
                cmd =>
                {
                    AddParam(cmd, "@Id", entity.Id);
                    AddParam(cmd, "@Nombre", entity.Nombre);
                    AddParam(cmd, "@Descripcion", entity.Descripcion);
                });
        }
        public void Update(UbicacionEquipo entity)
        {
            ExecuteNonQuery("UPDATE UbicacionEquipo SET Nombre=@Nombre, Descripcion=@Descripcion WHERE Id=@Id;",
                cmd =>
                {
                    AddParam(cmd, "@Id", entity.Id);
                    AddParam(cmd, "@Nombre", entity.Nombre);
                    AddParam(cmd, "@Descripcion", entity.Descripcion);
                });
        }
        public void Remove(UbicacionEquipo entity)
        {
            ExecuteNonQuery("DELETE FROM UbicacionEquipo WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        private UbicacionEquipo MapFromReader(SqlDataReader reader)
        {
            return new UbicacionEquipo
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = ReadString(reader, "Nombre"),
                Descripcion = reader["Descripcion"] as string
            };
        }
    }
}
