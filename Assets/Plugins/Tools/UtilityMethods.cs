using UnityEditor;
using UnityEngine;

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
    }


}
