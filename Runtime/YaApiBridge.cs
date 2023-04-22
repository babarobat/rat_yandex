using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

namespace RatYandex.Runtime
{
    public class YaApiBridge : MonoBehaviour
    {
        [DllImport("__Internal")] private static extern void _WebWindowAlert(string message);
        [DllImport("__Internal")] private static extern void _WebConsoleLog(string message);
        [DllImport("__Internal")] private static extern void _DialogReviewOpen();
        [DllImport("__Internal")] private static extern void _GetPlayerInfo();
        [DllImport("__Internal")] private static extern void _GetReviewInfo();

        public Action<string> OnPlayerInfoReceived;
        public Action OnPlayerInfoError;
        public Action<string> OnReviewInfoReceived;
        public Action OnReviewInfoError;
        public Action<string> OnDialogReviewClosed;
        public Action OnDialogReviewError;
        public void WebWindowAlert(string message) => _WebWindowAlert(message);
        public void WebConsoleLog(string message) => _WebConsoleLog(message);
        public void DialogReviewOpen() => _DialogReviewOpen();
        public void GetPlayerInfo() => _GetPlayerInfo();
        public void GetReviewInfo() => _GetReviewInfo();

        [UsedImplicitly] public void UpdatePlayerInfo(string data) => OnPlayerInfoReceived?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerInfoError() => OnPlayerInfoError?.Invoke();
        [UsedImplicitly] public void UpdateReviewInfo(string data) => OnReviewInfoReceived?.Invoke(data);
        [UsedImplicitly] public void UpdateReviewInfoError() => OnReviewInfoError?.Invoke();
        [UsedImplicitly] public void DialogReviewClosed(string reviewSend) => OnDialogReviewClosed?.Invoke(reviewSend);
        [UsedImplicitly] public void DialogReviewError() => OnDialogReviewError?.Invoke();

        
    }
}