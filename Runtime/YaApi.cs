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
            var request = new PlayerDataSaveRequest(_bridge);
            
            await SendRequest(request, data);
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
            var request = new BuyConsumableRequest(_bridge);
            
            await SendRequest(request, id);
        }
        
        public async Task BuyNonConsumable(string id)
        {
            var request = new BuyNonConsumableRequest(_bridge);
            
            await SendRequest(request, id);
        }

        private async Task SendRequest<TResponse, TError>(ARequest<TResponse, TError> request) where TError : RequestError
        {
            _bridge.WebConsoleLog($"start {request.GetType()} on client");

            await _bridge.AwaitCoroutine(request.Send());

            if (request.Status != RequestStatus.Success)
            {
                _bridge.WebConsoleLog($"error {request.GetType()} on client {request.Error.Message}");
                
                throw new YaException($"{request.Error.Message}");
            }
            
            _bridge.WebConsoleLog($"success {request.GetType()} on client");
        }

        private async Task SendRequest<TPayLoad, TError>(ARequestWithPayloadEmptyResult<TPayLoad,TError> request, TPayLoad payLoad) where TError : RequestError
        {
            _bridge.WebConsoleLog($"start {request.GetType()} on client");

            await _bridge.AwaitCoroutine(request.Send(payLoad));

            if (request.Status != RequestStatus.Success)
            {
                _bridge.WebConsoleLog($"error {request.GetType()} on client {request.Error.Message}");
                
                throw new YaException($"{request.Error.Message}");
            }
            
            _bridge.WebConsoleLog($"success {request.GetType()} on client");
        }
    }

    public class YaException : Exception
    {
        public YaException(string message) : base(message)
        {
            
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