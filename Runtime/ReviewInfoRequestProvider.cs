using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace RatYandex.Runtime
{
    public class ReviewInfoRequestProvider
    {
        private readonly YaApiBridge _bridge;
        private readonly WaitUntil _waitResponse;
        
        private bool _hasResponse;
        private bool _isSuccess;
        private string _data;

        public ReviewInfoRequestProvider(YaApiBridge bridge)
        {
            _bridge = bridge;

            _bridge.OnReviewInfoReceived = OnSuccess;
            _bridge.OnReviewInfoError = OnError;
            
            _waitResponse = new(() => _hasResponse);
        }

        public void Get(Action<ReviewInfo> onSuccess, Action onError)
        {
            _bridge.StartCoroutine(GetInternal(onSuccess, onError));
        }

        private IEnumerator GetInternal(Action<ReviewInfo> onSuccess, Action onError)
        {
            _bridge.GetReviewInfo();
            
            yield return _waitResponse;
            
            if (_isSuccess)
            {
                var result = JsonConvert.DeserializeObject<ReviewInfo>(_data);
                Clear();
                onSuccess?.Invoke(result);
            }
            else
            {
                Clear();
                onError?.Invoke();
            }
        }

        private void OnSuccess(string data)
        {
            _hasResponse = true;
            _isSuccess = true;
            _data = data;
        }

        private void OnError()
        {
            _hasResponse = true;
            _isSuccess = false;
        }

        private void Clear()
        {
            _hasResponse = default;
            _isSuccess = default;
            _data = default;
        }
    }
}