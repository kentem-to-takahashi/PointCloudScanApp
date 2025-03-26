using System.IO;
using UnityEngine;

namespace Bonus
{
    public class PointCloudSaver : MonoBehaviour
    {
        // PointCloudScannerをインスペクターから設定できるようにする
        [SerializeField]
        private PointCloudScanner pointCloudScanner;

        private static readonly string FILE_NAME = "/pointcloud.txt";

        public void Save()
        {
            // 点群の位置情報を取得する
            var points = pointCloudScanner.Points;

            // 点群の位置情報をファイルに保存する
            var filePath = Application.persistentDataPath + FILE_NAME;
            using (var writer = File.CreateText(filePath))
            {
                foreach (var point in points)
                {
                    writer.WriteLine(point.x + "," + point.y + "," + point.z);
                }
            }

            Debug.Log("Saved point cloud to " + filePath);
        }
    }
}
