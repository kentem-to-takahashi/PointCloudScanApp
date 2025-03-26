using UnityEngine;
using UnityEngine.UI;

namespace Step2
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField]
        private PointCloudSaver pointCloudSaver;

        private Button button;

        private void Awake()
        {
            // Buttonコンポーネントを取得する
            button = GetComponent<Button>();
        }

        private void Start()
        {
            // ボタンがクリックされたときのイベントを登録する
            button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            // イベントを解除する
            button.onClick.RemoveListener(OnClick);
        }

        public void OnClick()
        {
            // PointCloudSaverのSaveメソッドを呼び出す
            pointCloudSaver.Save();
        }
    }
}
