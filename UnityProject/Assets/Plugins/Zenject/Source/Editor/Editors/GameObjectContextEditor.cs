#if !ODIN_INSPECTOR

using UnityEditor;

namespace Zenject
{
    [CustomEditor(typeof(GameObjectContext))]
    public class GameObjectContextEditor : RunnableContextEditor
    {
        SerializedProperty _kernel;
        SerializedProperty _autoInjectInHierarchy;

        public override void OnEnable()
        {
            base.OnEnable();

            _kernel = serializedObject.FindProperty("_kernel");
            _autoInjectInHierarchy = serializedObject.FindProperty("_autoInjectInHierarchy");
        }

        protected override void OnGui()
        {
            base.OnGui();

            EditorGUILayout.PropertyField(_kernel);
            EditorGUILayout.PropertyField(_autoInjectInHierarchy);
        }
    }
}

#endif
