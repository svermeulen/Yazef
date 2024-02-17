#if !ODIN_INSPECTOR

using UnityEditor;

namespace Zenject
{
    [CustomEditor(typeof(ProjectContext))]
    public class ProjectContextEditor : ContextEditor
    {
        SerializedProperty _settingsProperty;
        SerializedProperty _parentNewObjectsUnderContextProperty;
        SerializedProperty _autoInjectInHierarchyProperty;

        public override void OnEnable()
        {
            base.OnEnable();

            _settingsProperty = serializedObject.FindProperty("_settings");
            _parentNewObjectsUnderContextProperty = serializedObject.FindProperty("_parentNewObjectsUnderContext");
            _autoInjectInHierarchyProperty = serializedObject.FindProperty("_autoInjectInHierarchy");
        }

        protected override void OnGui()
        {
            base.OnGui();

            EditorGUILayout.PropertyField(_settingsProperty, true);
            EditorGUILayout.PropertyField(_parentNewObjectsUnderContextProperty);
            EditorGUILayout.PropertyField(_autoInjectInHierarchyProperty);
        }
    }
}

#endif
