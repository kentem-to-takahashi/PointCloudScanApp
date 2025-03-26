using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Bonus
{
    public class CinemachineAutoFramer : MonoBehaviour
    {
        private static readonly float MAINCAMERA_ROTATE_DEFAULT = 0;
        private static readonly float MAINCAMERA_Z_OFFSET = -1.5f;

        [SerializeField]
        private CinemachineCamera _initialCamera;// 初期のカメラ(点群にフィットする前の位置のカメラ)

        [SerializeField]
        private CinemachineCamera _fitCamera;// メインのカメラ(点群にフィットした後の位置のカメラ)

        [SerializeField]
        private CinemachineCamera _controllableCamera;// 自作のスクリプトで操作するカメラ

        [SerializeField]
        private PointCloudVisualizer _pointCloudVisualizer;//点群のboundsを取得するために使用

        private void Start()
        {
            //画面が読み込まれた時に1度だけ初期カメラ→メインカメラに切り替える
            StartCoroutine(InitializeCameraFit());
        }

        private IEnumerator InitializeCameraFit()
        {
            //アニメーションを開始する前に少し待機
            yield return new WaitForSeconds(0.3f);

            //カメラの位置を点群にフィットするように設定
            FitCameraSetTransform();

            //メインカメラに切り替える
            yield return SwitchToFitCamera();
        }

        /// <summary>
        /// 現在のカメラ位置から点群にフィットするカメラ位置に切り替える
        /// </summary>
        /// <returns></returns>
        public IEnumerator SwitchToFitCamera()
        {
            //現在のカメラの優先順位をクリア
            ClearCameraPriority();

            //遷移を開始(cinemachineでは仮想カメラの優先順位を変更することで切り替えが行われる)
            _fitCamera.Priority = 10;

            //アニメーションが完了するまで待機
            yield return new WaitForSeconds(2.0f);

            //フィット後は自作スクリプトでカメラを操作するためControllableCameraの優先順位を上げる
            _controllableCamera.transform.position = _fitCamera.transform.position;
            _controllableCamera.transform.rotation = _fitCamera.transform.rotation;
            ClearCameraPriority();
            _controllableCamera.Priority = 10;
        }

        /// <summary>
        /// 点群を画角に収めるためのカメラの位置を計算し、設定する
        /// </summary>
        private void FitCameraSetTransform()
        {
            //点群のboundsを取得
            var bounds = _pointCloudVisualizer.GetCalculateBounds();

            //点群を画角に収めるためのカメラの位置を計算
            var distance = CalculateCameraDistance(bounds);
            var boundsCenterX = bounds.center.x;
            var boundsCenterY = bounds.center.y;

            // メインカメラの位置と回転を点群にフィットするように設定
            _fitCamera.transform.position = new Vector3(boundsCenterX, boundsCenterY, distance + MAINCAMERA_Z_OFFSET); ;
            _fitCamera.transform.rotation = Quaternion.Euler(new Vector3(MAINCAMERA_ROTATE_DEFAULT, MAINCAMERA_ROTATE_DEFAULT, MAINCAMERA_ROTATE_DEFAULT));
        }

        /// <summary>
        /// オブジェクトを画角に収めるための最適なカメラのZ位置を計算
        /// </summary>
        private float CalculateCameraDistance(Bounds bounds)
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

        /// <summary>
        /// 3つのカメラの優先順位をクリア
        /// </summary>
        private void ClearCameraPriority()
        {
            _initialCamera.Priority = 0;
            _fitCamera.Priority = 0;
            _controllableCamera.Priority = 0;
        }
    }
}