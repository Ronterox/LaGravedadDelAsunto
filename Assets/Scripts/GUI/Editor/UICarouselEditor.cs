using DG.Tweening;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace GUI.Editor
{
    [CustomEditor(typeof(UICarousel), true), CanEditMultipleObjects]
    public class UICarouselEditor : ScrollRectEditor
    {
        private SerializedProperty m_ElementPrefab;
        private SerializedProperty m_ScrollMask;

        private UICarousel carousel;

        protected override void OnEnable()
        {
            base.OnEnable();

            carousel = target as UICarousel;

            m_ElementPrefab = serializedObject.FindProperty("ElementPrefab");
            m_ScrollMask = serializedObject.FindProperty("ScrollMask");
        }

        public override void OnInspectorGUI()
        {
            var header = new GUIStyle(EditorStyles.boldLabel) { fontSize = 14 };

            GUILayout.Space(5);
            GUILayout.Label("Scroll Rect Behaviour", header);
            base.OnInspectorGUI();

            GUILayout.Label("Carousel", header);
            EditorGUILayout.PropertyField(m_ElementPrefab, new GUIContent("Element Prefab"));
            EditorGUILayout.PropertyField(m_ScrollMask, new GUIContent("Scroll Mask"));

            GUILayout.Space(5);
            GUILayout.Label("Animation", EditorStyles.boldLabel);
            carousel.ScrollSpeed = EditorGUILayout.FloatField("Scroll Speed", carousel.ScrollSpeed);
            carousel.ScrollEase = (Ease)EditorGUILayout.EnumPopup("Scroll Ease", carousel.ScrollEase);

            carousel.ScrollSpeed = Mathf.Max(carousel.ScrollSpeed, 0.01f);

            serializedObject.ApplyModifiedProperties();
        }

        protected override void OnDisable()
        {
            if (carousel) Undo.RecordObject(carousel, "UICarousel changed");
            base.OnDisable();
        }
    }
}
