using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField]
    private PointCloudSaver pointCloudSaver;

    private Button button;

    private void Awake()
    {
        // Buttonコンポーネントを取得する
    }

    private void Start()
    {
        // ボタンがクリックされたときのイベントを登録する
    }

    private void OnDestroy()
    {
        // イベントを解除する
    }

    public void OnClick()
    {
        // PointCloudSaverのSaveメソッドを呼び出す
    }
}
