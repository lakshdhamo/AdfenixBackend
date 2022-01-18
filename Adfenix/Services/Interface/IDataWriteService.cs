using Adfenix.RequestModels.CommandRequestModels;

namespace Adfenix.Services.Interface
{
    public interface IDataWriteService
    {
        Task SendDataAsync(SendDataRequestDto data);
        
    }
}
