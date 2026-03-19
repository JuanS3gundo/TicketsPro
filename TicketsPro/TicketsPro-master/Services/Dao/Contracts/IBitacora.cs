using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Dao.Contracts
{
    public interface IBitacora
    {
        void Agregar(Bitacora bitacora);
        List<Bitacora> ObtenerTodos(int pagina = 1, int registrosPorPagina = 100);
        List<Bitacora> ObtenerPorUsuario(string usuario);
        List<Bitacora> ObtenerPorFiltros(DateTime? fechaDesde, DateTime? fechaHasta, string usuario, string nivel, string accion, int topN = 1000);
        List<Bitacora> ObtenerPorEquipoId(int equipoId);
    }
}
