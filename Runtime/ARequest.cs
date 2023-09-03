using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace RatYandex.Runtime
{
    public enum RequestStatus
    {
        None, InProgress, Success, Error, Canceled
    }

    internal abstract class ARequest 
    {
        public RequestStatus Status { get; protected set; }
        public RequestError Error { get; protected set; }
        
        protected readonly WaitUntil WaitResponse;
        public bool IsCompleted() => Status is RequestStatus.Canceled or RequestStatus.Success or RequestStatus.Error;

        protected ARequest()
        {
            WaitResponse = new(IsCompleted);
        }

        public abstract IEnumerator Send();

        public abstract void Cancel();

        protected abstract Action<string> ErrorProvider { get; set; }
        protected abstract RequestError ParseError(string data);
    }

    internal abstract class ARequest<TResult>  : ARequest
    {
        public TResult Result { get; private set; }

        public override IEnumerator Send()
        {
            if (Status == RequestStatus.InProgress)
            {
                yield break; 
            }
            
            Status = RequestStatus.InProgress;

            ResponseProvider += OnSuccess;
            ErrorProvider += OnError;
            
            Request.Invoke();

            yield return WaitResponse;
            
            ResponseProvider -= OnSuccess;
            ErrorProvider -= OnError;
        }

        public override void Cancel()
        {
            Status = RequestStatus.Canceled;
            
            ResponseProvider -= OnSuccess;
            ErrorProvider -= OnError;
        }

        protected abstract Action Request { get; }
        protected abstract Action<string> ResponseProvider { get; set; }
        protected abstract TResult ParseResult(string data);
        
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
    
    internal abstract class ARequestWithPayloadEmptyResult<TPayload> : ARequest
    {
        public TPayload Payload { get; }
        
        protected ARequestWithPayloadEmptyResult(TPayload payload)
        {
            Payload = payload;
        }

        public override IEnumerator Send()
        {
            if (Status == RequestStatus.InProgress)
            {
                yield break; 
            }
            
            Status = RequestStatus.InProgress;

            ResponseProvider += OnSuccess;
            ErrorProvider += OnError;
            
            Request.Invoke(Payload);

            yield return WaitResponse;
            
            ResponseProvider -= OnSuccess;
            ErrorProvider -= OnError;
        }

        public override void Cancel()
        {
            Status = RequestStatus.Canceled;
            
            ResponseProvider -= OnSuccess;
            ErrorProvider -= OnError;
        }

        protected abstract Action<TPayload> Request { get; }
        protected abstract Action ResponseProvider { get; set; }
        
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
    
    public class YaRequestException : Exception
    {
        public override string Message { get; }

        internal YaRequestException(ARequest request)
        {
            var args = GetRequestArgs(request);

            Message = $"Exception while making Ya Api request\n{JsonConvert.SerializeObject(args)}";
        }

        private IDictionary GetRequestArgs(ARequest request)
        {
            return request switch
            {
                AuthenticationRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                BuyConsumableRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                    {"id", r.Payload},
                },
                BuyNonConsumableRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                    {"id", r.Payload},
                },
                CanReviewRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                GetPurchasesRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                InitializePaymentsRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                InitializeRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                InterstitialShowRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                PlayerDataLoadRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                PlayerDataSaveRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                    {"data", r.Payload},
                },
                PlayerInfoRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                ResetNonConsumableRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                    {"id", r.Payload},
                },
                ReviewShowRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                RewardedShowRequest r => new Dictionary<string, object>
                {
                    {"type", r.GetType()},
                },
                _ => throw new ArgumentOutOfRangeException(nameof(request))
            };
        }
    }
}