using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RatYandex.Runtime
{
    public class YaApi
    {
        private readonly YaApiBridge _bridge;
        private readonly PlayerInfoRequest _playerInfoRequest;
        private readonly ReviewInfoRequest _reviewInfoRequest;
        private readonly ReviewDialogRequest _reviewDialogRequest;
        private readonly PlayerDataLoadRequest _playerDataLoadRequest;

        public YaApi()
        {
            var container = new GameObject("ya_api");
            _bridge = container.AddComponent<YaApiBridge>();
            Object.DontDestroyOnLoad(_bridge);

            _playerInfoRequest = new(_bridge);
            _reviewInfoRequest = new(_bridge);
            _reviewDialogRequest = new(_bridge);
            _playerDataLoadRequest = new(_bridge);
        }

        public void WebWindowAlert(string message) => _bridge.WebWindowAlert(message);
        public void WebConsoleLog(string message) => _bridge.WebConsoleLog(message);
        public void DialogReviewOpen(Action<bool> onClose, Action<string> onError) => _reviewDialogRequest.Send(onClose, onError);
        public void RequestPlayerInfo(Action<PlayerInfo> onSuccess, Action<string> onError) => _playerInfoRequest.Send(onSuccess, onError);
        public void RequestReviewInfo(Action<ReviewInfo> onSuccess, Action<string> onError) => _reviewInfoRequest.Send(onSuccess, onError);
        public void RequestPlayerDataLoad(Action<string> onSuccess, Action<string> onError) => _playerDataLoadRequest.Send(onSuccess, onError);
    }
}