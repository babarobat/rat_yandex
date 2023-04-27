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
        [DllImport("__Internal")] private static extern void _Initialize();
        [DllImport("__Internal")] private static extern void _ReviewDialogOpen();
        [DllImport("__Internal")] private static extern void _AuthDialogOpen();
        [DllImport("__Internal")] private static extern void _GetPlayerInfo();
        [DllImport("__Internal")] private static extern void _GetReviewInfo();
        [DllImport("__Internal")] private static extern void _GetPlayerData();
        [DllImport("__Internal")] private static extern void _SetPlayerData(string data);

        public Action<string> OnInitializationSuccess;
        public Action<string> OnInitializationError;
        public Action<string> OnAuthenticationSuccess;
        public Action<string> OnAuthenticationError;
        public Action<string> OnPlayerInfoReceived;
        public Action<string> OnPlayerInfoError;
        public Action<string> OnReviewInfoReceived;
        public Action<string> OnReviewInfoError;
        public Action<string> OnDialogReviewClosed;
        public Action<string> OnDialogReviewError;
        public Action<string> OnPlayerDataReceived;
        public Action<string> OnPlayerDataError;
        public Action OnPlayerDataSaved;
        public Action<string> OnPlayerDataSaveError;
        public void WebWindowAlert(string message) => _WebWindowAlert(message);
        public void WebConsoleLog(string message) => _WebConsoleLog(message);
        public void Initialize() => _Initialize();
        public void ReviewDialogOpen() => _ReviewDialogOpen();
        public void AuthDialogOpen() => _AuthDialogOpen();
        public void GetPlayerInfo() => _GetPlayerInfo();
        public void GetReviewInfo() => _GetReviewInfo();
        public void GetPlayerData() => _GetPlayerData();
        public void SetPlayerData(string data) => _SetPlayerData(data);

        [UsedImplicitly] public void InitializationSuccess(string data) => OnInitializationSuccess?.Invoke(data);
        [UsedImplicitly] public void InitializationError(string data) => OnInitializationError?.Invoke(data);
        [UsedImplicitly] public void AuthenticationSuccess(string data) => OnAuthenticationSuccess?.Invoke(data);
        [UsedImplicitly] public void AuthenticationError(string data) => OnAuthenticationError?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerInfo(string data) => OnPlayerInfoReceived?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerInfoError(string data) => OnPlayerInfoError?.Invoke(data);
        [UsedImplicitly] public void UpdateReviewInfo(string data) => OnReviewInfoReceived?.Invoke(data);
        [UsedImplicitly] public void UpdateReviewInfoError(string data) => OnReviewInfoError?.Invoke(data);
        [UsedImplicitly] public void DialogReviewClosed(string data) => OnDialogReviewClosed?.Invoke(data);
        [UsedImplicitly] public void DialogReviewError(string data) => OnDialogReviewError?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerData(string data) => OnPlayerDataReceived?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerDataError(string data) => OnPlayerDataError?.Invoke(data);
        [UsedImplicitly] public void SavePlayerDataSuccess() => OnPlayerDataSaved?.Invoke();
        [UsedImplicitly] public void SavePlayerDataError(string data) => OnPlayerDataSaveError?.Invoke(data);
    }
}