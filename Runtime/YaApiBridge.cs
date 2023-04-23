using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

namespace RatYandex.Runtime
{
    public class YaApiBridge : MonoBehaviour, ICoroutine
    {
        [DllImport("__Internal")] private static extern void _WebWindowAlert(string message);
        [DllImport("__Internal")] private static extern void _WebConsoleLog(string message);
        [DllImport("__Internal")] private static extern void _DialogReviewOpen();
        [DllImport("__Internal")] private static extern void _GetPlayerInfo();
        [DllImport("__Internal")] private static extern void _GetReviewInfo();
        [DllImport("__Internal")] private static extern void _GetPlayerData();

        public Action<string> OnPlayerInfoReceived;
        public Action<string> OnPlayerInfoError;
        public Action<string> OnReviewInfoReceived;
        public Action<string> OnReviewInfoError;
        public Action<string> OnDialogReviewClosed;
        public Action<string> OnDialogReviewError;
        public Action<string> OnPlayerDataReceived;
        public Action<string> OnPlayerDataError;
        public void WebWindowAlert(string message) => _WebWindowAlert(message);
        public void WebConsoleLog(string message) => _WebConsoleLog(message);
        public void DialogReviewOpen() => _DialogReviewOpen();
        public void GetPlayerInfo() => _GetPlayerInfo();
        public void GetReviewInfo() => _GetReviewInfo();
        public void GetPlayerData() => _GetPlayerData();

        [UsedImplicitly] public void UpdatePlayerInfo(string data) => OnPlayerInfoReceived?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerInfoError(string data) => OnPlayerInfoError?.Invoke(data);
        [UsedImplicitly] public void UpdateReviewInfo(string data) => OnReviewInfoReceived?.Invoke(data);
        [UsedImplicitly] public void UpdateReviewInfoError(string data) => OnReviewInfoError?.Invoke(data);
        [UsedImplicitly] public void DialogReviewClosed(string data) => OnDialogReviewClosed?.Invoke(data);
        [UsedImplicitly] public void DialogReviewError(string data) => OnDialogReviewError?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerData(string data) => OnPlayerDataReceived?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerDataError(string data) => OnPlayerDataError?.Invoke(data);
    }
}