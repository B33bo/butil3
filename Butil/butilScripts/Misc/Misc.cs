using UnityEngine;

namespace b33bo.utils
{
    /// <summary>Random, useful methods</summary>
    public static class Other
    {
        /// <summary>All gameobjects in the current scene</summary>
        /// <returns>All gameobjects in the current scene</returns>
        public static GameObject[] GetGameObjects()
        {
            return GameObject.FindObjectsOfType<GameObject>();
        }

        /// <summary>Inverts the color</summary>
        /// <param name="color">The color to invert</param>
        /// <returns>The color, with it's RGB values flipped</returns>
        public static Color Invert(this Color color)
        {
            Color newColour = Color.white - color;
            newColour.a = 1;
            return newColour;
        }

        /// <summary>Gets the gameobject from the RaycastHit</summary>
        /// <param name="Raycast">The raycast to convert to a gameobject</param>
        /// <returns>the gameobject from the RaycastHit</returns>
        public static GameObject GetGameobject(this RaycastHit Raycast)
        {
            return Raycast.transform.gameObject;
        }

        /// <summary>Gets the gameobject from the RaycastHit2D</summary>
        /// <param name="Raycast">The raycast to convert to a gameobject</param>
        /// <returns>the gameobject from the RaycastHit2D</returns>
        public static GameObject GetGameobject(this RaycastHit2D Raycast)
        {
            return Raycast.transform.gameObject;
        }
    }

    /// <summary>Methods related to rotation</summary>
    public static class Rotation
    {
        /// <summary>Converts a float to a 2D rotation.</summary>
        /// <param name="rot">The 2D rotation</param>
        /// <returns>Short hand for Quaternion.Euler(0, 0, rot)</returns>
        public static Quaternion To2DRotation(float rot)
        {
            return Quaternion.Euler(0, 0, rot);
        }

        /// <summary>Faces the position (in 2D space), but gives the rotation</summary>
        /// <param name="current">Position to face from</param>
        /// <param name="position">Position to face to</param>
        /// <returns>A quaternion based on what rotation Position should be, if it wanted to face to Current</returns>
        public static Quaternion FacePosition2D(Vector3 current, Vector3 position)
        {
            Vector3 targetPos = position;
            return Quaternion.Euler(0, 0, Mathf.Atan2(targetPos.y - current.y, targetPos.x - current.x) * Mathf.Rad2Deg);
        }

        /// <summary>Faces the position (in 2D space), but gives the rotation</summary>
        /// <param name="current">Position to face from</param>
        /// <param name="position">Position to face to</param>
        /// <param name="Offset">The offset of the rotation</param>
        /// <returns>A quaternion based on what rotation Position should be, if it wanted to face to Current</returns>
        public static Quaternion FacePosition2D(Vector3 current, Vector3 position, float Offset)
        {
            Vector3 targetPos = position;
            return Quaternion.Euler(0, 0, Mathf.Atan2(targetPos.y - current.y, targetPos.x - current.x) * Mathf.Rad2Deg + Offset);
        }

        /// <summary>Faces the position (in 2D space)</summary>
        /// <param name="transform">The transform to rotate</param>
        /// <param name="position">Position to face to</param>
        public static void FacePosition2D(this Transform transform, Vector3 position)
        {
            transform.rotation = FacePosition2D(transform.position, position);
        }

        /// <summary>Faces the position (in 2D space)</summary>
        /// <param name="transform">The transform to rotate</param>
        /// <param name="position">Position to face to</param>
        /// <param name="current">The offset of the rotation</param>
        /// <param name="Offset">The offset of the rotation</param>
        public static void FacePosition2D(this Transform transform, Vector3 position, float Offset)
        {
            transform.rotation = FacePosition2D(transform.position, position, Offset);
        }
    }

    /// <summary>The percentage of the mouse, where the left would be -1 and the right would be 1</summary>
    public static class MouseScreenPercentage
    {
        /// <summary>The percentage of the mouse, where the left would be -1 and the right would be 1</summary>
        public static Vector2 MouseByScreen
        {
            get
            {
                float x = Input.mousePosition.x / Screen.width;
                float y = Input.mousePosition.y / Screen.height;

                //X,Y is between 0 and 1, which has a difference of 1. We want it to be from -1 to 1 so it needs to have a difference of 2
                //To achieve this, we first times it by 2. This is to get it from 0 - 2
                //We can then, simply subtract one to get -1 to 1
                return (new Vector2(x, y) * 2) - (Vector2.one);
            }
        }
    }

    /// <summary>Useful utilities for arrays</summary>
    public static class ArrayUtils
    {
        /// <summary>Creates a new array, with each index set to a default value</summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="length">Length of the array</param>
        /// <returns>a new array based off of the template</returns>
        public static T[] NewArrayOfType<T>(int length)
        {
            T[] returnVal = new T[length];

            for (int i = 0; i < returnVal.Length; i++)
            {
                returnVal[i] = default;
            }
            return returnVal;
        }

        /// <summary>Creates a new array based off of the template</summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="template">Template to copy from</param>
        /// <param name="length">Length of the array</param>
        /// <returns>a new array based off of the template</returns>
        public static T[] NewArrayOfType<T>(this T template, int length)
        {
            T[] returnVal = new T[length];

            for (int i = 0; i < returnVal.Length; i++)
            {
                returnVal[i] = template;
            }
            return returnVal;
        }

        /// <summary>Gets the sum of the int array</summary>
        /// <param name="numbers">the int array to sum</param>
        /// <returns>the sum of the int array</returns>
        public static int Sum(this int[] numbers)
        {
            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        /// <summary>Flips a string</summary>
        /// <param name="s">The string to flip</param>
        /// <returns>The flipped string</returns>
        public static string Flipped(this string s)
        {
            string newString = "";

            //A reverse for loop
            for (int i = s.Length - 1; i >= 0; i--)
            {
                newString += s[i];
            }

            return newString;
        }
    }
}