using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Bonus
{
    /// <summary>
    /// 以下のフローを実施するクラス
    /// 1. 点群の大きさや位置にあわせてメインのカメラの位置を調整
    /// 2. 初期のカメラからメインのカメラに切り替える(遷移はアニメーションします)
    /// 3. カメラの切り替えが完了したら、以降は自作のカメラコントロールスクリプト
    //     を使用するためcinemachineを無効化
    /// </summary>
    public class CinemachineAutoFramer : MonoBehaviour
    {
        private static readonly float MAINCAMERA_ROTATE_DEFAULT = 0;
        private static readonly float MAINCAMERA_Z_OFFSET = -1.5f;

        [SerializeField]
        private CinemachineBrain _cinemachineBrain;

        [SerializeField]
        private CinemachineCamera _initialCamera;// 初期のカメラ(点群にフィットする前の位置のカメラ)

        [SerializeField]
        private CinemachineCamera _mainCamera;// メインのカメラ(点群にフィットした後の位置のカメラ)

        [SerializeField]
        private PointCloudVisualizer _pointCloudVisualizer;

        private void Start()
        {
            StartCoroutine(SwitchToMainCamera());
        }

        private IEnumerator SwitchToMainCamera()
        {
            //アニメーションを開始する前に少し待機
            yield return new WaitForSeconds(0.3f);

            //点群のboundsを取得
            var bounds = _pointCloudVisualizer.GetCalculateBounds();

            //点群を画角に収めるためのカメラの位置を計算
            var distance = CalculateCameraDistance(bounds);
            var boundsCenterX = bounds.center.x;
            var boundsCenterY = bounds.center.y;

            // メインカメラの位置と回転を点群にフィットするように設定
            _mainCamera.transform.position = new Vector3(boundsCenterX, boundsCenterY, distance + MAINCAMERA_Z_OFFSET); ;
            _mainCamera.transform.rotation = Quaternion.Euler(new Vector3(MAINCAMERA_ROTATE_DEFAULT, MAINCAMERA_ROTATE_DEFAULT, MAINCAMERA_ROTATE_DEFAULT));

            //遷移を開始(cinemachineでは仮想カメラの優先順位を変更することで切り替えが行われる)
            _mainCamera.Priority = 10;

            //アニメーションが完了するまで待機
            yield return new WaitForSeconds(2.0f);

            //カメラ操作は独自スクリプトで行うためcinemachineを無効化
            _cinemachineBrain.enabled = false;
        }

        /// <summary>
        /// オブジェクトを画角に収めるための最適なカメラのZ位置を計算
        /// </summary>
        public float CalculateCameraDistance(Bounds bounds)
        {
            float objectHeight = bounds.size.y; // オブジェクトの高さ
            float objectWidth = bounds.size.x;  // オブジェクトの幅

            // カメラのFOVをラジアンに変換
            float verticalFovRad = Camera.main.fieldOfView * Mathf.Deg2Rad;
            float horizontalFovRad = 2f * Mathf.Atan(Mathf.Tan(verticalFovRad / 2f) * Camera.main.aspect);

            // 縦方向のZ距離
            float distanceHeight = (objectHeight / 2f) / Mathf.Tan(verticalFovRad / 2f);

            // 横方向のZ距離
            float distanceWidth = (objectWidth / 2f) / Mathf.Tan(horizontalFovRad / 2f);

            // より大きい方の距離を採用 (確実に収める)
            return Mathf.Max(distanceHeight, distanceWidth) * -1f;
        }
    }
}