using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class ReviewDialogRequest : ARequest<bool>
    {
        private readonly YaApiBridge _bridge;

        public ReviewDialogRequest(YaApiBridge bridge) : base(bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.DialogReviewOpen;
        protected override Action<string> Response
        {
            get => _bridge.OnDialogReviewClosed;
            set => _bridge.OnDialogReviewClosed = value;
        }

        protected override Action<string> Error {
            get => _bridge.OnDialogReviewError;
            set => _bridge.OnDialogReviewError = value;
        }
        protected override bool Convert(string data) => JsonConvert.DeserializeObject<bool>(data);
    }
}