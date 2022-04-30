using UnityEngine;
using UnityEditor;
using Btools.Extensions;

namespace Btools.DevConsole
{
#if UNITY_EDITOR
    public class DevInspectorWindow : EditorWindow
    {
        private string CurrentCommand;
        [MenuItem("Window/General/DevConsole")]
        public static void ShowWindow() =>
            GetWindow<DevInspectorWindow>("Command").minSize = new Vector2(5, 5);

        public void OnGUI()
        {
            Event e = Event.current;

            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.Tab)
                    CurrentCommand = DevCommands.AutoComplete(CurrentCommand);
            }

            GUILayout.BeginHorizontal();
            CurrentCommand = EditorGUILayout.TextField(CurrentCommand);

            if (GUILayout.Button("Do", GUILayout.Width(100)))
            {
                string[] parameters = CurrentCommand.SplitEscaped(',');
                Debug.Log(DevCommands.Excecute(parameters[0], parameters));
            }

            GUILayout.EndHorizontal();
        }
    }
#endif
}
