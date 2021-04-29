using UnityEditor;

namespace Plugins.Persistence.Editor
{
    public abstract class DataPersisterEditor : UnityEditor.Editor
    {
        protected IDataPersister m_DataPersister;

        private SerializedProperty m_Settings;

        void OnEnable()
        {
            m_DataPersister = (IDataPersister)target;

            m_Settings = serializedObject.FindProperty("DataSettings");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Persistence", EditorStyles.boldLabel);

            DataPersisterGUI();
        }

        public void DataPersisterGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Settings.FindPropertyRelative("type"));
            if (m_DataPersister.GetDataSettings().type != DataSettings.PersistenceType.DoNotPersist)
            {
                EditorGUILayout.PropertyField(m_Settings.FindPropertyRelative("dataId"));
            } 

            serializedObject.ApplyModifiedProperties();
        }
    }
}