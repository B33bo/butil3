using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using b33bo.utils;

namespace b33bo.numerics
{
    /// <summary>Better rounding, it lets you specify the round uncertainty</summary>
    public static class Round
    {
        /// <summary>Rounds the input</summary>
        /// <param name="Input">The thing to round</param>
        /// <param name="size">The uncertainty of rounding</param>
        /// <returns>the input, rounded to the uncertainty. E.G: 85 to the nearest 10 = 100</returns>
        public static float Float(float Input, float size)
        {
            return Mathf.Round(Input / size) * size;
        }

        /// <summary>Rounds the input</summary>
        /// <param name="Input">The thing to round</param>
        /// <param name="size">The uncertainty of rounding</param>
        /// <returns>the input, rounded to the uncertainty. E.G: 85 to the nearest 10 = 100</returns>
        public static Vector2 Vector2(Vector2 Input, float size)
        {
            return new Vector2(Float(Input.x, size),
                               Float(Input.y, size));
        }

        /// <summary>Rounds the input</summary>
        /// <param name="Input">The thing to round</param>
        /// <param name="size">The uncertainty of rounding</param>
        /// <returns>the input, rounded to the uncertainty. E.G: 85 to the nearest 10 = 100</returns>
        public static Vector3 Vector3(Vector3 Input, float size)
        {
            return new Vector3(Float(Input.x, size),
                               Float(Input.y, size),
                               Float(Input.z, size));
        }

        /// <summary>Rounds the input, in euler angles</summary>
        /// <param name="Input">The thing to round</param>
        /// <param name="size">The uncertainty of rounding</param>
        /// <returns>the input, rounded to the uncertainty. E.G: 85 to the nearest 10 = 100</returns>
        public static Quaternion Quaternion(Quaternion Input, float size)
        {
            return UnityEngine.Quaternion.Euler(Vector3(Input.eulerAngles, size));
        }
    }

    /// <summary>Better flooring, it lets you specify the round uncertainty</summary>
    public static class Floor
    {
        /// <summary>Floors the input</summary>
        /// <param name="Input">The thing to floor</param>
        /// <param name="size">The uncertainty of flooring</param>
        /// <returns>the input, floored to the uncertainty. E.G: 85 to the lowest 10 = 80</returns>
        public static float Float(float Input, float size)
        {
            return Mathf.Floor(Input / size) * size;
        }

        /// <summary>Floors the input</summary>
        /// <param name="Input">The thing to floor</param>
        /// <param name="size">The uncertainty of flooring</param>
        /// <returns>the input, floored to the uncertainty. E.G: 85 to the lowest 10 = 80</returns>
        public static Vector2 Vector2(Vector2 Input, float size)
        {
            return new Vector2(Float(Input.x, size),
                               Float(Input.y, size));
        }

        /// <summary>Floors the input</summary>
        /// <param name="Input">The thing to floor</param>
        /// <param name="size">The uncertainty of flooring</param>
        /// <returns>the input, floored to the uncertainty. E.G: 85 to the lowest 10 = 80</returns>
        public static Vector3 Vector3(Vector3 Input, float size)
        {
            return new Vector3(Float(Input.x, size),
                               Float(Input.y, size),
                               Float(Input.z, size));
        }

        /// <summary>Floors the input, as euler angles</summary>
        /// <param name="Input">The thing to floor</param>
        /// <param name="size">The uncertainty of flooring</param>
        /// <returns>the input, floored to the uncertainty. E.G: 85 to the lowest 10 = 80</returns>
        public static Quaternion Quaternion(Quaternion Input, float size)
        {
            return UnityEngine.Quaternion.Euler(Vector3(Input.eulerAngles, size));
        }
    }

    /// <summary>Better ceiling, it lets you specify the round uncertainty</summary>
    public static class Ceil
    {
        /// <summary>Gets the ceiling of the input</summary>
        /// <param name="Input">The thing to ceil</param>
        /// <param name="size">The uncertainty of flooring</param>
        /// <returns>the input, ceiled to the uncertainty. E.G: 84 to the highest 10 = 90</returns>
        public static float Float(float Input, float size)
        {
            return Mathf.Ceil(Input / size) * size;
        }

        /// <summary>Gets the ceiling of the input</summary>
        /// <param name="Input">The thing to ceil</param>
        /// <param name="size">The uncertainty of flooring</param>
        /// <returns>the input, ceiled to the uncertainty. E.G: 84 to the highest 10 = 90</returns>
        public static Vector2 Vector2(Vector2 Input, float size)
        {
            return new Vector2(Float(Input.x, size),
                               Float(Input.y, size));
        }

        /// <summary>Gets the ceiling of the input</summary>
        /// <param name="Input">The thing to ceil</param>
        /// <param name="size">The uncertainty of flooring</param>
        /// <returns>the input, ceiled to the uncertainty. E.G: 84 to the highest 10 = 90</returns>
        public static Vector3 Vector3(Vector3 Input, float size)
        {
            return new Vector3(Float(Input.x, size),
                               Float(Input.y, size),
                               Float(Input.z, size));
        }

