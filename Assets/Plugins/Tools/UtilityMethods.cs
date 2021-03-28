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
        public static float ToRadians(this float degrees) => Mathf.Rad2Deg * degrees;

        public static string ToColorString(this string sentence, string color) => $"<color={color}>{sentence}</color>";

        public static Vector3 GetRandomDirection(bool horizontalDirections, bool verticalDirections) => Random.Range(horizontalDirections ? 0 : 4, verticalDirections ? 6 : 4) switch { 0 => Vector3.forward, 1 => Vector3.back, 2 => Vector3.left, 3 => Vector3.right, 4 => Vector3.up, 5 => Vector3.down, _ => Vector3.zero };
    }


}
