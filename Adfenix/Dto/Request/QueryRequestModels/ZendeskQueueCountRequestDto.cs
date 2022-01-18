using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.RequestModels.QueryRequestModels
{
    public class ZendeskQueueCountRequestDto : IRequest<string>
    {
        public string Url { get; set; }
        public string Token { get; set; }
    }
}
