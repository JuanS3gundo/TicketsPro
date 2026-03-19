using System;
using Services.BLL;
namespace BLL.Exceptions
{

    public class TransicionEstadoInvalidaException : BLLException
    {
        public string EstadoActual { get; }
        public string EstadoDestino { get; }
        public TransicionEstadoInvalidaException(string estadoActual, string estadoDestino, string razon)
            : base(string.Format(LanguageBLL.Translate("Ticket_Transicion_MensajeException"), estadoActual, estadoDestino, razon))
        {
            EstadoActual = estadoActual;
            EstadoDestino = estadoDestino;
        }
    }
}
