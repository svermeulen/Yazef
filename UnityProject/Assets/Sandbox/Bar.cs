
using UnityEngine;
using Zenject;

namespace Svkj
{
    public class Bar : MonoBehaviour
    {
        [Inject]
        public void Construct(Foo foo)
        {
            Debug.LogFormat("Bar Constructed with foo {0}", foo);
        }
    }
}
