using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace Zenject
{
    public class SampleSceneSwitcher : MonoBehaviour
    {
        static readonly string[] SceneNames = new string[]
        {
            "Asteroids",
            "SpaceFighter",
        };

        public void Update()
        {
            // This is just used to make it easier to test builds
            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl))
            {
                var currentSceneName = SceneManager.GetActiveScene().name;
                var otherSceneName = SceneNames.First(x => x != currentSceneName);

                SceneManager.LoadScene(otherSceneName);
            }
        }
    }
}
