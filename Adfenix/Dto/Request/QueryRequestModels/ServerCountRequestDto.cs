using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.RequestModels.QueryRequestModels
{
    public class ServerCountRequestDto : IRequest<string>
    {
        public int ServerId { get; set; }

    }
}
