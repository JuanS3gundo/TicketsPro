using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class TipoEquipoRepository : BaseRepository<TipoEquipo>, ITipoEquipoRepository
    {
        private const string SelectAll = "SELECT Id, Nombre, Descripcion FROM TipoEquipo";
        public TipoEquipoRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public IEnumerable<TipoEquipo> GetAll()
        {
            return QueryList(SelectAll + " ORDER BY Nombre ASC", MapFromReader);
        }
        public TipoEquipo GetById(Guid id)
        {
            return QuerySingle(SelectAll + " WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", id), MapFromReader);
        }
        public TipoEquipo GetByNombre(string nombre)
        {
            return QuerySingle(SelectAll + " WHERE Nombre = @Nombre",
                cmd => AddParam(cmd, "@Nombre", nombre), MapFromReader);
        }
        public void Add(TipoEquipo entity)
        {
            ExecuteNonQuery("INSERT INTO TipoEquipo (Id, Nombre, Descripcion) VALUES (@Id, @Nombre, @Descripcion);",
                cmd =>
                {
                    AddParam(cmd, "@Id", entity.Id);
                    AddParam(cmd, "@Nombre", entity.Nombre);
                    AddParam(cmd, "@Descripcion", entity.Descripcion);
                });
        }
        public void Update(TipoEquipo entity)
        {
            ExecuteNonQuery("UPDATE TipoEquipo SET Nombre=@Nombre, Descripcion=@Descripcion WHERE Id=@Id;",
                cmd =>
                {
                    AddParam(cmd, "@Id", entity.Id);
                    AddParam(cmd, "@Nombre", entity.Nombre);
                    AddParam(cmd, "@Descripcion", entity.Descripcion);
                });
        }
        public void Remove(TipoEquipo entity)
        {
            ExecuteNonQuery("DELETE FROM TipoEquipo WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        private TipoEquipo MapFromReader(SqlDataReader reader)
        {
            return new TipoEquipo
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = ReadString(reader, "Nombre"),
                Descripcion = reader["Descripcion"] as string
            };
        }
    }
}
