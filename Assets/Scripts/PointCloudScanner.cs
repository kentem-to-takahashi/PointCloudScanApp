using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PointCloudScanner : MonoBehaviour
{
    [SerializeField]
    private ARPointCloudManager pointCloudManager;

    private Dictionary<ulong, Vector3> points = new Dictionary<ulong, Vector3>();

    private void Awake()
    {
        // はじめはスキャンを停止しておく
        pointCloudManager.enabled = false;
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
            if(!pointCloud.positions.HasValue)
            {
                continue;
            }

            if(!pointCloud.identifiers.HasValue)
            {
                continue;
            }

            // ポイントクラウドの位置情報をDictionaryに保存する
            for (int i = 0; i < pointCloud.positions.Value.Length; i++)
            {
                var id = pointCloud.identifiers.Value[i];
                var position = pointCloud.positions.Value[i];

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
