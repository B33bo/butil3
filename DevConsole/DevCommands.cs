using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using b33bo.utils;

namespace b33bo.dev
{
    /// <summary>Dev Commands</summary>
    public static class DevCommands
    {
        /// <summary>A list of all commands (for autocomplete)</summary>
        public static string[] commands = new string[]
            {"testing", "clear", "scene~SceneName~loadscene", "exit", "timescale~value", "fov~value", "camsize~value", "objects", "commands",
            "forcecrash"};

        const string ErrorText = "<color=#FF0000>";
        const string WarningText = "<color=#FFFF00>";

        /// <summary>Run a command, where the string isn't split</summary>
        /// <param name="UserCommand">The command</param>
        /// <param name="ErrorsWithColours">If there's an error, should it return a color=#FF0000 or not?</param>
        /// <returns>The output of the command(s)</returns>
        public static string Command(string UserCommand, bool ErrorsWithColours)
        {
            string[] Commands = UserCommand.Split(';');
            string returnValue = "";

            for (int i = 0; i < Commands.Length; i++)
            {
                returnValue += "\r\n" + Command(ErrorsWithColours, Commands[i].Split('~'));
            }

            if (returnValue.Length <= 2)
                return string.Empty;

            return returnValue.Substring(2);
        }

        /// <summary>Run a command, where the string array is the parameters</summary>
        /// <param name="Params">The parameters of the command (1st index is the command)</param>
        /// <param name="ErrorsWithColours">If there's an error, should it return a color=#FF0000 or not?</param>
        /// <returns>The output of the command</returns>
        public static string Command(bool ErrorsWithColours, params string[] Params)
        {
            string error = ErrorsWithColours ? ErrorText : "";
            string warning = ErrorsWithColours ? WarningText : "";
            string endCol = ErrorsWithColours ? "</color>" : "";

            switch (Params[0].ToLower().Trim())
            {
                default:
                    return $"{error}ERROR: Command {Params[0]} NOT FOUND{endCol}";

                case "":
                    return "";

                case "testing":
                    return "Hello World!";

                case "hellothere":
                    return "Hi!";

                case "clear":
                    Debug.Log("butil--CLEAR");
                    return "Phew! That was getting crowded!";

                case "scene":
                    string CurrentScene = SceneManager.GetActiveScene().name;
                    string NewScene = ManipulateVariable(SceneManager.GetActiveScene().name, Params);

                    string type = SafeGet(2, Params, "loadscene").ToLower();

                    if (CurrentScene != NewScene)
                    {
                        if (type == "loadscene")
                            SceneManager.LoadScene(NewScene);
                        else if (type == "loadtop")
                            SceneManager.LoadScene(NewScene, LoadSceneMode.Additive);
                        else if (type == "unloadscene")
                            SceneManager.UnloadSceneAsync(NewScene);
                    }

                    return NewScene;
                case "exit":
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.ExitPlaymode();
#endif
                    Application.Quit();
                    return "Quit Game.";
                case "timescale":
                    Time.timeScale = ManipulateVariable(Time.timeScale, Params);
                    return Time.timeScale.ToString();
                case "fov":
                    Camera.main.fieldOfView = ManipulateVariable(Camera.main.fieldOfView, Params);
                    return Camera.main.fieldOfView.ToString();
                case "camsize":
                    Camera.main.orthographicSize = ManipulateVariable(Camera.main.orthographicSize, Params);
                    return Camera.main.orthographicSize.ToString();
                case "objects":
                    string GameObjects = "";
                    foreach (GameObject item in Other.GetGameObjects())
                    {
                        GameObjects += item.name + "\n";
                    }
                    return GameObjects;
                case "commands":
                    string CommandPrinter = "";
                    foreach (string item in commands)
                    {
                        CommandPrinter += item + "\n";
                    }
                    return CommandPrinter;
                case "forcecrash":
                    UnityEngine.Diagnostics.Utils.ForceCrash(SafeGet(1, Params, UnityEngine.Diagnostics.ForcedCrashCategory.Abort));
                    return "NUKED";
            }
        }

        /// <summary>Autocompletes the command</summary>
        /// <param name="autoComplete">The first few letters</param>
        /// <returns>The closest command</returns>
        public static string Autocomplete(string autoComplete)
        {
            if (autoComplete == "")
                return "";

            for (int i = 0; i < DevCommands.commands.Length; i++)
            {
                if (DevCommands.commands[i].ToLower().StartsWith(autoComplete.ToLower()))
                    //Inserts the autocomplete string before the text to keep capitilization
                    return autoComplete + DevCommands.commands[i].Substring(autoComplete.Length);

                //Example: typed = HELL command = hellothere
                //Output: HELLothere
            }

            return "";
        }


        private static T ManipulateVariable<T>(T variable, string[] Params, int index = 1)
        {
            if (Params.Length <= index)
                return variable;

            if (Params[index] == "~")
                return variable;

            float VariableModifier;
            if (!float.TryParse(Params[index], out VariableModifier))
            {
                if (!float.TryParse(Params[index].Substring(1), out VariableModifier))
                    return (T)System.Convert.ChangeType(Params[index], typeof(T));
            }

            float temp = (float)System.Convert.ChangeType(variable, typeof(float));
            switch (Params[index][0])
            {
                default:
                    return (T)System.Convert.ChangeType(Params[index], typeof(T));
                case '+':
                    return (T)System.Convert.ChangeType(temp + VariableModifier, typeof(T));
                case '-':
                    return (T)System.Convert.ChangeType(temp - VariableModifier, typeof(T));
                case '*':
                    return (T)System.Convert.ChangeType(temp * VariableModifier, typeof(T));
                case '/':
                    return (T)System.Convert.ChangeType(temp / VariableModifier, typeof(T));
            }
        }

        private static T SafeGet<T>(int Index, string[] Params, T defaultValue)
        {
            if (Index >= Params.Length)
                return defaultValue;
            return (T)System.Convert.ChangeType(Params[Index], typeof(T));
        }
    }

    /// <summary>Advanced log methods</summary>
    public static class AdvancedLogger
    {
        /// <summary>Prints a colour to the console</summary>
        /// <param name="color">The colour to log</param>
        public static void LogColor(Color color)
        {
            Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">██████████</color> = " + color);
        }

        /// <summary>Prints a colour to the console, but bri'ish</summary>
        /// <param name="color">The colour to log</param>
        /// <remarks>It's annoying typing Color instead of ColoUr</remarks>
        public static void LogColour(Color colour)
        {
            LogColor(colour);
        }
    }
}