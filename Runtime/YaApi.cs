using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RatYandex.Runtime
{
    public class YaApi
    {
        private readonly YaApiBridge _bridge;

        public YaApi()
        {
            var container = new GameObject("ya_api");
            _bridge = container.AddComponent<YaApiBridge>();
            Object.DontDestroyOnLoad(_bridge);
        }

        public void WebWindowAlert(string message) => _bridge.WebWindowAlert(message);
        public void WebConsoleLog(string message) => _bridge.WebConsoleLog(message);

        public async Task<InitializationResponse> Initialize()
        {
            var request = new InitializeRequest(_bridge);
            
            await SendRequest(request);

            return request.Result;
        }

        public async Task<AuthenticationResponse> Authenticate()
        {
            var request = new AuthenticationRequest(_bridge);
            
            await SendRequest(request);

            return request.Result;
        }

        public async Task<InitializePaymentsResult> InitializePayments()
        {
            var request = new InitializePaymentsRequest(_bridge);
            
            await SendRequest(request);

            return request.Result;
        }

        public async Task<PlayerDataLoadResponse> LoadPlayerData()
        {
            var request = new PlayerDataLoadRequest(_bridge);
            
            await SendRequest(request);

            return request.Result;
        }

        public async Task SavePlayerData(string data)
        {
            var request = new PlayerDataSaveRequest(_bridge, data);
            
            await SendRequest(request);
        }

        public async Task<InterstitialShowResult> ShowInterstitial()
        {
            var request = new InterstitialShowRequest(_bridge);
            
            await SendRequest(request);

            return request.Result;
        }

        public async Task<RewardedShowResult> ShowRewarded()
        {
            var request = new RewardedShowRequest(_bridge);
            
            await SendRequest(request);

            return request.Result;
        }

        public async Task BuyConsumable(string id)
        {
            var request = new BuyConsumableRequest(_bridge, id);
            
            await SendRequest(request);
        }
        
        public async Task BuyNonConsumable(string id)
        {
            var request = new BuyNonConsumableRequest(_bridge, id);
            
            await SendRequest(request);
        }
        
        public async Task ResetNonConsumable(string id)
        {
            var request = new ResetNonConsumableRequest(_bridge, id);
            
            await SendRequest(request);
        }
        
        public async Task<Purchase[]> GetPurchases()
        {
            var request = new GetPurchasesRequest(_bridge);
            
            await SendRequest(request);

            return request.Result.Purchases;
        }

        public async Task<bool> ShowReview()
        {
            var request = new ReviewShowRequest(_bridge);
            
            await SendRequest(request);

            return request.Result.IsSent;
        }
        
        public async Task<CanReviewResult> CanReview()
        {
            var request = new CanReviewRequest(_bridge);
            
            await SendRequest(request);

            return request.Result;
        }

        private async Task SendRequest(ARequest request) 
        {
            _bridge.WebConsoleLog($"start {request.GetType()} on client");
        
            await _bridge.AwaitCoroutine(request.Send());
        
            if (request.Status != RequestStatus.Success)
            {
                _bridge.WebConsoleLog($"error {request.GetType()} on client {request.Error.Message}");
                
                throw new YaRequestException(request);
            }
            
            _bridge.WebConsoleLog($"success {request.GetType()} on client");
        }
    }

    internal static class CoroutineExtensions
    {
        public static Task AwaitCoroutine(this MonoBehaviour monoBehaviour, IEnumerator coroutine)
        {
            var tcs = new TaskCompletionSource<bool>();
            monoBehaviour.StartCoroutine(RunCoroutine(coroutine, tcs));
            return tcs.Task;
        }

        private static IEnumerator RunCoroutine(IEnumerator coroutine, TaskCompletionSource<bool> tcs)
        {
            yield return coroutine;
            tcs.SetResult(true);
        }
    }
}