using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Step1
{
    public class PointCloudScanner : MonoBehaviour
    {
        [SerializeField]
        private ARPointCloudManager pointCloudManager;

        private void Awake()
        {
            // はじめはスキャンを停止しておく
            pointCloudManager.enabled = false;
        }

        public void StartScan()
        {
            // スキャンを開始する
            pointCloudManager.enabled = true;
        }

        public void StopScan()
        {
            // スキャンを停止する
            pointCloudManager.enabled = false;
        }
    }
}
