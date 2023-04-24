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
    public enum RequestStatus
    {
        None, InProgress, Success, Error, Canceled
    }
    public abstract class ARequest<TResult, TError>
    {
        public RequestStatus Status { get; private set; }
        public TResult Result { get; private set; }
        public TError Error { get; private set; }
        
        private readonly Action _request;
        private readonly WaitUntil _waitResponse;
        
        protected ARequest()
        {
            _waitResponse = new(IsCompleted);
        }

        public IEnumerator Send()
        {
            if (Status == RequestStatus.InProgress)
            {
                yield break; 
            }
            
            Status = RequestStatus.InProgress;

            ResponseProvider += OnSuccess;
            ErrorProvider += OnError;
            
            Request.Invoke();

            yield return _waitResponse;
            
            ResponseProvider -= OnSuccess;
            ErrorProvider -= OnError;
        }

        public void Cancel()
        {
            Status = RequestStatus.Canceled;
            
            ResponseProvider -= OnSuccess;
            ErrorProvider -= OnError;
        }

        protected abstract Action Request { get; }
        protected abstract Action<string> ResponseProvider { get; set; }
        protected abstract Action<string> ErrorProvider { get; set; }
        protected abstract TResult ParseResult(string data);
        protected abstract TError ParseError(string data);

        private bool IsCompleted() => Status is RequestStatus.Canceled or RequestStatus.Success or RequestStatus.Error;

        private void OnSuccess(string data)
        {
            Result = ParseResult(data);
            Status = RequestStatus.Success;
        }

        private void OnError(string data)
        {
            Error = ParseError(data);
            Status = RequestStatus.Error;
        }
    }
}