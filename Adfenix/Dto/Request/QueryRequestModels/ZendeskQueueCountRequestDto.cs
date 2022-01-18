using MediatR;

namespace Adfenix.RequestModels.QueryRequestModels
{
    /// <summary>
    /// Dto to gets the input values
    /// </summary>
    public class ZendeskQueueCountRequestDto : IRequest<string>
    {
        /// <summary>
        /// Zendesk Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Zendesk Authentication Token
        /// </summary>
        public string Token { get; set; }
    }
}
