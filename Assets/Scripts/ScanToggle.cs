using UnityEngine;
using UnityEngine.UI;

public class ScanToggle : MonoBehaviour
{
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
