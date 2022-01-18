using Adfenix.RequestModels.CommandRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Services.Interface
{
    public interface IDataWriteService
    {
        Task SendDataAsync(SendDataRequestDto data);
        
    }
}
