using UnityEngine;
using UnityEngine.UI;

namespace Example
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField]
        private PointCloudSaver pointCloudSaver;

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

        public void OnClick()
        {
            pointCloudSaver.Save();
        }
    }
}
