using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PointCloudScanner : MonoBehaviour
{
    [SerializeField]
    private ARPointCloudManager pointCloudManager;

    public void StartScan()
    {
        pointCloudManager.enabled = true;
    }

    public void StopScan()
    {
        pointCloudManager.enabled = false;
    }
}
