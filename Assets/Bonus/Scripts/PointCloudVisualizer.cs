using UnityEngine;

namespace Bonus
{
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
            var particleSystem = GetComponent<ParticleSystem>();

            // 点群ファイルリーダーから点群の位置情報を取得する
            var points = pointCloudFileReader.Points;

            // パーティクルの配列を作成して点群の位置情報を設定する
            var numParticles = points.Length;
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

        /// <summary>
        /// パーティクルの境界（Bounds）を計算するメソッド
        /// </summary>
        /// <returns>点群データの境界を表す Bounds</returns>
        public Bounds GetCalculateBounds()
        {
            if (particles == null || particles.Length == 0)
            {
                throw new System.InvalidOperationException("particles is null or empty.");
            }

            Vector3 min = particles[0].position;
            Vector3 max = particles[0].position;

            foreach (var particle in particles)
            {
                min = Vector3.Min(min, particle.position);
                max = Vector3.Max(max, particle.position);
            }

            Vector3 center = (min + max) / 2;
            Vector3 size = max - min;

            return new Bounds(center, size);
        }
    }
}