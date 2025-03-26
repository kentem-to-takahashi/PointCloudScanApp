using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Step2
{
    public class PointCloudScanner : MonoBehaviour
    {
        [SerializeField]
        private ARPointCloudManager pointCloudManager;

        private Dictionary<ulong, Vector3> points = new Dictionary<ulong, Vector3>();

        public Vector3[] Points
        {
            get
            {
                return points.Values.ToArray();
            }
        }

        private void Awake()
        {
            // はじめはスキャンを停止しておく
            pointCloudManager.enabled = false;
        }

        private void OnDestroy()
        {
            // GameObjectが破棄されるときにARPointCloudManagerの更新イベントを解除する
            pointCloudManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
        }

        public void StartScan()
        {
            // スキャンを開始する
            pointCloudManager.enabled = true;

            // ARPointCloudManagerの更新イベントを登録する
            pointCloudManager.trackablesChanged.AddListener(OnTrackablesChanged);
        }

        public void StopScan()
        {
            // ARPointCloudManagerの更新イベントを解除する
            pointCloudManager.trackablesChanged.RemoveListener(OnTrackablesChanged);

            // スキャンを停止する
            pointCloudManager.enabled = false;
        }

        private void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARPointCloud> args)
        {
            // 更新されたポイントクラウドの位置情報を取得する
            foreach (var pointCloud in args.updated)
            {
                if (!pointCloud.positions.HasValue)
                {
                    continue;
                }

                if (!pointCloud.identifiers.HasValue)
                {
                    continue;
                }

                // ポイントクラウドの位置情報をListに追加する
                for (int i = 0; i < pointCloud.positions.Value.Length; i++)
                {
                    // confidenceValuesはAndroidのみ対応しているため、nullチェックを行う
                    if (pointCloud.confidenceValues.HasValue)
                    {
                        // 信頼度が0.5未満の点は無視する
                        if (pointCloud.confidenceValues.Value[i] < 0.5f)
                        {
                            continue;
                        }
                    }

                    var id = pointCloud.identifiers.Value[i];
                    var position = pointCloud.positions.Value[i];

                    // 既に同じIDのポイントが存在する場合は上書きする
                    if (points.ContainsKey(id))
                    {
                        points[id] = position;
                    }
                    else
                    {
                        points.Add(id, position);
                    }
                }
            }
        }
    }
}
