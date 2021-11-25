using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Threading;
using b33bo.utils;
using b33bo.utils.emptyMonoBehaviour;

namespace b33bo.timedEvents
{
    /// <summary>Uses Delegates combined with coroutines to make timed events, without having to do is messily</summary>
    public static class TimedEvents
    {
        #region After Time
        /// <summary>Run an action after the specified time has finished</summary>
        /// <param name="action">Code to run</param>
        /// <param name="time">Time to wait</param>
        public static void RunAfterTime(Action action, float time)
        {
            EmptyMonoBehaviour.EmptyMonobehaviour.StartCoroutine(Coroutine_AfterTime(action, time));
        }

        private static IEnumerator Coroutine_AfterTime(Action action, float t)
        {
            yield return new WaitForSeconds(t);
            action.Invoke();
        }

        #endregion
        #region After Real Time
        /// <summary>Run an action after the specified time has finished (ignores timescale)</summary>
        /// <param name="action">Code to run</param>
        /// <param name="time">Time to wait</param>
        public static void RunAfterRealTime(Action action, float time)
        {
            EmptyMonoBehaviour.EmptyMonobehaviour.StartCoroutine(Coroutine_AfterRealTime(action, time));
        }

        private static IEnumerator Coroutine_AfterRealTime(Action action, float t)
        {
            yield return new WaitForSecondsRealtime(t);
            action.Invoke();
        }

        #endregion
        #region Repeat

        /// <summary> Repeat an action %length% times after %interval% time has passed</summary>
        /// <param name="action">Code to run</param>
        /// <param name="interval">how often should it run</param>
        /// <param name="length">how many iterations should it make</param>
        public static void Repeat(Action action, int length, float interval)
        {
            EmptyMonoBehaviour.EmptyMonobehaviour.StartCoroutine(Coroutine_Repeat(action, interval, length));
        }

        /// <summary> Repeat an action forever after %interval% time has passed</summary>
        /// <param name="action">Code to run</param>
        /// <param name="interval">how often should it run</param>
        public static void Repeat(Action action, float interval)
        {
            Repeat(action, -1, interval);
        }

        static IEnumerator Coroutine_Repeat(Action action, float interval, int length)
        {
            bool repeatForever = length < 0;
            for (int i = 0; i < length || repeatForever; i++)
            {
                yield return new WaitForSeconds(interval);
                action.Invoke();
            }
        }

        #endregion
        #region Repeat For Seconds
        /// <summary>Repeats the action for the specified time, with the parameter as the current time</summary>
        /// <param name="action">Code to run</param>
        /// <param name="seconds">Total duration</param>
        public static void RepeatForSeconds(Action<float> action, float seconds)
        {
            EmptyMonoBehaviour.EmptyMonobehaviour.StartCoroutine(Coroutine_RepeatForSecs(action, seconds));
        }

        static IEnumerator Coroutine_RepeatForSecs(Action<float> action, float seconds)
        {
            float time = 0;

            while (seconds > time)
            {
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
                action.Invoke(time);
            }
        }

        #endregion
        #region Repeat Real Time

        /// <summary> Repeat an action %length% times after %interval% time has passed</summary>
        /// <param name="action">Code to run</param>
        /// <param name="interval">how often should it run</param>
        /// <param name="length">how many iterations should it make</param>
        public static void RepeatRealtime(Action action, float interval, int length)
        {
            EmptyMonoBehaviour.EmptyMonobehaviour.StartCoroutine(Coroutine_RepeatRealTime(action, interval, length));
        }

        /// <summary> Repeat an action forever after %interval% time has passed (ignores timescale)</summary>
        /// <param name="action">Code to run</param>
        /// <param name="interval">how often should it run</param>
        public static void RepeatRealtime(Action action, float interval)
        {
            RepeatRealtime(action, interval, -1);
        }


        static IEnumerator Coroutine_RepeatRealTime(Action action, float interval, int length)
        {
            bool repeatForever = length < 0;
            for (int i = 0; i < length || repeatForever; i++)
            {
                yield return new WaitForSecondsRealtime(interval);
                action.Invoke();
            }
        }

        #endregion
        #region Frames

        /// <summary> Repeat the code for the specified amount of frames </summary>
        /// <param name="action">Code to run</param>
        /// <param name="length">Frame count</param>
        public static void RepeatForFrames(Action action, int length)
        {
            EmptyMonoBehaviour.EmptyMonobehaviour.StartCoroutine(Coroutine_RepeatFrame(action, length));
        }

        /// <summary> Repeat the code every frame </summary>
        /// <param name="action">Code to run</param>
        public static void RepeatForFrames(Action action)
        {
            RepeatForFrames(action, -1);
        }

        static IEnumerator Coroutine_RepeatFrame(Action action, int length)
        {
            bool repeatForever = length < 0;
            for (int i = 0; i < length || repeatForever; i++)
            {
                yield return new WaitForEndOfFrame();
                action.Invoke();
            }
        }

        #endregion
        #region Until

        /// <summary> Repeats the code until the condition is met, every frame </summary>
        /// <param name="action">Code to run</param>
        /// <param name="StopRunning">The condition to stop at</param>
        public static void RepeatUntil(Action action, Func<bool> StopRunning)
        {
            EmptyMonoBehaviour.EmptyMonobehaviour.StartCoroutine(Coroutine_RepeatUntil(action, StopRunning));
        }

        /// <summary> Repeats the code until the condition is met, every interval </summary>
        /// <param name="action">Code to run</param>
        /// <param name="StopRunning">The condition to stop at</param>
        /// <param name="interval">The interval for the code to run</param>
        public static void RepeatUntil(Action action, Func<bool> StopRunning, float interval)
        {
            EmptyMonoBehaviour.EmptyMonobehaviour.StartCoroutine(Coroutine_RepeatUntil(action, StopRunning, interval));
        }


        static IEnumerator Coroutine_RepeatUntil(Action action, Func<bool> StopRunning, float interval)
        {
            while (!StopRunning.Invoke())
            {
                action.Invoke();
                yield return new WaitForSeconds(interval);
            }
        }

        static IEnumerator Coroutine_RepeatUntil(Action action, Func<bool> predicate)
        {
            while (!predicate.Invoke())
            {
                action.Invoke();
                yield return new WaitForEndOfFrame();
            }
        }

        #endregion
    }
}