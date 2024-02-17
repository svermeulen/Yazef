using UnityEditor;
using Zenject;

namespace Zenject
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CompositeMonoInstaller))]
    public class CompositeMonoInstallerEditor : BaseCompositetInstallerEditor<CompositeMonoInstaller, MonoInstallerBase>
    {
    }
}
