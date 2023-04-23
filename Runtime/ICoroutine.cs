using System.Collections;
using UnityEngine;

namespace RatYandex.Runtime
{
    public interface ICoroutine
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}