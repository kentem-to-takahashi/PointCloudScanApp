using UnityEngine;

namespace Bonus
{
    public class FitCameraButton : MonoBehaviour
    {
        [SerializeField]
        private CinemachineAutoFramer _cinemachineAutoFramer;

        void Start()
        {
            var button = GetComponent<UnityEngine.UI.Button>();
            //ボタンがクリックされた時の処理を登録
            button.onClick.AddListener(OnClick);

        }
        private void OnClick()
        {
            //フィットカメラに切り替える
            StartCoroutine(_cinemachineAutoFramer.SwitchToFitCamera());
        }
    }
}