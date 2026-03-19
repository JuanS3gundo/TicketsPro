using System;
using Services.DomainModel;
namespace Services.Services
{

    public interface IExceptionService
    {

        void Handle(Exception ex, ExceptionContext context);
    }
}
