using Adfenix.RequestModels.CommandRequestModels;
using Adfenix.RequestModels.QueryRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Services.Interface
{
    public interface IDataReadService
    {
        Task<string> GetServerCountById(int id);
        Task<string> GetServerCountById(ZendeskQueueCountRequestDto input);
    }
}
