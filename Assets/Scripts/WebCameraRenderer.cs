using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UniRx;
using System;

/// <summary>
/// Webカメラ
/// </summary>
public class WebCameraRenderer : MonoBehaviour
{
    /// <summary>
    /// 描画用カメラ
    /// </summary>
    [SerializeField]
    private Camera RenderCamera = null;

    /// <summary>
    /// 描画先
    /// </summary>
    private ReactiveProperty<RenderTexture> _RenderTarget = new ReactiveProperty<RenderTexture>();

    /// <summary>
    /// 描画先
    /// ※Awakeの実行順序に依存しないようにObservableとして外部に公開する
    /// </summary>
    public IObservable<RenderTexture> RenderTarget { get { return _RenderTarget; } }

    void Awake()
    {
        float Height = RenderCamera.orthographicSize * 2;
        float Width = Height * RenderCamera.aspect;
        transform.localScale = new Vector3(Width, Height, 0);

        WebCamDevice Device = WebCamTexture.devices[0];
        var CamTex = new WebCamTexture(Device.name, Screen.width, Screen.height, 60);
        CamTex.Play();

        GetComponent<MeshRenderer>().material.mainTexture = CamTex;

        var Target = new RenderTexture(1024, 768, 0);
        RenderCamera.SetTargetBuffers(Target.colorBuffer, Target.depthBuffer);
        _RenderTarget.Value = Target;
    }
}
