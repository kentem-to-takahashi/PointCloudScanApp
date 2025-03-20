using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PointCloudScanner : MonoBehaviour
{
    [SerializeField]
    private ARPointCloudManager pointCloudManager;

    private void Awake()
    {
        pointCloudManager.enabled = false;
    }

    public void StartScan()
    {
        pointCloudManager.enabled = true;
    }

    public void StopScan()
    {
        pointCloudManager.enabled = false;
    }
}
