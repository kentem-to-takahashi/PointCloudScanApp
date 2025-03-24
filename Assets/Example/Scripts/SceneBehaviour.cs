using UnityEngine;
using UnityEngine.SceneManagement;

namespace Example
{
    public class SceneBehaviour : MonoBehaviour
    {
        public void LoadViewScene()
        {
            SceneManager.LoadScene("ViewScene");
        }

        public void LoadARScene()
        {
            SceneManager.LoadScene("ARScene");
        }
    }
}
