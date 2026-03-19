using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Implementations.SqlServer.Repositories;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.UnitOfWork
{
    public class UnitOfWorkSqlRepository : IUnitOfWorkRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        public ITicketRepository TicketRepository { get; private set; }
        public IEquipoInformaticoRepository EquipoInformaticoRepository { get; private set; }
        public ISolucionTicketRepository SolucionTicketRepository { get; private set; }
        public IInventarioItemRepository InventarioItemRepository { get; private set; }
        public IPrioridadTicketRepository PrioridadTicketRepository { get; private set; }
        public IPoliticaSLARepository PoliticaSLARepository { get; private set; }
        public IComentarioTicketRepository ComentarioTicketRepository { get; private set; }
        public IAdjuntoTicketRepository AdjuntoTicketRepository { get; private set; }
        public IEstadoTicketRepository EstadoTicketRepository { get; private set; }
        public ICategoriaTicketRepository CategoriaTicketRepository { get; private set; }
        public IUbicacionRepository UbicacionRepository { get; private set; }
        public IUsuarioRepository UsuarioRepository { get; private set; }
        public ICategoriaItemRepository CategoriaItemRepository { get; private set; }
        public IUbicacionEquipoRepository UbicacionEquipoRepository { get; private set; }
        public ITipoEquipoRepository TipoEquipoRepository { get; private set; }
        public UnitOfWorkSqlRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
            TicketRepository = new TicketRepository(_connection, _transaction);
            EquipoInformaticoRepository = new EquipoInformaticoRepository(_connection, _transaction);
            SolucionTicketRepository = new SolucionTicketRepository(_connection, _transaction);
            InventarioItemRepository = new InventarioItemRepository(_connection, _transaction);
            PrioridadTicketRepository = new PrioridadTicketRepository(_connection, _transaction);
            PoliticaSLARepository = new PoliticaSLARepository(_connection, _transaction);
            ComentarioTicketRepository = new ComentarioTicketRepository(_connection, _transaction);
            AdjuntoTicketRepository = new AdjuntoTicketRepository(_connection, _transaction);
            EstadoTicketRepository = new EstadoTicketRepository(_connection, _transaction);
            CategoriaTicketRepository = new CategoriaTicketRepository(_connection, _transaction);
            UbicacionRepository = new UbicacionRepository(_connection, _transaction);
            UsuarioRepository = new UsuarioRepository(_connection, _transaction);
            CategoriaItemRepository = new CategoriaItemRepository(_connection, _transaction);
            UbicacionEquipoRepository = new UbicacionEquipoRepository(_connection, _transaction);
            TipoEquipoRepository = new TipoEquipoRepository(_connection, _transaction);
        }
    }
}
