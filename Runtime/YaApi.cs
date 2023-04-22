using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RatYandex.Runtime
{
    public class YaApi
    {
        private readonly YaApiBridge _bridge;
        private readonly PlayerInfoRequestProvider _playerInfoRequestProvider;
        private readonly ReviewInfoRequestProvider _reviewInfoRequestProvider;
        private readonly ReviewDialogRequestProvider _reviewDialogRequestProvider;

        public YaApi()
        {
            var container = new GameObject("ya_api");
            _bridge = container.AddComponent<YaApiBridge>();
            Object.DontDestroyOnLoad(_bridge);

            _playerInfoRequestProvider = new(_bridge);
            _reviewInfoRequestProvider = new(_bridge);
            _reviewDialogRequestProvider = new(_bridge);
        }

        public void WebWindowAlert(string message) => _bridge.WebWindowAlert(message);
        public void WebConsoleLog(string message) => _bridge.WebConsoleLog(message);
        public void DialogReviewOpen(Action<bool> onClose, Action onError) => _reviewDialogRequestProvider.OpenReviewDialog(onClose, onError);
        public void RequestPlayerInfo(Action<PlayerInfo> onSuccess, Action onError) => _playerInfoRequestProvider.Get(onSuccess, onError);
        public void RequestReviewInfo(Action<ReviewInfo> onSuccess, Action onError) => _reviewInfoRequestProvider.Get(onSuccess, onError);
    }
}