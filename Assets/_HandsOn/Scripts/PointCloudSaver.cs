using UnityEngine;

public class PointCloudSaver : MonoBehaviour
{
    // PointCloudScannerをインスペクターから設定できるようにする

    private static readonly string FILE_NAME = "/pointcloud.txt";

    public void Save()
    {
        // 点群の位置情報を取得する


        // 点群の位置情報をファイルに保存する
        var filePath = Application.persistentDataPath + FILE_NAME;


        Debug.Log("Saved point cloud to " + filePath);
    }
}
