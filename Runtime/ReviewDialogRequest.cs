using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class ReviewDialogRequest : ARequest<ReviewDialogResponse>
    {
        private readonly YaApiBridge _bridge;

        public ReviewDialogRequest(YaApiBridge bridge) : base(bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.ReviewDialogOpen;
        protected override Action<string> Response
        {
            get => _bridge.OnDialogReviewClosed;
            set => _bridge.OnDialogReviewClosed = value;
        }

        protected override Action<string> Error {
            get => _bridge.OnDialogReviewError;
            set => _bridge.OnDialogReviewError = value;
        }
        protected override ReviewDialogResponse Convert(string data) => JsonConvert.DeserializeObject<ReviewDialogResponse>(data);
    }
}