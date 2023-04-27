using System;
using System.Collections;
using UnityEngine;

namespace RatYandex.Runtime
{
    public enum RequestStatus
    {
        None, InProgress, Success, Error, Canceled
    }
    
    public abstract class ARequest<TResult, TError>
    {
        public RequestStatus Status { get; private set; }
        public TResult Result { get; private set; }
        public TError Error { get; private set; }
        
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
    
    public abstract class ARequestWithPayloadEmptyResult<TPayload, TError>
    {
        public RequestStatus Status { get; private set; }
        public TError Error { get; private set; }
        
        private readonly WaitUntil _waitResponse;
        
        protected ARequestWithPayloadEmptyResult()
        {
            _waitResponse = new(IsCompleted);
        }

        public IEnumerator Send(TPayload payload)
        {
            if (Status == RequestStatus.InProgress)
            {
                yield break; 
            }
            
            Status = RequestStatus.InProgress;

            ResponseProvider += OnSuccess;
            ErrorProvider += OnError;
            
            Request.Invoke(payload);

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

        protected abstract Action<TPayload> Request { get; }
        protected abstract Action ResponseProvider { get; set; }
        protected abstract Action<string> ErrorProvider { get; set; }
        protected abstract TError ParseError(string data);

        private bool IsCompleted() => Status is RequestStatus.Canceled or RequestStatus.Success or RequestStatus.Error;

        private void OnSuccess()
        {
            Status = RequestStatus.Success;
        }

        private void OnError(string data)
        {
            Error = ParseError(data);
            Status = RequestStatus.Error;
        }
    }
}