using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Rendering;

namespace Test1
{
    /// <summary>
    /// テスト用エフェクトその１
    /// </summary>
    public class TestEffect : MonoBehaviour
    {
        /// <summary>
        /// 描画先
        /// </summary>
        private MeshRenderer Renderer = null;

        /// <summary>
        /// Webカメラ
        /// </summary>
        [SerializeField]
        private WebCameraRenderer WebCamera = null;

        /// <summary>
        /// エフェクト描画用カメラ
        /// </summary>
        [SerializeField]
        private Camera EffectCamera = null;

        /// <summary>
        /// 描画テクスチャ
        /// </summary>
        private ReactiveProperty<RenderTexture> _TargetTexture = new ReactiveProperty<RenderTexture>();

        /// <summary>
        /// 描画テクスチャ
        /// </summary>
        public IObservable<RenderTexture> TargetTexture { get { return _TargetTexture; } }

        void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();

            WebCamera.RenderTarget
                .Where((Tex) => Tex != null)
                .Subscribe(Initialize).AddTo(gameObject);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="WebCameraTexture">Webカメラからの出力テクスチャ</param>
        private void Initialize(RenderTexture WebCameraTexture)
        {
            var BufferTex = new RenderTexture(Screen.width, Screen.height, 0);
            var RenderTex = new RenderTexture(Screen.width, Screen.height, 24);
            EffectCamera.SetTargetBuffers(new RenderBuffer[] { RenderTex.colorBuffer }, RenderTex.depthBuffer);

            var Mat = Renderer.material;
            Mat.SetTexture("_MainTex", WebCameraTexture);
            Mat.SetTexture("_BufferTex", BufferTex);

            var PostCmdBuffer = new CommandBuffer();
            var Identifier = new RenderTargetIdentifier(BuiltinRenderTextureType.CurrentActive);
            PostCmdBuffer.SetRenderTarget(-1);
            PostCmdBuffer.Blit(RenderTex, Identifier);
            EffectCamera.AddCommandBuffer(CameraEvent.AfterEverything, PostCmdBuffer);

            _TargetTexture.Value = RenderTex;

            // 2FPSで動くWebカメラの映像を作り上げ、Shader内で60FPSの方と合成する
            //      →残像の出来上がり
            Observable.Interval(TimeSpan.FromSeconds(0.5))
                      .Subscribe((_) => Graphics.Blit(WebCameraTexture, BufferTex))
                      .AddTo(gameObject);
        }
    }
}
