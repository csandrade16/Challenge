using ProfitSharing.Domain.DTOs;

namespace ProfitSharing.Domain.Interfaces
{
    public interface IProfitSharingService
    {
        Task<ProfitSharingResultDTO> CalculateProfitSharing(decimal avaiableSum);
    }
}
