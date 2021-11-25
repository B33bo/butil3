using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace b33bo.meta
{
#if UNITY_EDITOR
    /// <summary>Only used for the Gameobject/Create function for butil</summary>
    public class GameobjectMakers
    {
        /// <summary>Creates a UI element, as it would normally</summary>
        /// <param name="path">file Path of the prefab</param>
        public static void LoadUI(string path)
        {
            if (Selection.activeGameObject == null)
            {
                //Canvases should automatically be selected if nothing else is
                Canvas newCanvas = MonoBehaviour.FindObjectOfType<Canvas>();
                if (newCanvas != null)
                    Selection.activeTransform = newCanvas.transform;
            }

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            Selection.activeObject = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            PrefabUtility.UnpackPrefabInstance(Selection.activeGameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
        }

        [MenuItem("GameObject/UI/Slider2D - butil", priority = 2034)]
        public static void Slider2D()
        {
            LoadUI("Assets/Butil/Slider2D/Slider2D.prefab");
        }

        [MenuItem("GameObject/UI/DialougeBox - butil", priority = 3000)]
        public static void DialougeBox()
        {
            LoadUI("Assets/Butil/Dialouge/Dialouge.prefab");
        }

        [MenuItem("GameObject/UI/Joystick - butil", priority = 3000)]
        public static void Joystick()
        {
            LoadUI("Assets/Butil/Joystick/Joystick.prefab");
        }

        [MenuItem("GameObject/UI/Color Picker - butil", priority = 3000)]
        public static void ColorPicker()
        {
            LoadUI("Assets/Butil/ColourPicker/ColorPicker.prefab");
        }

        [MenuItem("GameObject/UI/Tooltip - butil", priority = 3000)]
        public static void Tooltip()
        {
            LoadUI("Assets/Butil/Tooltip/Tooltip.prefab");
        }

        [MenuItem("GameObject/UI/Window - butil", priority = 3000)]
        public static void Window()
        {
            LoadUI("Assets/Butil/Window/Window.prefab");
        }

        [MenuItem("GameObject/UI/DevConsole - butil", priority = 3000)]
        public static void DevConsole()
        {
            LoadUI("Assets/Butil/DevConsole/DevConsole.prefab");
        }

        [MenuItem("GameObject/UI/multi Tool - butil", priority = 3000)]
        public static void UImultiTool()
        {
            LoadUI("Assets/Butil/UImultiTool/UImultitool.prefab");
        }
    }
#endif

    /// <summary>Things related to the library itself</summary>
    public static class butil
    {
        /// <summary>The current version of Butil</summary>
        public static float version = 3.0f;
    }
}
