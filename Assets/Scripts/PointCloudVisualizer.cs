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

    private void DisplayPointCloud()
    {
        // パーティクルシステムを取得する
        var particleSystem = GetComponent<ParticleSystem>();

        var points = pointCloudFileReader.Points;
        var numParticles = points.Length;

        // パーティクルの配列を作成して点群の位置情報を設定する
        particles = new ParticleSystem.Particle[numParticles];
        for (var i = 0; i < numParticles; i++)
        {
            particles[i].startColor = particleSystem.main.startColor.color;
            particles[i].startSize = particleSystem.main.startSize.constant;
            particles[i].position = points[i];
            particles[i].remainingLifetime = 1f;
        }

        // パーティクルシステムにパーティクルの情報を設定する
        particleSystem.SetParticles(particles, numParticles);
    }
}
