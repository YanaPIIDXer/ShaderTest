﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Rendering;
using UniRx.Triggers;

namespace Test2
{
    /// <summary>
    /// 動いた場所から波紋を出すシェーダ
    /// </summary>
    public class WaveShader : MonoBehaviour
    {
        /// <summary>
        /// Webカメラのレンダラ
        /// </summary>
        [SerializeField]
        private WebCameraRenderer WebCamRenderer = null;

        /// <summary>
        /// 描画用カメラ
        /// </summary>
        [SerializeField]
        private Camera RenderCamera = null;

        /// <summary>
        /// レンダラ
        /// </summary>
        MeshRenderer Renderer = null;

        void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();

            WebCamRenderer.RenderTarget
                          .Where((t) => t != null)
                          .Subscribe((t) => Initialize(t))
                          .AddTo(gameObject);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="WebCameraTexture">Webカメラの映像テクスチャ</param>
        private void Initialize(RenderTexture WebCameraTexture)
        {
            var BufferTex = new RenderTexture(Screen.width, Screen.height, 0);
            var RenderTex = new RenderTexture(Screen.width, Screen.height, 24);

            Graphics.Blit(WebCameraTexture, BufferTex);
            RenderCamera.SetTargetBuffers(RenderTex.colorBuffer, RenderTex.depthBuffer);

            var Mat = Renderer.material;
            Mat.SetTexture("_MainTex", WebCameraTexture);
            Mat.SetTexture("_BufferTex", BufferTex);

            var PostCmdBuffer = new CommandBuffer();
            var Identifier = new RenderTargetIdentifier(BuiltinRenderTextureType.CurrentActive);
            PostCmdBuffer.SetRenderTarget(-1);
            PostCmdBuffer.Blit(RenderTex, Identifier);
            RenderCamera.AddCommandBuffer(CameraEvent.AfterEverything, PostCmdBuffer);

            this.FixedUpdateAsObservable()
                .ThrottleFirstFrame(5)
                .Subscribe((_) => Graphics.Blit(WebCameraTexture, BufferTex));
        }
    }
}
