using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        WebCamDevice Device = WebCamTexture.devices[0];
        var CamTex = new WebCamTexture(Device.name, 1920, 1080, 60);
        CamTex.Play();

        RenderTextureRenderer.material.SetTexture("_MainTex", CamTex);
    }
}
