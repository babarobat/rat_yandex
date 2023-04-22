using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace RatYandex.Runtime
{
    public class ReviewDialogRequestProvider
    {
        private readonly YaApiBridge _bridge;
        private readonly WaitUntil _waitResponse;
        
        private bool _hasResponse;
        private bool _isSuccess;
        private string _data;

        public ReviewDialogRequestProvider(YaApiBridge bridge)
        {
            _bridge = bridge;

            _bridge.OnDialogReviewClosed = OnDialogClose;
            _bridge.OnDialogReviewError = OnError;

            _bridge.GetReviewInfo();
            _waitResponse = new(() => _hasResponse);
        }
        
        public void OpenReviewDialog(Action<bool> onClose, Action onError)
        {
            _bridge.StartCoroutine(GetInternal(onClose, onError));
        }

        private IEnumerator GetInternal(Action<bool> onClose, Action onError)
        {
            _bridge.DialogReviewOpen();
            
            _hasResponse = false;
            
            yield return _waitResponse;
            
            if (_isSuccess)
            {
                var result = JsonConvert.DeserializeObject<bool>(_data);
                Clear();
                onClose?.Invoke(result);
            }
            else
            {
                Clear();
                onError?.Invoke();
            }
        }

        private void OnDialogClose(string data)
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