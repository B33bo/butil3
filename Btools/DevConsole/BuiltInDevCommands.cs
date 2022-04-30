using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Btools.DevConsole
{
    internal static class BuiltInDevCommands
    {
        public static Type thisType => typeof(BuiltInDevCommands);
        public static Dictionary<string, Func<string[], string>> Builtins => new Dictionary<string, Func<string[], string>>()
        {
            {"", _ => "" },
            {"timescale", x => DevCommands.SetProperty(x, thisType.GetProperty("TimeScale"), null) },
            {"quit", Quit },
            {"exit", Quit },
            {"log", LogMessage },
            {"echo", x => x[1] },
            {"scene", LoadScene },
            {"clear", _ => "$clearConsole" },
            {"maxfps", x => DevCommands.SetProperty(x, thisType.GetProperty("MaxFPS"), null)},
            {"fps", _ => FPS.ToString() },
            {"objects", x => ListObjects(x) },
            {"destroy", x => Destroy(x) },
            {"pos", Position },
            {"rot", Rotation},
            {"scale", Scale},
            {"clone", Clone},
            {"parent", SetParent},
            {"reset", _ => "$resetConsole" },
            {"varedit", ComponentVariable },
            {"components", ComponentsOf },
        };
#pragma warning disable IDE0051 // Remove unused private members

        public static float TimeScale
        {
            get => Time.timeScale;
            set => Time.timeScale = value;
        }

        public static int MaxFPS
        {
            get => Application.targetFrameRate;
            set => Application.targetFrameRate = value;
        }

        public static float FPS => 1f / Time.deltaTime;

        private static string Quit(string[] Parameters)
        {
            if (Parameters.Length <= 1 || Parameters[1].ToLower() != "crash")
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return "bye!";
            }

            UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.Abort);
            return "Force crash";
        }

        private static string LogMessage(string[] Parameters)
        {
            if (Parameters.Length < 3)
            {
                Debug.Log(Parameters[1]);
                return Parameters[1];
            }

            LogType logType = (LogType)(-1);
            switch (Parameters[2].ToLower())
            {
                default:
                    Debug.LogWarning($"{Parameters[2]} is not a log type");
                    Debug.Log(Parameters[1]);
                    break;
                case "log":
                    Debug.Log(Parameters[1]);
                    logType = LogType.Log;
                    break;
                case "warn":
                case "warning":
                    Debug.LogWarning(Parameters[1]);
                    logType = LogType.Warning;
                    break;
                case "error":
                    Debug.LogError(Parameters[1]);
                    logType = LogType.Error;
                    break;
                case "assert":
                case "assertion":
                    Debug.LogAssertion(Parameters[1]);
                    logType = LogType.Assert;
                    break;
                case "exception":
                    Debug.LogException(new Exception(Parameters[1]));
                    logType = LogType.Exception;
                    break;
                case "all":
                    Debug.Log(Parameters[1]);
                    Debug.LogWarning(Parameters[1]);
                    Debug.LogError(Parameters[1]);
                    Debug.LogAssertion(Parameters[1]);
                    Debug.LogException(new Exception(Parameters[1]));
                    break;
            }
            string ColorString = logType switch
            {
                LogType.Assert => "<#FF0000>",
                LogType.Error => "<#FF0000>",
                LogType.Exception => "<#FF0000>",
                LogType.Log => "<#CCCCCC>",
                LogType.Warning => "<#FFFF00>",
                _ => "<#FFFFFF>",
            };

            return ColorString + Parameters[1] + "</color>";
        }

        private static string LoadScene(string[] Parameters)
        {
            if (Parameters.Length == 1)
                return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (Parameters.Length == 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(Parameters[1]);
                return Parameters[1];
            }

            if (Parameters.Length >= 3)
            {
                switch (Parameters[2].ToLower())
                {
                    default:
                        return $"{Parameters[2]} is invalid";
                    case "unload":
                        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(Parameters[2]);
                        return "Unloaded " + Parameters[2];
                    case "load":
                    case "single":
                        UnityEngine.SceneManagement.SceneManager.LoadScene(Parameters[2]);
                        return "loaded " + Parameters[2];
                    case "add":
                        UnityEngine.SceneManagement.SceneManager.LoadScene(Parameters[2], UnityEngine.SceneManagement.LoadSceneMode.Additive);
                        return "added " + Parameters[2];
                }
            }

            return "error";
        }

        private static string ListObjects(string[] Parameters)
        {
            bool tree = true;
            for (int i = 1; i < Parameters.Length; i++)
            {
                if (Parameters[i].Replace(" ", "") == "tree=false")
                    tree = false;
            }

            if (!tree)
            {
                var Gameobjects = GameObject.FindObjectsOfType<GameObject>();
                string s = "";
                for (int i = 0; i < Gameobjects.Length; i++)
                    s += Gameobjects[i].name + "\n";
                return s;
            }

            var AllGameobjects = GameObject.FindObjectsOfType<GameObject>();
            List<GameObject> ParentlessGameobjects = new List<GameObject>(10); //L + Fatherless
            for (int i = 0; i < AllGameobjects.Length; i++)
            {
                if (AllGameobjects[i].transform.parent is null)
                    ParentlessGameobjects.Add(AllGameobjects[i]);
            }

            string AllObjects = "";
            for (int i = 0; i < ParentlessGameobjects.Count; i++)
            {
                AllObjects += GetObjectsInsideOf(ParentlessGameobjects[i].transform, 0) + "\n";
            }

            return AllObjects;
        }

        private static string GetObjectsInsideOf(Transform TargetObject, int tabIndex)
        {
            string[] colourCodes = new string[]
            {
                "#660000",
                "#666600",
                "#006600",
                "#006666",
                "#000066",
                "#660066",
                "#000066",
                "#006666",
                "#006600",
                "#666600",
            };
            string tabs = "";

            for (int i = 0; i < tabIndex; i++)
                tabs += $"<{colourCodes[i % colourCodes.Length]}>|</color>";

            string MyColourCode = $"<{colourCodes[tabIndex % colourCodes.Length]}>".Replace("6", "F");
            string FinalString = tabs + MyColourCode + TargetObject.name + "</color>";
            Transform[] children = TargetObject.transform.GetComponentsInChildren<Transform>();

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].transform.parent != TargetObject.transform)
                    continue;

                FinalString += "\n" + GetObjectsInsideOf(children[i], tabIndex + 1);
            }

            return FinalString;
        }

        private static string Destroy(string[] Parameters)
        {
            List<string> failedObjects = new List<string>();
            for (int i = 1; i < Parameters.Length; i++)
            {
                GameObject destoryObject = GameObject.Find(Parameters[i]);

                if (destoryObject is null)
                    failedObjects.Add(Parameters[i]);
                else
                    MonoBehaviour.Destroy(destoryObject);
            }
            string failedObjectsString = null;
            for (int i = 0; i < failedObjects.Count; i++)
                failedObjectsString += $"Failed To Destroy: {failedObjects[i]}\n";

            return failedObjectsString ?? "Success";
        }

        private static string Position(string[] Parameters)
        {
            GameObject gameObject = GameObject.Find(Parameters[1]);
            if (Parameters.Length == 4)
                gameObject.transform.position = new Vector2(float.Parse(Parameters[2]), float.Parse(Parameters[3]));
            else if (Parameters.Length > 4)
                gameObject.transform.position = new Vector3(float.Parse(Parameters[2]), float.Parse(Parameters[3]), float.Parse(Parameters[4]));

            return gameObject.transform.position.ToString();
        }

        private static string Rotation(string[] Parameters)
        {
            GameObject gameObject = GameObject.Find(Parameters[1]);

            if (Parameters.Length == 3)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, float.Parse(Parameters[2]));
            else if (Parameters.Length == 5)
                gameObject.transform.rotation = Quaternion.Euler(float.Parse(Parameters[2]), float.Parse(Parameters[3]), float.Parse(Parameters[4]));
            else if (Parameters.Length > 5)
                gameObject.transform.rotation = new Quaternion(float.Parse(Parameters[2]), float.Parse(Parameters[3]), float.Parse(Parameters[4]), float.Parse(Parameters[5]));

            return gameObject.transform.rotation.eulerAngles.ToString();
        }

        private static string Scale(string[] Parameters)
        {
            GameObject gameObject = GameObject.Find(Parameters[1]);

            if (Parameters.Length == 3)
                gameObject.transform.localScale *= float.Parse(Parameters[2]);
            else if (Parameters.Length == 4)
                gameObject.transform.localScale = new Vector3(float.Parse(Parameters[2]), float.Parse(Parameters[3]));
            else if (Parameters.Length > 4)
                gameObject.transform.localScale = new Vector3(float.Parse(Parameters[2]), float.Parse(Parameters[3]), float.Parse(Parameters[4]));

            return gameObject.transform.localScale.ToString();
        }

        private static string Clone(string[] Parameters)
        {
            GameObject gameObject = GameObject.Find(Parameters[1]);
            GameObject newGM;

            if (Parameters.Length > 2)
            {
                newGM = MonoBehaviour.Instantiate(gameObject);
                newGM.name = Parameters[2];
            }
            else
                newGM = MonoBehaviour.Instantiate(gameObject);

            return "Created " + newGM.name;
        }

        private static string SetParent(string[] Parameters)
        {
            GameObject target = GameObject.Find(Parameters[1]);
            GameObject parent = GameObject.Find(Parameters[2]);

            target.transform.parent = parent.transform;

            return $"{target.name} is not parent of {parent.name}";
        }

        //ComponentVar,GameObject,ComponentName,VarName,NewVal?
        private static string ComponentVariable(string[] Parameters)
        {
            GameObject target = GameObject.Find(Parameters[1]);
            Component component = target.GetComponent(Parameters[2]);
            Type type = component.GetType();

            var Target = type.GetMember(Parameters[3])[0];

            MemberType memberType = MemberType.Unknown;

            PropertyInfo propertyInfo = null;
            FieldInfo fieldInfo = null;
            MethodInfo methodInfo = null;

            propertyInfo = Target as PropertyInfo;
            memberType = MemberType.Property;
            if (propertyInfo is null)
            {
                fieldInfo = Target as FieldInfo;
                memberType = MemberType.Field;

                if (fieldInfo is null)
                {
                    memberType = MemberType.Method;
                    methodInfo = Target as MethodInfo;

                    if (methodInfo is null)
                        memberType = MemberType.Unknown;
                }
            }

            if (Parameters.Length < 4)
            {
                return memberType switch
                {
                    MemberType.Field => fieldInfo.GetValue(component).ToString(),
                    MemberType.Property => propertyInfo.GetValue(component).ToString(),
                    MemberType.Method => methodInfo.Invoke(component, new object[0]).ToString(),
                    _ => "unknown member type " + Target.GetType(),
                };
            }

            switch (memberType)
            {
                case MemberType.Field:
                    fieldInfo.SetValue(component, Convert.ChangeType(Parameters[4], fieldInfo.FieldType));
                    break;
                case MemberType.Property:
                    propertyInfo.SetValue(component, Convert.ChangeType(Parameters[4], propertyInfo.PropertyType));
                    break;
                case MemberType.Method:
                    methodInfo.Invoke(component, new object[0]);
                    break;
                default:
                    return "unknown member type " + Target.GetType();
            }

            return Parameters[4];
        }

        private static string ComponentsOf(string[] Parameters)
        {
            GameObject gameObject = GameObject.Find(Parameters[1]);

            Component[] components = gameObject.GetComponents(typeof(Component));
            string s = "";

            MemberInfo[] normalVars = typeof(Component).GetMembers();

            for (int i = 0; i < components.Length; i++)
            {
                s += components[i].GetType().Name + "\n";
            }
            return s;
        }

        private static bool Contains(this MemberInfo[] array, MemberInfo a)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == a)
                    return true;
            }
            return false;
        }

        private enum MemberType : byte
        {
            Unknown,
            Field,
            Property,
            Method,
        }
    }
}
