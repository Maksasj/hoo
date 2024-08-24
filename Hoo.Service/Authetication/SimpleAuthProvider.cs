using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Abstractions;

namespace Hoo.Service.Authetication
{
    public class SimpleAuthProvider : IAuthenticationProvider
    {
        private readonly string _accessToken;
        public SimpleAuthProvider(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task AuthenticateRequestAsync(RequestInformation request,
            Dictionary<string, object>? additionalAuthenticationContext = default,
            CancellationToken cancellationToken = default)
        {
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
        }
    }

}
