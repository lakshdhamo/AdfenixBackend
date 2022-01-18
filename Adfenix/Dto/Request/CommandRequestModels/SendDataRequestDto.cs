using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.RequestModels.CommandRequestModels
{
    public class SendDataRequestDto : IRequest
    {
        public string? Metric { get; set; }
        public string? Value { get; set; }
        public int epochTimestamp { get; set; } = 0;
        public string? VisualiserSeriesUri { get; set; }   
        public string ? VisualiserApiKey { get; set; }

    }
}
