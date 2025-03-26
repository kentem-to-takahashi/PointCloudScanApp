using UnityEngine;
using UnityEngine.UI;

public class ScanToggle : MonoBehaviour
{
    // PointCloudScannerをインスペクターから設定できるようにする


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

        }
        else
        {
            
        }
    }
}
