using Entity.Domain;
using System;
namespace DAL.Contracts.Repositories
{
    public interface IPoliticaSLARepository : IGenericRepository<PoliticaSLA, Guid>
    {
        PoliticaSLA GetByPrioridadId(Guid prioridadId);
    }
}
