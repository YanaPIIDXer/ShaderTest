using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// 普通にWebカメラの映像を描画するだけ
/// </summary>
public class SimpleCameraRender : MonoBehaviour
{
    /// <summary>
    /// Webカメラレンダラ
    /// </summary>
    [SerializeField]
    private WebCameraRenderer CamRenderer = null;

    void Awake()
    {
        var Mat = GetComponent<MeshRenderer>().material;
        CamRenderer.RenderTarget
                   .Where((t) => t != null)
                   .Subscribe((t) => Mat.mainTexture = t)
                   .AddTo(gameObject);
    }
}
