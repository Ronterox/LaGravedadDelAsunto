using GUI.Minigames.Cook_Plate;
using Plugins.Editor;
using UnityEditor;
using UnityEngine;

namespace GUI.Editor
{
    [CustomEditor(typeof(PlatesMenuElement))]
    public class PlatesMenuElementEditor : UICarouselElementEditor
    {
        private SerializedProperty m_Plate, m_PlateImage, m_PlateName;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Plate = serializedObject.FindProperty("plate");
            m_PlateImage = serializedObject.FindProperty("plateImage");
            m_PlateName = serializedObject.FindProperty("plateName");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(10);
            DrawProperty(m_Plate, "Plate Item");
            DrawProperty(m_PlateImage, "Plate Image");
            DrawProperty(m_PlateName, "Plate Name");
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawProperty(SerializedProperty property, string textName) => EditorGUILayout.PropertyField(property, new GUIContent(textName));
    }
}
