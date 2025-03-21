using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PointCloudFileReader : MonoBehaviour
{
    private static readonly string FILE_NAME = "/pointcloud.txt";

    private List<Vector3> points = new List<Vector3>();

    public event Action ReadedFile;

    public Vector3[] Points => points.ToArray();

    private void Start()
    {
        ReadFile();
    }

    public void ReadFile()
    {
        var filePath = Application.persistentDataPath + FILE_NAME;
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            return;
        }

        points.Clear();

        using (var reader = File.OpenText(filePath))
        {
            while (!reader.EndOfStream)
            {
                // 1行読み込む
                var line = reader.ReadLine();

                // カンマで区切って3つの値を取得する
                var values = line.Split(',');
                if (values.Length != 3)
                {
                    Debug.LogWarning("Invalid line: " + line);
                    continue;
                }

                // 3つの値をVector3に変換してリストに追加する
                var x = float.Parse(values[0]);
                var y = float.Parse(values[1]);
                var z = float.Parse(values[2]);
                points.Add(new Vector3(x, y, z));
            }
        }

        // ファイル読み込み完了時にイベントを発行する
        ReadedFile?.Invoke();
    }
}
