using UnityEngine;

namespace b33bo.utils
{
    /// <summary>The FPS of the game</summary>
    public static class FPSreader
    {
        /// <summary> Minimum time between frames </summary>
        public static float MinimumTimeDelta
        {
            get
            {
                return 1 / Application.targetFrameRate;
            }
            set
            {
                Application.targetFrameRate = (int)(1f / value);
            }
        }

        /// <returns>The Frames per Second</returns>
        public static float FPS()
        {
            return 1f / Time.deltaTime;
        }

        /// <param name="DeltaTime">Specify which delta time you use</param>
        /// <returns>The Frames per Second</returns>
        /// <example>FPSreader.FPS(Time.unscaledDeltaTime)</example>
        public static float FPS(float DeltaTime)
        {
            return 1f / DeltaTime;
        }

        /// <param name="DeltaTime">Specify which delta time you use</param>
        /// <param name="time">Specify the time taken</param>
        /// <returns>The Frames per Second</returns>
        /// <example>FPSreader.FPS(Time.unscaledDeltaTime)</example>
        public static float FPS(float DeltaTime, float time)
        {
            return time / DeltaTime;
        }

        /// <summary>Can be better if your framerate takes a huge dip, but not good for debugging because it's the average</summary>
        /// <returns>The average framerate</returns>
        public static float AverageFPS()
        {
            return Time.frameCount / Time.realtimeSinceStartup;
        }
    }
}
