using UnityEngine;
using TMPro;
using Btools.utils;
using Btools.Extensions;

namespace Btools.DevConsole
{
    public class DevConsoleWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField commandInput;

        [SerializeField]
        private TextMeshProUGUI PlaceholderText;

        [SerializeField]
        private TextMeshProUGUI CommandOutput;

        private void Start()
        {
            Application.logMessageReceived += LogMessageRevieved;
        }

        private void LogMessageRevieved(string condition, string stackTrace, LogType type)
        {
            string ColorString = type switch
            {
                LogType.Assert => "<#FF0000>",
                LogType.Error => "<#FF0000>",
                LogType.Exception => "<#FF0000>",
                LogType.Log => "<#CCCCCC>",
                LogType.Warning => "<#FFFF00>",
                _ => "<#FFFFFF>",
            };

            CommandOutput.text += $"{ColorString}{condition}</color>";
        }

        public void TypedWord(string text)
        {
            string newString = DevCommands.AutoComplete(text);
            PlaceholderText.text = newString;
        }

        public void Run()
        {
            Run(commandInput.text);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Tab))
                return;
            if (!commandInput.isFocused)
                return;

            commandInput.text = DevCommands.AutoComplete(commandInput.text);
            commandInput.caretPosition = commandInput.text.Length;
        }

        public void Run(string text)
        {
            string[] parameters = text.SplitEscaped(',');

            string commandCallback = DevCommands.Excecute(parameters[0].ToLower(), parameters);
            CommandOutput.text += commandCallback + "\n";
            CommandOutput.rectTransform.anchoredPosition = new Vector2(0, CommandOutput.renderedHeight + 50);

            if (commandCallback == "$clearConsole")
                CommandOutput.text = "";
            else if (commandCallback == "$resetConsole")
                GetComponent<Btools.Components.Window>().IsFullscreen = false;
        }
    }
}
