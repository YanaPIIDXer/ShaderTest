using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラ全体に描画されるQuad
/// ※１：Quadでの使用を想定。それ以外のMeshでの挙動は知らん
/// ※２：CameraのProjectionがorthographicになっていないと正常動作しない
/// </summary>
/// <see href="https://gist.github.com/tsubaki/8109124">参考資料</see>
public class FillQuad : MonoBehaviour
{
    /// <summary>
    /// 対象カメラ
    /// </summary>
    [SerializeField]
    private Camera _TargetCamera = null;

    /// <summary>
    /// 対象カメラ
    /// </summary>
    public Camera TargetCamera
    {
        get { return _TargetCamera; }
        set
        {
            _TargetCamera = value;
            FixToCamera();
        }
    }

    void Awake()
    {
        if (TargetCamera == null)
        {
            // 未設定の場合はメインカメラを使用する
            _TargetCamera = Camera.main;
        }

        FixToCamera();
    }

    /// <summary>
    /// カメラに合わせる
    /// </summary>
    private void FixToCamera()
    {
        float Height = TargetCamera.orthographicSize * 2;
        float Width = Height * TargetCamera.aspect;
        transform.localScale = new Vector3(Width, Height, 1);
        transform.position = new Vector3(transform.position.x, TargetCamera.transform.position.y, transform.position.z);
    }
}
