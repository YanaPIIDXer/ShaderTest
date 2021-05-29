using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// Webカメラ
/// </summary>
public class WebCamera
{
    /// <summary>
    /// Webカメラのテクスチャ
    /// </summary>
    public WebCamTexture CamTex { get; private set; } = null;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public WebCamera()
    {
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        WebCamDevice Device = WebCamTexture.devices[0];
        CamTex = new WebCamTexture(Device.name, Screen.width, Screen.height, 60);
        CamTex.Play();
    }

    ~WebCamera()
    {
        // シーン遷移が絡む場合、これが無いとカメラを掴みっぱなしになる
        // ※EditorでPlayModeを抜ける時は何故か問題なくカメラを放す
        if (CamTex != null)
        {
            CamTex.Stop();
        }
    }
}
