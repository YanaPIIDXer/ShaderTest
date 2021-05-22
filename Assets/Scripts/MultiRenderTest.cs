using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// マルチレンダーテスト
/// </summary>
public class MultiRenderTest : MonoBehaviour
{
    /// <summary>
    /// バッファテクスチャ表示用
    /// </summary>
    [SerializeField]
    private MeshRenderer BufferTextureRenderer = null;

    /// <summary>
    /// レンダーテクスチャ表示用
    /// </summary>
    [SerializeField]
    private MeshRenderer RenderTextureRenderer = null;

    /// <summary>
    /// カメラテクスチャ描画用
    /// </summary>
    [SerializeField]
    private MeshRenderer CameraTextureRenderer = null;

    /// <summary>
    /// 描画カメラ
    /// </summary>
    [SerializeField]
    private Camera RenderCamera = null;

    void Awake()
    {
        float Height = RenderCamera.orthographicSize * 2;
        float Width = Height * RenderCamera.aspect;
        CameraTextureRenderer.transform.localScale = new Vector3(Width, Height, 0);

        WebCamDevice Device = WebCamTexture.devices[0];
        var CamTex = new WebCamTexture(Device.name, Screen.width, Screen.height, 60);
        CamTex.Play();

        var BufferTex = new RenderTexture(Screen.width, Screen.height, 0);
        var RenderTex = new RenderTexture(Screen.width, Screen.height, 24);

        CameraTextureRenderer.material.SetTexture("_MainTex", CamTex);
        CameraTextureRenderer.material.SetTexture("_BufferTex", BufferTex);
        CameraTextureRenderer.material.SetTexture("_RenderTex", RenderTex);

        RenderCamera.SetTargetBuffers(new RenderBuffer[] { BufferTex.colorBuffer, RenderTex.colorBuffer }, RenderTex.depthBuffer);

        BufferTextureRenderer.material.SetTexture("_MainTex", BufferTex);
        RenderTextureRenderer.material.SetTexture("_MainTex", RenderTex);
    }
}
