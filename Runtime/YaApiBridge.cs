using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

namespace RatYandex.Runtime
{
    public class YaApiBridge : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void _WebWindowAlert(string message);

        [DllImport("__Internal")]
        private static extern void _WebConsoleLog(string message);

        [DllImport("__Internal")]
        private static extern void _GetPlayerInfo();

        public Action<string> OnPlayerInfoReceived;
        public void WebWindowAlert(string message) => _WebWindowAlert(message);
        public void WebConsoleLog(string message) => _WebConsoleLog(message);
        public void GetPlayerInfo() => _GetPlayerInfo();

        [UsedImplicitly]
        public void UpdatePlayerInfo(string data) => OnPlayerInfoReceived?.Invoke(data);
    }
}