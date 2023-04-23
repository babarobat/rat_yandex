using System;
using System.Collections;
using UnityEngine;

namespace RatYandex.Runtime
{
    public abstract class ARequest<T>
    {
        private readonly ICoroutine _coroutine;
        private readonly Action _request;
        private readonly WaitUntil _waitResponse;

        private bool _hasResponse;
        private bool _isSuccess;
        private string _data;
        private string _error;
        private bool _hasRequest;

        protected ARequest(ICoroutine bridge)
        {
            _coroutine = bridge;
            _waitResponse = new(CheckResponse);
        }

        public void Send(Action<T> onSuccess, Action<string> onError)
        {
            _coroutine.StartCoroutine(SendInternal(onSuccess, onError));
        }

        protected abstract Action Request { get; }
        protected abstract Action<string> Response { get; set; }
        protected abstract Action<string> Error { get; set; }
        protected abstract T Convert(string data);
        private IEnumerator SendInternal(Action<T> onSuccess, Action<string> onError)
        {
            if (_hasRequest)
            {
                yield break; 
            }

            _hasRequest = true;
            _hasResponse = false;
            
            Response += OnSuccess;
            Error += OnError;
            
            Request.Invoke();

            yield return _waitResponse;
            
            if (_isSuccess)
            {
                var result = Convert(_data);
                Reset();
                onSuccess?.Invoke(result);
            }
            else
            {
                var message = _error;
                Reset();
                onError?.Invoke(message);
            }
        }

        private bool CheckResponse() => _hasResponse;

        private void OnSuccess(string data)
        {
            _hasResponse = true;
            _isSuccess = true;
            _data = data;
        }

        private void OnError(string message)
        {
            _hasResponse = true;
            _isSuccess = false;
            _error = message;
        }

        private void Reset()
        {
            Response -= OnSuccess;
            Error -= OnError;
            
            _hasResponse = default;
            _hasRequest = default;
            _isSuccess = default;
            _data = default;
            _error = default;
        }
    }
}