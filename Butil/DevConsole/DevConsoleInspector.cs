using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using b33bo.dev;

#if UNITY_EDITOR
namespace b33bo.meta
{
    public class DevConsoleInspector : EditorWindow
    {
        string cmd;
        [MenuItem("Window/General/DevConsole")]
        public static void ShowWindow()
        {
            GetWindow<DevConsoleInspector>("Command").minSize = new Vector2(5, 5);
        }

        public void OnGUI()
        {
            Event e = Event.current;

            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.Tab)
                {
                    cmd = DevCommands.Autocomplete(cmd);
                }
            }

            GUILayout.BeginHorizontal();
            cmd = EditorGUILayout.TextField("", cmd);

            if (GUILayout.Button("Do", GUILayout.Width(100)))
                Debug.Log(DevCommands.Command(true, cmd));

            GUILayout.EndHorizontal();

            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.Return)
                {
                    Debug.Log(DevCommands.Command(true, cmd));
                }
            }
        }
    }
}
#endif