        /// <summary>Gets the ceiling of the input, in euler angles</summary>
        /// <param name="Input">The thing to ceil</param>
        /// <param name="size">The uncertainty of flooring</param>
        /// <returns>the input, ceiled to the uncertainty. E.G: 84 to the highest 10 = 90</returns>
        public static Quaternion Quaternion(Quaternion Input, float size)
        {
            return UnityEngine.Quaternion.Euler(Vector3(Input.eulerAngles, size));
        }
    }

    /// <summary>Useful randomizer functions</summary>
    public static class Randomizer
    {
        /// <summary>Get a random item, where one item can be more likely than another</summary>
        /// <param name="weights">the weights of each item.</param>
        /// <returns>Picks a random number from the weight table, and returns the index</returns>
        public static int GetIndexFromWeight(int[] weights)
        {
            int randomNumber = Random.Range(0, weights.Sum());
            int weightTotalCurrent = 0;

            //Works by picking a random number from the *sum* of the weights,
            //then loops through them all and has a number
            //each time the loop iterates, the number increases by the current weight
            //If the total is higher than the sum, it's found the index

            for (int i = 0; i < weights.Length; i++)
            {
                //If the weight current is higher than the random number, you have found it.
                if (weightTotalCurrent >= randomNumber)
                    return i - 1;

                //Counts how far it has gone
                weightTotalCurrent += weights[i];
            }

            //Incase it finds it right at the end
            if (weightTotalCurrent >= randomNumber)
                return weights.Length-1;

            //There was some kind of error
            return -1;
        }

        static string letters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPPQqRrSsTtUuVvWwXxYyZz1234567890!$%#@";
        public static string String() => String(Random.Range(2, 15), letters);

        public static string String(int length) => String(length, letters);

        public static string String(string letters) => String(Random.Range(2, 15), letters);

        public static string String(int length, string letters)
        {
            string newString = "";

            for (int i = 0; i < length; i++)
                newString += Choice(letters);

            return newString;
        }

        public static char Choice(string array) => array[Random.Range(0, array.Length)];
        public static T Choice<T>(T[] array) => array[Random.Range(0, array.Length)];
        public static T Choice<T>(List<T> array) => array[Random.Range(0, array.Count)];
    }

    /// <summary>Clamp functions, with Vectors</summary>
    public static class Clamp
    {
        static Rect canvasRect;
        /// <summary>The clamped vector</summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The smallest allowed value</param>
        /// <param name="max">The largest allowed value</param>
        /// <returns>The clamped vector</returns>
        public static Vector3 Vector3(Vector3 value, Vector3 min, Vector3 max)
        {
            float x = Mathf.Clamp(value.x, min.x, max.x);
            float y = Mathf.Clamp(value.y, min.y, max.y);
            return new Vector3(x, y);
        }

        /// <summary>The vector, clamped to be inside the rect</summary>
        /// <param name="value">Vector to clamp</param>
        /// <param name="rect">Rect that the vector must be inside</param>
        /// <returns>The vector, clamped to be inside the rect</returns>
        public static Vector3 Rect(Vector3 value, Rect rect)
        {
            float x = Mathf.Clamp(value.x, rect.xMin, rect.xMax);
            float y = Mathf.Clamp(value.y, rect.yMin, rect.yMax);

            return new Vector3(x, y);
        }

        /// <summary>
        /// The vector, clamped to be inside the rect
        /// </summary>
        /// <param name="position"></param>
        /// <param name="value"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Vector3 Rect(Vector3 value, Rect rectOfValue, Rect rect)
        {
            //The furthest left it can be, with the anchor point at the middle, is the radius so it doesn't go halfway offscreen
            //The furthest right is also the radius, but to clamp it properly, you need to get the total canvas width - radius

            Vector2 radius = new Vector2(rectOfValue.width, rectOfValue.height) / 2;

            value.x = Mathf.Clamp(value.x, radius.x, (rect.width - radius.x));
            value.y = Mathf.Clamp(value.y, radius.y, (rect.height - radius.y));

            return value;
        }

        public static Vector3 Canvas(Vector3 value, Rect rectOfValue, Canvas canvas)
        {
            float scaleFactor = canvas.scaleFactor;

            Rect canvasToUse;
            if (canvas.isRootCanvas)
            {
                if (canvasRect == null || canvasRect.width == 0)
                    canvasRect = canvas.GetComponent<RectTransform>().rect;

                canvasToUse = canvasRect;
            }
            else
            {
                canvasToUse = canvas.GetComponent<RectTransform>().rect;
            }

            Vector2 radius = new Vector2(rectOfValue.width, rectOfValue.height) / 2;

            value.x = Mathf.Clamp(value.x, radius.x * scaleFactor, (canvasToUse.width - radius.x) * scaleFactor);
            value.y = Mathf.Clamp(value.y, radius.y * scaleFactor, (canvasToUse.height - radius.y) * scaleFactor);

            return value;
        }
    }
}
