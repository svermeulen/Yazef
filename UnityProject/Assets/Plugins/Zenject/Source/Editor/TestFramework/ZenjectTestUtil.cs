using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zenject.Internal
{
    public static class ZenjectTestUtil
    {
        static string unitTestRunnerGameObjectName;
        public static string UnitTestRunnerGameObjectName
        {
            get
            {
                if (unitTestRunnerGameObjectName != null)
                {
                    return unitTestRunnerGameObjectName;
                }

                // Unity Test Framework version 1.x and 2.x use different names
                // for the test runner. Since there is no way of knowing which
                // version that's used at compile time we have to brute force
                // both versions the first time.
                unitTestRunnerGameObjectName =
                    GameObject.Find("Code-based tests runner")?.name ?? // v1
                    GameObject.Find("tests runner")?.name;              // v2

                return unitTestRunnerGameObjectName;
            }
        }

        public static void DestroyEverythingExceptTestRunner(bool immediate)
        {
            var testRunner = GameObject.Find(UnitTestRunnerGameObjectName);
            Assert.IsNotNull(testRunner);
            GameObject.DontDestroyOnLoad(testRunner);

            // We want to clear all objects across all scenes to ensure the next test is not affected
            // at all by previous tests
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                foreach (var obj in SceneManager.GetSceneAt(i).GetRootGameObjects())
                {
                    if (obj.name != UnitTestRunnerGameObjectName)
                    {
                        GameObject.DestroyImmediate(obj);
                    }
                }
            }

            if (ProjectContext.HasInstance)
            {
                var dontDestroyOnLoadRoots = ProjectContext.Instance.gameObject.scene
                    .GetRootGameObjects();

                foreach (var rootObj in dontDestroyOnLoadRoots)
                {
                    if (rootObj.name != UnitTestRunnerGameObjectName)
                    {
                        if (immediate)
                        {
                            GameObject.DestroyImmediate(rootObj);
                        }
                        else
                        {
                            GameObject.Destroy(rootObj);
                        }
                    }
                }
            }
        }
    }
}
