using Adfenix.RequestModels.QueryRequestModels;

namespace Adfenix.Services.Interface
{
    public interface IDataReadService
    {
        Task<string> GetServerCountById(int id);
        Task<string> GetServerCountById(ZendeskQueueCountRequestDto input);
    }
}
