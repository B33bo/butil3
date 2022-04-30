using System;
using System.Reflection;
using System.Collections.Generic;

namespace Btools.DevConsole
{
    public static class DevCommands
    {
        private static Dictionary<string, Func<string[], string>> Commands = BuiltInDevCommands.Builtins;

        public static string SetProperty(string[] Parameters, PropertyInfo propertyInfo, object target)
        {
            if (Parameters.Length > 1)
                propertyInfo.SetValue(target, Convert.ChangeType(Parameters[1], propertyInfo.PropertyType));

            return propertyInfo.GetValue(target).ToString();
        }

        public static bool TryAdd(string CommandName, Func<string[], string> CommandAction)
        {
            CommandName = CommandName.ToLower();
            if (Commands.ContainsKey(CommandName))
                return false;
            Commands.Add(CommandName, CommandAction);
            return true;
        }

        public static string AutoComplete(string unfinishedString)
        {
            string lowercaseUnfinishedStr = unfinishedString.ToLower();
            foreach (var command in Commands)
            {
                if (command.Key.StartsWith(lowercaseUnfinishedStr))
                    return unfinishedString + command.Key.Substring(unfinishedString.Length); //keep capitilisation
            }
            return unfinishedString;
        }

        public static bool TryAdd(string CommandName, PropertyInfo property, object target) =>
            TryAdd(CommandName, x => SetProperty(x, property, target));

        public static string Excecute(string CommandName, string[] Parameters)
        {
            CommandName = CommandName.ToLower();
            if (!Commands.ContainsKey(CommandName))
                return $"<#FF0000>Command '<noparse>{CommandName}</noparse>' Not found.</color>";
                //throw new ArgumentException("The given key was not present", CommandName);

            return Commands[CommandName].Invoke(Parameters);
        }
    }
}
