using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RatYandex.Runtime
{
    public class YaApi
    {
        public event Action OnPlayerInfoChanged;

        private readonly YaApiBridge _bridge;

        public YaApi()
        {
            var container = new GameObject("ya_api");
            _bridge = container.AddComponent<YaApiBridge>();
            Object.DontDestroyOnLoad(_bridge);

            _bridge.OnPlayerInfoReceived = UpdatePlayerInfo;
        }

        public void WebWindowAlert(string message) => _bridge.WebWindowAlert(message);
        public void WebConsoleLog(string message) => _bridge.WebConsoleLog(message);
        public void GetPlayerInfo() => _bridge.GetPlayerInfo();
        public PlayerInfo PlayerInfo { get; private set; }

        [UsedImplicitly]
        public void UpdatePlayerInfo(string data)
        {
            PlayerInfo = JsonConvert.DeserializeObject<PlayerInfo>(data);
            OnPlayerInfoChanged?.Invoke();
        }
    }
}