using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class AuthenticationRequest : ARequest<AuthenticationResponse>
    {
        private readonly YaApiBridge _bridge;

        public AuthenticationRequest(YaApiBridge bridge) : base(bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.AuthDialogOpen;
        protected override Action<string> Response
        {
            get => _bridge.OnAuthenticationSuccess;
            set => _bridge.OnAuthenticationSuccess = value;
        }

        protected override Action<string> Error {
            get => _bridge.OnAuthenticationError;
            set => _bridge.OnAuthenticationError = value;
        }

        protected override AuthenticationResponse Convert(string data) => JsonConvert.DeserializeObject<AuthenticationResponse>(data);
    }
}