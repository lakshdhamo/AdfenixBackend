using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.RequestModels.CommandRequestModels
{
    /// <summary>
    /// Dto to gets the input values
    /// </summary>
    public class SendDataRequestDto : IRequest
    {
        /// <summary>
        /// Metric value
        /// </summary>
        public string? Metric { get; set; }
        /// <summary>
        /// Count value
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// TimeStamp
        /// </summary>
        public int epochTimestamp { get; set; } = 0;
        /// <summary>
        /// Uri
        /// </summary>
        public string? VisualiserSeriesUri { get; set; }  
        /// <summary>
        /// route value
        /// </summary>
        public string ? VisualiserApiKey { get; set; }

    }
}
