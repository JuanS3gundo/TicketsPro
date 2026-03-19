using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class CategoriaItemRepository : BaseRepository<CategoriaItem>, ICategoriaItemRepository
    {
        private const string SelectAll = "SELECT Id, Nombre, Descripcion FROM CategoriaItem";
        public CategoriaItemRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public IEnumerable<CategoriaItem> GetAll()
        {
            return QueryList(SelectAll + " ORDER BY Nombre ASC", MapFromReader);
        }
        public CategoriaItem GetById(Guid id)
        {
            return QuerySingle(SelectAll + " WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", id), MapFromReader);
        }
        public CategoriaItem GetByNombre(string nombre)
        {
            return QuerySingle(SelectAll + " WHERE Nombre = @Nombre",
                cmd => AddParam(cmd, "@Nombre", nombre), MapFromReader);
        }
        public void Add(CategoriaItem entity)
        {
            const string sql = @"
INSERT INTO CategoriaItem (Id, Nombre, Descripcion)
VALUES (@Id, @Nombre, @Descripcion);";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@Nombre", entity.Nombre);
                AddParam(cmd, "@Descripcion", entity.Descripcion);
            });
        }
        public void Update(CategoriaItem entity)
        {
            const string sql = @"
UPDATE CategoriaItem SET Nombre = @Nombre, Descripcion = @Descripcion WHERE Id = @Id;";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@Nombre", entity.Nombre);
                AddParam(cmd, "@Descripcion", entity.Descripcion);
            });
        }
        public void Remove(CategoriaItem entity)
        {
            ExecuteNonQuery("DELETE FROM CategoriaItem WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        private CategoriaItem MapFromReader(SqlDataReader reader)
        {
            return new CategoriaItem
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = ReadString(reader, "Nombre"),
                Descripcion = reader["Descripcion"] as string
            };
        }
    }
}
