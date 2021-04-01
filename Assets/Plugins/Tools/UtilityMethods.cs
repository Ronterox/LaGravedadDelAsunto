using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Plugins.Tools
{
    /// <summary>
    /// A class with useful methods for every type of special case
    /// </summary>
    public static class UtilityMethods
    {
        /// <summary>
        /// Activates or deactivates the children the gameObject
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="active">Whether to activate or deactivate its children</param>
        public static void SetActiveChildren(this GameObject parent, bool active = true)
        {
            foreach (Transform child in parent.GetComponentsInChildren<Transform>(active)) child.gameObject.SetActive(active);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Instantiates an actual prefab instead of a gameObject
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        public static GameObject InstantiatePrefab(GameObject prefab, Vector2 position, Quaternion rotation,
            Transform parent = null)
        {
            if (!(PrefabUtility.InstantiatePrefab(prefab) is GameObject result)) return null;
            result.transform.position = position;
            result.transform.parent = parent;
            result.transform.rotation = rotation;
            return result;
        }
#endif
        /// <summary>
        /// Returns the degrees as radians
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float ToRadians(this float degrees) => Mathf.Rad2Deg * degrees;

        /// <summary>
        /// Returns the string as a smart color string
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToColorString(this string sentence, string color) => $"<color={color}>{sentence}</color>";

        /// <summary>
        /// Returns a random direction Vector3
        /// </summary>
        /// <param name="horizontalDirections"></param>
        /// <param name="verticalDirections"></param>
        /// <returns></returns>
        public static Vector3 GetRandomDirection(bool horizontalDirections, bool verticalDirections) => Random.Range(horizontalDirections ? 0 : 4, verticalDirections ? 6 : 4) switch { 0 => Vector3.forward, 1 => Vector3.back, 2 => Vector3.left, 3 => Vector3.right, 4 => Vector3.up, 5 => Vector3.down, _ => Vector3.zero };

        /// <summary>
        /// Returns the float, as a percentage of the max value, from 0 to 1;
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static float GetPercentageValue(this float value, float maxValue) => value / maxValue;

        /// <summary>
        /// Returns a random vector2 with points between the values passed
        /// </summary>
        /// <param name="minValueX"></param>
        /// <param name="maxValueX"></param>
        /// <param name="minValueY"></param>
        /// <param name="maxValueY"></param>
        /// <returns></returns>
        public static Vector2 GetRandomVector2(float minValueX, float maxValueX, float minValueY, float maxValueY) => new Vector2(Random.Range(minValueX, maxValueX), Random.Range(minValueY, maxValueY));

        /// <summary>
        /// Changes the value by a limited amount
        /// </summary>
        /// <param name="value"></param>
        /// <param name="increment">Positive or negative value to be incremented or decremented with</param>
        /// <param name="byLimit">the max amount to be limited to</param>
        public static void ChangeValueLimited(this ref float value, float increment, float byLimit) => value = (value + increment) % byLimit;
        
        /// <summary>
        /// Changes the value by a limited amount
        /// </summary>
        /// <param name="value"></param>
        /// <param name="increment">Positive or negative value to be incremented or decremented with</param>
        /// <param name="byLimit">the max amount to be limited to</param>
        public static void ChangeValueLimited(this ref int value, int increment, int byLimit) => value = (value + increment) % byLimit;

        /// <summary>
        /// Coroutine standard cycle function to be reused
        /// </summary>
        /// <param name="whileCondition"></param>
        /// <param name="loopWaitForSeconds"></param>
        /// <param name="loopAction"></param>
        /// <param name="onceStartCoroutine"></param>
        /// <param name="onceFinishCoroutine"></param>
        /// <returns></returns>
        public static IEnumerator FunctionCycleCoroutine(Func<bool> whileCondition, Action loopAction, WaitForSeconds loopWaitForSeconds = null, Action onceStartCoroutine = null, Action onceFinishCoroutine = null)
        {
            onceStartCoroutine?.Invoke();
            while (whileCondition())
            {
                loopAction?.Invoke();
                yield return loopWaitForSeconds;
            }
            onceFinishCoroutine?.Invoke();
        }

        /// <summary>
        /// Checks if a float approximates other float by a tolerance
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objective"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool Approximates(this float value, float objective, float tolerance = 0.01f) => Math.Abs(value - objective) <= tolerance;

        
        /// <summary>
        /// Checks if a vector3 approximates other vector3 by a tolerance
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objective"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool Approximates(this Vector3 value, Vector3 objective, float tolerance = 0.01f) => Vector3.SqrMagnitude(value - objective) <= tolerance;

        /// <summary>
        /// Lerps a vector3
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objective"></param>
        /// <param name="speed">Between 0 and 1</param>
        public static void Lerp(this ref Vector3 value, Vector3 objective, float speed) => value = Vector3.Lerp(value, objective, speed);

        /// <summary>
        /// Lerps a float
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objective"></param>
        /// <param name="speed">Between 0 and 1</param>
        public static void Lerp(this ref float value, float objective, float speed) => value = Mathf.Lerp(value, objective, speed);
    }


}
