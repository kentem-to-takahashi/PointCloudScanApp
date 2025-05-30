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
            // Toggleコンポーネントを取得する
            toggle = GetComponent<Toggle>();
        }

        private void Start()
        {
            // イベントを登録する
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnDestroy()
        {
            // イベントを解除する
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool isOn)
        {
            // トグルがオンのときはスキャンを開始し、オフのときはスキャンを停止する
            if (isOn)
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
