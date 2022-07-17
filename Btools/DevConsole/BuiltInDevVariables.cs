using System.Collections.Generic;
using UnityEngine;

namespace Btools.DevConsole
{
    internal class BuiltInDevVariables
    {
        public static Dictionary<string, DevConsoleVariable> Builtins => new Dictionary<string, DevConsoleVariable>()
        {
            {"", new DevConsoleVariable(
                "",
                "empty variable",
                typeof(string), () => "") },

            {"null", new DevConsoleVariable(
                "null",
                "null (nothing) or empty",
                typeof(object), () => null) },

            {"true", new DevConsoleVariable(
                "true",
                "true",
                typeof(bool), () => true
                ) },

            {"false", new DevConsoleVariable(
                "false",
                "false",
                typeof(bool), () => false
                ) },

            {"timescale", new DevConsoleVariable(
                "timescale",
                "timescale that unity runs at. default = 1",
                typeof(float), () => Time.timeScale, x => Time.timeScale = float.Parse(x)) },

            {"maxfps", new DevConsoleVariable(
                "maxfps",
                "if the framerate runs quicker, pause execution until target framerate met.\nuncapped = -1",
                typeof(int), () => Application.targetFrameRate, x => Application.targetFrameRate = int.Parse(x)) },

            {"fps", new DevConsoleVariable(
                "fps",
                "1 / deltatime",
                typeof(float), () => 1 / Time.deltaTime) },

            {"deltatime", new DevConsoleVariable(
                "deltatime",
                "time since last frame",
                typeof(float), () => Time.deltaTime) },

            {"fixed_delta_time", new DevConsoleVariable("fixed_delta_time",
                "the time between frames for fixed update",
                typeof(float), () => Time.fixedDeltaTime, x => Time.fixedDeltaTime = float.Parse(x)) },

            {"fixed_fps", new DevConsoleVariable("fixed_fps", "1 / fixed_delta_time",
                typeof(float), () => 1 / Time.fixedDeltaTime, x => Time.fixedDeltaTime = 1 / float.Parse(x)) },

            {"cam_size", new DevConsoleVariable(
                "cam_size", 
                "camera orthographic size for default camera", 
                typeof(float), () => Camera.main.orthographicSize, x => Camera.main.orthographicSize = float.Parse(x)) },

            {"platform", new DevConsoleVariable(
                "platform",
                "the operating system that the current user is using",
                typeof(RuntimePlatform), () => Application.platform.ToString()) },

            {"unity_pro_license", new DevConsoleVariable(
                "unity_pro_license",
                "does the user have unity pro license", 
                typeof(bool), () => Application.HasProLicense()) },

            {"version", new DevConsoleVariable(
                "version",
                "the version of the current application",
                typeof(string), () => Application.version) },

            {"unity_version", new DevConsoleVariable(
                "unity_version",
                "the version of unity that the project uses",
                typeof(string), () => Application.unityVersion) },

            {"system_lang", new DevConsoleVariable(
                "system_lang",
                "the language of the operating system",
                typeof(SystemLanguage), () => Application.systemLanguage) },

            {"run_in_background", new DevConsoleVariable(
                "run_in_background",
                "can the current project run when not focused",
                typeof(bool), () => Application.runInBackground, x => Application.runInBackground = bool.Parse(x)) },

            {"product_name", new DevConsoleVariable(
                "product_name",
                "the name of the product",
                typeof(string), () => Application.productName) },

            {"focused", new DevConsoleVariable(
                "focused",
                "is the current application focused",
                typeof(string), () => Application.isFocused) },

            {"company", new DevConsoleVariable(
                "company", 
                "the company that made the game", 
                typeof(string), () => Application.companyName) },

            {"splash_screen_on", new DevConsoleVariable(
                "splash_screen_on", "is the unity splash screen showing",
                typeof(string), () => !UnityEngine.Rendering.SplashScreen.isFinished) },

            {"glitch_variable", new DevConsoleVariable(
                "glitch_variable",
                "a variable that throws an error when attempting to get it's value",
                typeof(void), null) }
        };
    }
}
