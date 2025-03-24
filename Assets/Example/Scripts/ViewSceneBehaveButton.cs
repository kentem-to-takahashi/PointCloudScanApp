using UnityEngine;
using UnityEngine.UI;

namespace Example
{
    public class ViewSceneBehaveButton : MonoBehaviour
    {
        [SerializeField]
        private SceneBehaviour sceneBehaviour;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            sceneBehaviour.LoadViewScene();
        }
    }
}
