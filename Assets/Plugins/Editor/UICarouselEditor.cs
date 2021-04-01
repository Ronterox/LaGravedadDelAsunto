using DG.Tweening;
using Plugins.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Plugins.Editor
{
    [CustomEditor(typeof(UICarousel)), CanEditMultipleObjects]
    public class UICarouselEditor : ScrollRectEditor
    {
        private SerializedProperty m_ElementPrefab;
        private SerializedProperty m_ScrollMask;

        private UICarousel carousel;

        protected override void OnEnable()
        {
            base.OnEnable();

            carousel = target as UICarousel;

            m_ElementPrefab = serializedObject.FindProperty("elementPrefab");
            m_ScrollMask = serializedObject.FindProperty("scrollMask");
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
            carousel.scrollSpeed = EditorGUILayout.FloatField("Scroll Speed", carousel.scrollSpeed);
            carousel.scrollEase = (Ease)EditorGUILayout.EnumPopup("Scroll Ease", carousel.scrollEase);

            carousel.scrollSpeed = Mathf.Max(carousel.scrollSpeed, 0.01f);

            serializedObject.ApplyModifiedProperties();
        }

        protected override void OnDisable()
        {
            if (carousel) Undo.RecordObject(carousel, "UICarousel changed");
            base.OnDisable();
        }
    }
}
