using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class AuthenticationRequest : ARequest<AuthenticationResponse, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public AuthenticationRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.AuthDialogOpen;
        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnAuthenticationSuccess;
            set => _bridge.OnAuthenticationSuccess = value;
        }

        protected override Action<string> ErrorProvider {
            get => _bridge.OnAuthenticationError;
            set => _bridge.OnAuthenticationError = value;
        }

        protected override AuthenticationResponse ParseResult(string data) => JsonConvert.DeserializeObject<AuthenticationResponse>(data);
        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}