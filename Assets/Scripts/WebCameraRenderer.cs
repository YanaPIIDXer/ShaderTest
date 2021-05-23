using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        float Height = RenderCamera.orthographicSize * 2;
        float Width = Height * RenderCamera.aspect;
        transform.localScale = new Vector3(Width, Height, 0);

        WebCamDevice Device = WebCamTexture.devices[0];
        var CamTex = new WebCamTexture(Device.name, Screen.width, Screen.height, 60);
        CamTex.Play();

        GetComponent<MeshRenderer>().material.mainTexture = CamTex;
    }
}
