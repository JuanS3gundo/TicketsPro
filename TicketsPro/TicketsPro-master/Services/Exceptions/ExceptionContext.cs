namespace Services.DomainModel
{
    public class ExceptionContext
    {
        public enum Category
        {
            Business,
            Technical,
            Unexpected
        }
        public enum Severity
        {
            Info,
            Warning,
            Error,
            Critical
        }
        public Category CategoryType { get; set; }
        public Severity SeverityLevel { get; set; }
        public string OperationName { get; set; }
        public System.DateTime Timestamp { get; set; } = System.DateTime.UtcNow;
        public System.Guid? UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }
        public string Entidad { get; set; }
        public System.Guid? EntidadId { get; set; }
    }
}
