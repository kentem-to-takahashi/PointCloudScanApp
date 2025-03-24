using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bonus
{
    public class SceneBehaviour : MonoBehaviour
    {
        public void LoadViewScene()
        {
            SceneManager.LoadScene("ViewScene");
        }
    }
}
