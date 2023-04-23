using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RatYandex.Runtime
{
    public class YaApi
    {
        private readonly YaApiBridge _bridge;
        
        private readonly InitializeRequest _initializeRequest;
        private readonly AuthenticationRequest _authenticationRequest;
        private readonly PlayerInfoRequest _playerInfoRequest;
        private readonly ReviewInfoRequest _reviewInfoRequest;
        private readonly ReviewDialogRequest _reviewDialogRequest;
        private readonly PlayerDataLoadRequest _playerDataLoadRequest;

        public YaApi()
        {
            var container = new GameObject("ya_api");
            _bridge = container.AddComponent<YaApiBridge>();
            Object.DontDestroyOnLoad(_bridge);

            _initializeRequest = new(_bridge);
            _authenticationRequest = new(_bridge);
            _playerInfoRequest = new(_bridge);
            _reviewInfoRequest = new(_bridge);
            _reviewDialogRequest = new(_bridge);
            _playerDataLoadRequest = new(_bridge);
        }

        public void WebWindowAlert(string message) => _bridge.WebWindowAlert(message);
        public void WebConsoleLog(string message) => _bridge.WebConsoleLog(message);

        public void Initialize(Action<InitializationResponse> onSuccess, Action<string> onError) =>
            _initializeRequest.Send(onSuccess, onError);

        public void AuthenticationDialogOpen(Action<AuthenticationResponse> onSuccess, Action<string> onError) =>
            _authenticationRequest.Send(onSuccess, onError);

        public void DialogReviewOpen(Action<ReviewDialogResponse> onClose, Action<string> onError) =>
            _reviewDialogRequest.Send(onClose, onError);

        public void RequestPlayerInfo(Action<PlayerInfoResponse> onSuccess, Action<string> onError) =>
            _playerInfoRequest.Send(onSuccess, onError);

        public void RequestReviewInfo(Action<ReviewInfoResponse> onSuccess, Action<string> onError) =>
            _reviewInfoRequest.Send(onSuccess, onError);

        public void RequestPlayerData(Action<PlayerDataLoadResponse> onSuccess, Action<string> onError) =>
            _playerDataLoadRequest.Send(onSuccess, onError);
    }
}