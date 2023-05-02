using UnityEngine;
using Object = UnityEngine.Object;

namespace RatYandex.Runtime
{
    public class YaApi
    {
        private readonly YaApiBridge _bridge;
        
        public InitializeRequest InitializeRequest { get; }
        public AuthenticationRequest AuthenticationRequest { get; }
        public PlayerInfoRequest PlayerInfoRequest { get; }
        public ReviewInfoRequest ReviewInfoRequest { get; }
        public ReviewDialogRequest ReviewDialogRequest { get; }
        public PlayerDataLoadRequest PlayerDataLoadRequest { get; }
        public PlayerDataSaveRequest PlayerDataSaveRequest { get; }
        public InterstitialShowRequest InterstitialShowRequest { get; }
        public RewardedShowRequest RewardedShowRequest { get; }
        public InitializePaymentsRequest InitializePaymentsRequest { get; }

        public YaApi()
        {
            var container = new GameObject("ya_api");
            _bridge = container.AddComponent<YaApiBridge>();
            Object.DontDestroyOnLoad(_bridge);

            InitializeRequest = new(_bridge);
            AuthenticationRequest = new(_bridge);
            PlayerInfoRequest = new(_bridge);
            ReviewInfoRequest = new(_bridge);
            ReviewDialogRequest = new(_bridge);
            PlayerDataLoadRequest = new(_bridge);
            PlayerDataSaveRequest = new(_bridge);
            InterstitialShowRequest = new(_bridge);
            RewardedShowRequest = new(_bridge);
            InitializePaymentsRequest = new(_bridge);
        }

        public void WebWindowAlert(string message) => _bridge.WebWindowAlert(message);
        public void WebConsoleLog(string message) => _bridge.WebConsoleLog(message);
    }
}