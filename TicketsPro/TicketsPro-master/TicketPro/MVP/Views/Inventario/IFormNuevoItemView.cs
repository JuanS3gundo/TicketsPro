using Entity.Domain;
using System.Collections.Generic;
namespace TicketPro.MVP.Views
{
    public interface IFormNuevoItemView
    {
        string Codigo { get; }
        string Nombre { get; }
        string CategoriaSeleccionada { get; }
        string Ubicacion { get; }
        int Cantidad { get; }
        string Descripcion { get; }
        bool Disponible { get; }
        object ValorSeleccionado { get; }
        object UnidadSeleccionada { get; }
        void LlenarCategorias(string[] categorias);
        void ConfigurarControlesMemoria(bool habilitar, List<int> potenciasDos, string[] unidades);
        void MostrarError(string mensaje, string titulo = "Error");
        void MostrarAdvertencia(string mensaje, string titulo = "Advertencia");
        void NotificarExitoYAsignarItem(string mensaje, string titulo, InventarioItem item);
    }
}
