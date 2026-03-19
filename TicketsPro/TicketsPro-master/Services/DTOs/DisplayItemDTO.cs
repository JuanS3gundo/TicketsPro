namespace Services.DTOs
{
    /// <summary>
    /// DTO genérico para mostrar items simples en listas y combos de la UI.
    /// Evita crear múltiples DTOs con solo Id y Nombre.
    /// </summary>
    /// <typeparam name="TKey">Tipo de la clave primaria (Guid, int, etc.)</typeparam>
    public class DisplayItemDTO<TKey>
    {
        public TKey Id { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }

        public DisplayItemDTO() { }

        public DisplayItemDTO(TKey id, string display, string description = null)
        {
            Id = id;
            Display = display;
            Description = description;
        }

        public override string ToString() => Display;
    }

    /// <summary>
    /// Versión con Guid para facilitar el uso más común
    /// </summary>
    public class DisplayItemDTO : DisplayItemDTO<System.Guid>
    {
        public DisplayItemDTO() { }
        public DisplayItemDTO(System.Guid id, string display, string description = null)
            : base(id, display, description) { }
    }
}
