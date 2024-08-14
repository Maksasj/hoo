using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Abstractions;

namespace HooService.Repository.OneDrive
{
    public class SimpleAuthProvider : IAuthenticationProvider
    {
        private readonly string _accessToken;
        public SimpleAuthProvider(string accessToken)
        {
            this._accessToken = accessToken;
        }

        public async Task AuthenticateRequestAsync(RequestInformation request,
            Dictionary<string, object>? additionalAuthenticationContext = default,
            CancellationToken cancellationToken = default)
        {
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
        }
    }

}
