using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
using UnityEngine.Rendering;

namespace Test3
{
    /// <summary>
    /// リムライト（？）
    /// </summary>
    public class LimLight : MonoBehaviour
    {
        /// <summary>
        /// Webカメラのレンダラ
        /// </summary>
        [SerializeField]
        private WebCameraRenderer WebCamRenderer = null;

        /// <summary>
        /// 描画カメラ
        /// </summary>
        [SerializeField]
        private Camera RenderCamera = null;

        void Awake()
        {
            WebCamRenderer.RenderTarget
                          .Where((t) => t != null)
                          .Subscribe(Initialize)
                          .AddTo(gameObject);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="WebCameraTexture">Webカメラのテクスチャ</param>
        private void Initialize(RenderTexture WebCameraTexture)
        {
            var BufferTex = new RenderTexture(Screen.width, Screen.height, 0);

            Graphics.Blit(WebCameraTexture, BufferTex);

            var Renderer = GetComponent<MeshRenderer>();
            var Mat = Renderer.material;
            Mat.SetTexture("_MainTex", WebCameraTexture);
            Mat.SetTexture("_BufferTex", BufferTex);
            Mat.SetFloat("_TexelX", 1.0f / Screen.width);
            Mat.SetFloat("_TexelY", 1.0f / Screen.height);

            // １０フレーム毎にWebカメラの画像を取得してバッファに流し、Shader内で差分を取る
            this.FixedUpdateAsObservable()
                .ThrottleFirstFrame(10)
                .Subscribe((_) =>
                {
                    Graphics.Blit(WebCameraTexture, BufferTex);
                }).AddTo(gameObject);
        }
    }
}
