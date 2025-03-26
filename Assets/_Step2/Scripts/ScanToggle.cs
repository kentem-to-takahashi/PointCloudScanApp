using UnityEngine;
using UnityEngine.UI;

namespace Step2
{
    public class ScanToggle : MonoBehaviour
    {
        // PointCloudScannerをインスペクターから設定できるようにする
        [SerializeField]
        private PointCloudScanner pointCloudScanner;

        private Toggle toggle;

        private void Awake()
        {
            // Toggleコンポーネントを取得して、値が変更されたときのイベントを登録する
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnDestroy()
        {
            // イベントを解除する
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool isToggled)
        {
            // トグルがオンのときはスキャンを開始し、オフのときはスキャンを停止する
            if (isToggled)
            {
                pointCloudScanner.StartScan();
            }
            else
            {
                pointCloudScanner.StopScan();
            }
        }
    }
}
