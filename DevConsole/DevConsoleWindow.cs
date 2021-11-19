using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using b33bo.timedEvents;
using TMPro;
using b33bo.numerics;
using b33bo.dev;

namespace b33bo.components
{
    /// <summary>The window for the dev console</summary>
    public class DevConsoleWindow : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI output;

        [SerializeField]
        private TextMeshProUGUI autoCompleteTxt;

        [SerializeField]
        private ScrollRect scroll;

        [SerializeField]
        private TMP_InputField input;

        void Awake()
        {
            output.text = "--Dev Console Output\n";
            Application.logMessageReceived += UpdateLog;
        }

        void UpdateLog(string message, string stacktrace, LogType logType)
        {
            if (message == "butil--CLEAR")
                output.text = "";

            string colour = "";

            switch (logType)
            {
                case LogType.Error:
                    colour = "<#FF0000>";
                    break;
                case LogType.Assert:
                    colour = "<#FF0000>";
                    break;
                case LogType.Warning:
                    colour = "<#FFFF00>";
                    break;
                case LogType.Log:
                    colour = "<#00B71B>";
                    break;
                case LogType.Exception:
                    colour = "<#FF0000>";
                    break;
                default:
                    break;
            }
            output.text += $"{colour}{message}\n";
            scroll.verticalNormalizedPosition = -1;
        }

        public void DoCommand(string command)
        {
            Debug.Log(DevCommands.Command(command, true));
        }

        public void DoCommand(TMP_InputField command)
        {
            Debug.Log(DevCommands.Command(command.text, true));
        }

        public void SetAutocompleteDummyTxt(string autoComplete)
        {
            autoCompleteTxt.text = DevCommands.Autocomplete(autoComplete);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                input.text = DevCommands.Autocomplete(input.text);
                input.caretPosition = input.text.Length;
            }
        }
    }
}
