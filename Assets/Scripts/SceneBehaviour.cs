using UnityEngine;
using UnityEngine.SceneManagement;

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
