using Plugins.Editor;
using UnityEditor;
using UnityEngine;

namespace GUI.Editor
{
    [CustomEditor(typeof(PlatesMenu))]
    public class PlatesMenuEditor : UICarouselEditor
    {
        private SerializedProperty m_Items;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Items = serializedObject.FindProperty("plates");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(m_Items, new GUIContent("Items Array"));
        }
    }
}
