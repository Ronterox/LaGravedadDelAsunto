using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace GUI.Editor
{
    [CustomEditor(typeof(UICarouselElement)), CanEditMultipleObjects]
    public class UICarouselElementEditor : ButtonEditor
    {
        private SerializedProperty m_SelectAnim;
        private SerializedProperty m_DeselectAnim;

        private const string SELECT_ANIMATION_TEXT = "Select Animation";
        private const string DESELECT_ANIMATION_TEXT = "Deselect Animation";

        protected override void OnEnable()
        {
            base.OnEnable();

            m_SelectAnim = serializedObject.FindProperty("selectAnim");
            m_DeselectAnim = serializedObject.FindProperty("deselectAnim");
        }

        public override void OnInspectorGUI()
        {
            var header = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 14
            };

            GUILayout.Space(5);
            GUILayout.Label("Button Behaviour", header);
            base.OnInspectorGUI();

            GUILayout.Label("Animations", header);
            EditorGUILayout.PropertyField(m_SelectAnim, new GUIContent(SELECT_ANIMATION_TEXT));
            EditorGUILayout.PropertyField(m_DeselectAnim, new GUIContent(DESELECT_ANIMATION_TEXT));

            serializedObject.ApplyModifiedProperties();
        }

        protected override void OnDisable()
        {
            var element = (UICarouselElement)target;

            if (element)
                Undo.RecordObject(element, "UICarouselElement changed");

            base.OnDisable();
        }
    } 
}
