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

        using (var reader = File.OpenText(filePath))
        {
            
        }

        // ファイル読み込み完了時にイベントを発行する
        ReadedFile?.Invoke();
    }
}
