using UnityEngine;

public class PointCloudVisualizer : MonoBehaviour
{
    [SerializeField]
    private PointCloudFileReader pointCloudFileReader;

    private ParticleSystem.Particle[] particles;

    private void Awake()
    {
        pointCloudFileReader.ReadedFile += DisplayPointCloud;
    }

    private void OnDestroy()
    {
        pointCloudFileReader.ReadedFile -= DisplayPointCloud;
    }

    private void DisplayPointCloud()
    {
        // パーティクルシステムを取得する

        // 点群ファイルリーダーから点群の位置情報を取得する

        // パーティクルの配列を作成して点群の位置情報を設定する

        // パーティクルシステムにパーティクルの情報を設定する
    }
}
