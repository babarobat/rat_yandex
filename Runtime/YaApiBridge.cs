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
        [DllImport("__Internal")] private static extern void _AuthDialogOpen();
        [DllImport("__Internal")] private static extern void _GetPlayerInfo();
        [DllImport("__Internal")] private static extern void _GetPlayerData();
        [DllImport("__Internal")] private static extern void _SetPlayerData(string data);
        [DllImport("__Internal")] private static extern void _BuyConsumable(string id);
        [DllImport("__Internal")] private static extern void _BuyNonConsumable(string id);
        [DllImport("__Internal")] private static extern void _ResetNonConsumable(string id);
        [DllImport("__Internal")] private static extern void _ShowInterstitial();
        [DllImport("__Internal")] private static extern void _ShowRewarded();
        [DllImport("__Internal")] private static extern void _InitializePayments();
        [DllImport("__Internal")] private static extern void _GetPurchases();
        [DllImport("__Internal")] private static extern void _ShowReview();
        [DllImport("__Internal")] private static extern void _CanReview();

        public Action<string> OnInitializationSuccess;
        public Action<string> OnInitializationError;
        public Action<string> OnAuthenticationSuccess;
        public Action<string> OnAuthenticationError;
        public Action<string> OnPlayerInfoReceived;
        public Action<string> OnPlayerInfoError;
        public Action<string> OnPlayerDataReceived;
        public Action<string> OnPlayerDataError;
        public Action OnPlayerDataSaved;
        public Action<string> OnPlayerDataSaveError;
        public Action<string> OnAdsInterstitialSuccess;
        public Action<string> OnAdsInterstitialShowError;
        public Action<string> OnRewardedShowSuccess;
        public Action<string> OnRewardedShowError;
        public Action<string> OnInitializePaymentsSuccess;
        public Action<string> OnInitializePaymentsError;
        public Action OnBuyConsumableSuccess;
        public Action<string> OnBuyConsumableError;
        public Action OnBuyNonConsumableSuccess;
        public Action<string> OnBuyNonConsumableError;
        public Action<string> OnGetPurchasesSuccess;
        public Action<string> OnGetPurchasesError;
        public Action OnResetNonConsumableSuccess;
        public Action<string> OnResetNonConsumableError;
        public Action<string> OnShowReviewSuccess;
        public Action<string> OnShowReviewError;
        public Action<string> OnCanReviewSuccess;
        public Action<string> OnCanReviewError;
        
        public void WebWindowAlert(string message) => _WebWindowAlert(message);
        public void WebConsoleLog(string message) => _WebConsoleLog(message);
        public void Initialize() => _Initialize();
        public void AuthDialogOpen() => _AuthDialogOpen();
        public void GetPlayerInfo() => _GetPlayerInfo();
        public void GetPlayerData() => _GetPlayerData();
        public void SetPlayerData(string data) => _SetPlayerData(data);
        public void BuyConsumable(string id) => _BuyConsumable(id);
        public void BuyNonConsumable(string id) => _BuyNonConsumable(id);
        public void ResetNonConsumable(string id) => _ResetNonConsumable(id);
        public void ShowInterstitial() => _ShowInterstitial();
        public void ShowRewarded() => _ShowRewarded();
        public void InitializePayments() => _InitializePayments();
        public void GetPurchases() => _GetPurchases();
        public void ShowReview() => _ShowReview();
        public void CanReview() => _CanReview();

        [UsedImplicitly] public void InitializationSuccess(string data) => OnInitializationSuccess?.Invoke(data);
        [UsedImplicitly] public void InitializationError(string data) => OnInitializationError?.Invoke(data);
        [UsedImplicitly] public void AuthenticationSuccess(string data) => OnAuthenticationSuccess?.Invoke(data);
        [UsedImplicitly] public void AuthenticationError(string data) => OnAuthenticationError?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerInfo(string data) => OnPlayerInfoReceived?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerInfoError(string data) => OnPlayerInfoError?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerData(string data) => OnPlayerDataReceived?.Invoke(data);
        [UsedImplicitly] public void UpdatePlayerDataError(string data) => OnPlayerDataError?.Invoke(data);
        [UsedImplicitly] public void SavePlayerDataSuccess() => OnPlayerDataSaved?.Invoke();
        [UsedImplicitly] public void SavePlayerDataError(string data) => OnPlayerDataSaveError?.Invoke(data);
        [UsedImplicitly] public void InterstitialShowSuccess(string data) => OnAdsInterstitialSuccess?.Invoke(data);
        [UsedImplicitly] public void InterstitialShowStartError(string data) => OnAdsInterstitialShowError?.Invoke(data);
        [UsedImplicitly] public void RewardedShowSuccess(string data) => OnRewardedShowSuccess?.Invoke(data);
        [UsedImplicitly] public void RewardedShowError(string data) => OnRewardedShowError?.Invoke(data);
        [UsedImplicitly] public void InitializePaymentsSuccess(string data) => OnInitializePaymentsSuccess?.Invoke(data);
        [UsedImplicitly] public void InitializePaymentsError(string data) => OnInitializePaymentsError?.Invoke(data);
        [UsedImplicitly] public void BuyConsumableSuccess() => OnBuyConsumableSuccess?.Invoke();
        [UsedImplicitly] public void BuyConsumableError(string data) => OnBuyConsumableError?.Invoke(data);
        [UsedImplicitly] public void BuyNonConsumableSuccess() => OnBuyNonConsumableSuccess?.Invoke();
        [UsedImplicitly] public void BuyNonConsumableError(string data) => OnBuyNonConsumableError?.Invoke(data);
        [UsedImplicitly] public void ResetNonConsumableSuccess() => OnResetNonConsumableSuccess?.Invoke();
        [UsedImplicitly] public void ResetNonConsumableError(string data) => OnResetNonConsumableError?.Invoke(data);
        [UsedImplicitly] public void GetPurchasesSuccess(string data) => OnGetPurchasesSuccess?.Invoke(data);
        [UsedImplicitly] public void GetPurchasesError(string data) => OnGetPurchasesError?.Invoke(data);
        [UsedImplicitly] public void ShowReviewSuccess(string data) => OnShowReviewSuccess?.Invoke(data);
        [UsedImplicitly] public void ShowReviewError(string data) => OnShowReviewError?.Invoke(data);
        [UsedImplicitly] public void CanReviewSuccess(string data) => OnCanReviewSuccess?.Invoke(data);
        [UsedImplicitly] public void CanReviewError(string data) => OnCanReviewError?.Invoke(data);
    }
}