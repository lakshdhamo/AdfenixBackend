using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.RequestModels.QueryRequestModels
{
    /// <summary>
    /// Dto to gets the input values
    /// </summary>
    public class ServerCountRequestDto : IRequest<string>
    {
        /// <summary>
        /// Server Id
        /// </summary>
        public int ServerId { get; set; }

    }
}
