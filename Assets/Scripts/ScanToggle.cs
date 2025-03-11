using UnityEngine;
using UnityEngine.UI;

public class ScanToggle : MonoBehaviour
{
    [SerializeField]
    private PointCloudScanner pointCloudScanner;

    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isToggled)
    {
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
