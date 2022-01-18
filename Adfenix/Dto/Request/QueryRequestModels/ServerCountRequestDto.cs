using MediatR;

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
