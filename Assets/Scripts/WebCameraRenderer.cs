using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UniRx;
using System;

/// <summary>
/// Webカメラ
/// </summary>
[RequireComponent(typeof(FillQuad))]
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

    /// <summary>
    /// Webカメラのテクスチャ
    /// </summary>
    private WebCamTexture CamTex = null;

    void Awake()
    {
        GetComponent<FillQuad>().TargetCamera = RenderCamera;

        WebCamDevice Device = WebCamTexture.devices[0];
        CamTex = new WebCamTexture(Device.name, Screen.width, Screen.height, 60);

        GetComponent<MeshRenderer>().material.SetTexture("_MainTex", CamTex);

        var Target = new RenderTexture(1024, 768, 0);
        RenderCamera.SetTargetBuffers(Target.colorBuffer, Target.depthBuffer);

        CamTex.Play();
        _RenderTarget.Value = Target;
    }

    void OnDestroy()
    {
        // シーン遷移が絡む場合、これが無いとカメラを掴みっぱなしになる
        // ※EditorでPlayModeを抜ける時は何故か問題なくカメラを放す
        CamTex.Stop();
    }
}
