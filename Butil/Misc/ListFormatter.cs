using System.Collections.Generic;
using System;

namespace b33bo.utils
{
    /// <summary>Lists, but formatted to be human-readable</summary>
    public static class ListFormatter
    {
        /// <summary> Formats the array by a newline character after each entry</summary>
        /// <returns>A readable version of the array, seperated by a newline</returns>
        public static string ToFormattedString<T>(this T[] array)
        {
            return ToFormattedString(array, "\r\n");
        }

        /// <summary> Formats the array by a custom string after each entry</summary>
        /// <param name="separator">Thing to place between entries</param>
        /// <returns>A readable version of the array, seperated by a character of your choice</returns>
        public static string ToFormattedString<T>(this T[] array, string separator)
        {
            string s = "";

            for (int i = 0; i < array.Length; i++)
            {
                s += separator + array[i].ToString();
            }

            return s.Substring(separator.Length);
        }

        /// <summary>Formats the array by a custom script</summary>
        /// <typeparam name="T">The type of list</typeparam>
        /// <param name="array">the array</param>
        /// <param name="runForEach">the action to perform</param>
        /// <returns>the array formatted by a custom script</returns>
        public static string ToFormattedString<T>(this T[] array, Func<T, object> runForEach)
        {
            string s = "";

            for (int i = 0; i < array.Length; i++)
            {
                s += runForEach.Invoke(array[i]);
            }

            return s;
        }

        /// <summary>Formats the list by a custom script</summary>
        /// <typeparam name="T">The type of list</typeparam>
        /// <param name="array">the list</param>
        /// <param name="runForEach">the action to perform</param>
        /// <returns>the array formatted by a custom script</returns>
        public static string ToFormattedString<T>(this List<T> array, Func<T, object> runForEach)
        {
            string s = "";

            for (int i = 0; i < array.Count; i++)
            {
                s += runForEach.Invoke(array[i]);
            }

            return s;
        }

        /// <summary> Formats the list by a newline character after each entry</summary>
        /// <returns>A readable version of the list, seperated by a newline</returns>
        public static string ToFormattedString<T>(this List<T> list)
        {
            return ToFormattedString(list, "\n");
        }

        /// <summary> Formats the list by a custom string after each entry</summary>
        /// <param name="separator">Thing to place between entries</param>
        /// <returns>A readable version of the list, seperated by a character of your choice</returns>
        public static string ToFormattedString<T>(this List<T> list, string separator)
        {
            string s = "";

            for (int i = 0; i < list.Count; i++)
            {
                s += list[i].ToString() + separator;
            }

            //The string would start with the seperator (a comma, for example) if this wasn't here
            return s;
        }

        /// <summary>The same as substring, but for arrays</summary>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <param name="array">The list to sublist</param>
        /// <param name="startIndex">The starting index</param>
        /// <returns>The entire list, except for the first couple</returns>
        public static T[] Sublist<T>(this T[] array, int startIndex)
        {
            T[] newArray = new T[array.Length - startIndex];
            for (int i = startIndex; i < array.Length; i++)
            {
                newArray[i - startIndex] = array[i];
            }

            return newArray;
        }

        /// <summary>The same as substring, but for arrays</summary>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <param name="array">The list to sublist</param>
        /// <param name="startIndex">The starting index</param>
        /// <param name="length">The length to sub list from</param>
        /// <returns>The list inside of the bounds</returns>
        public static T[] Sublist<T>(this T[] array, int startIndex, int length)
        {
            T[] newArray = new T[length];

            for (int i = startIndex; i < length + startIndex; i++)
            {
                newArray[i - startIndex] = array[i];
            }

            return newArray;
        }
    }
}
