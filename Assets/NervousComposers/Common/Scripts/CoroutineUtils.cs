using System;
using System.Collections;
using UnityEngine;

namespace Jusw85.Common
{
    public static class CoroutineUtils
    {
        /// <summary>
        /// Usage: StartCoroutine(CoroutineUtils.Chain(...))
        /// For example:
        /// Note 1 frame delay after Do()
        ///     StartCoroutine(CoroutineUtils.Chain(this,
        ///         CoroutineUtils.Do(() => Debug.Log("A")),
        ///         CoroutineUtils.WaitForSeconds(2),
        ///         CoroutineUtils.Do(() => Debug.Log("B")),
        ///         CoroutineUtils.WaitForSeconds(3),
        ///         CoroutineUtils.Do(() => Debug.Log("C"))));
        ///
        /// No frame delays
        ///     Debug.Log("A");
        ///     StartCoroutine(CoroutineUtils.Chain(this,
        ///         CoroutineUtils.Delay(() => Debug.Log("B"), 2),
        ///         CoroutineUtils.Delay(() => Debug.Log("C"), 3)));
        /// </summary>
        public static IEnumerator Chain(MonoBehaviour obj, params IEnumerator[] actions)
        {
            foreach (IEnumerator action in actions)
            {
                yield return obj.StartCoroutine(action);
            }
        }

        /// <summary>
        /// Usage: StartCoroutine(CoroutineUtils.DelaySeconds(action, delay))
        /// For example:
        ///     StartCoroutine(CoroutineUtils.DelaySeconds(
        ///         () => DebugUtils.Log("2 seconds past"), 2);
        /// </summary>
        public static IEnumerator DelaySeconds(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

        public static IEnumerator WaitForSeconds(float time)
        {
            yield return new WaitForSeconds(time);
        }

        public static IEnumerator Repeat(Action action, float delay, float interval)
        {
            yield return new WaitForSeconds(delay);
            while (true)
            {
                action();
                yield return new WaitForSeconds(interval);
            }
        }

        /// <summary>
        /// 1 frame delay at the end 
        /// </summary>
        public static IEnumerator Do(Action action)
        {
            action();
            yield return null;
        }
    }
}