using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class ReviewDialogRequest : ARequest<ReviewDialogResponse, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public ReviewDialogRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.ReviewDialogOpen;
        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnDialogReviewClosed;
            set => _bridge.OnDialogReviewClosed = value;
        }

        protected override Action<string> ErrorProvider {
            get => _bridge.OnDialogReviewError;
            set => _bridge.OnDialogReviewError = value;
        }
        protected override ReviewDialogResponse ParseResult(string data) => JsonConvert.DeserializeObject<ReviewDialogResponse>(data);
        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}