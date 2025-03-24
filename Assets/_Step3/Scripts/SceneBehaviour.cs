using UnityEngine;
using UnityEngine.SceneManagement;

namespace Step3
{
    public class SceneBehaviour : MonoBehaviour
    {
        public void LoadViewScene()
        {
            SceneManager.LoadScene("ViewScene");
        }
    }
}
