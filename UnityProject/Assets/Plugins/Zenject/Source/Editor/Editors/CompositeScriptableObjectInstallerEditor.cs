using UnityEditor;
using Zenject;

namespace Zenject
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CompositeScriptableObjectInstaller))]
    public class CompositeScriptableObjectInstallerEditor : BaseCompositetInstallerEditor<CompositeScriptableObjectInstaller, ScriptableObjectInstallerBase>
    {
    }
}
