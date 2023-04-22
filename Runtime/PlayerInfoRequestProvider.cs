using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace RatYandex.Runtime
{
    public class PlayerInfoRequestProvider
    {
        private readonly YaApiBridge _bridge;
        private readonly WaitUntil _waitResponse;
        
        private bool _hasResponse;
        private bool _isSuccess;
        private string _data;

        public PlayerInfoRequestProvider(YaApiBridge bridge)
        {
            _bridge = bridge;

            _bridge.OnPlayerInfoReceived = OnSuccess;
            _bridge.OnPlayerInfoError = OnError;
            
            _waitResponse = new(() => _hasResponse);
        }

        public void Get(Action<PlayerInfo> onSuccess, Action onError)
        {
            _bridge.StartCoroutine(GetInternal(onSuccess, onError));
        }

        private IEnumerator GetInternal(Action<PlayerInfo> onSuccess, Action onError)
        {
            _bridge.GetPlayerInfo();
            
            yield return _waitResponse;
            
            if (_isSuccess)
            {
                var result = JsonConvert.DeserializeObject<PlayerInfo>(_data);
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