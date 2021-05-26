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
        /// バッファ確認用
        /// </summary>
        [SerializeField]
        private MeshRenderer BufferCheck = null;

        /// <summary>
        /// 本番テクスチャ確認用
        /// </summary>
        [SerializeField]
        private MeshRenderer RenderCheck = null;

        /// <summary>
        /// 本番描画先
        /// </summary>
        [SerializeField]
        private MeshRenderer ProductionRenderer = null;

        /// <summary>
        /// Webカメラ
        /// </summary>
        [SerializeField]
        private WebCameraRenderer WebCamera = null;

        void Awake()
        {
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
            var BufferTex = new RenderTexture(1024, 768, 0);
            var RenderTex = new RenderTexture(1024, 768, 0);
            Camera.main.SetTargetBuffers(new RenderBuffer[] { BufferTex.colorBuffer, RenderTex.colorBuffer }, RenderTex.depthBuffer);

            var Mat = ProductionRenderer.material;
            Mat.SetTexture("_MainTex", WebCameraTexture);
            Mat.SetTexture("_BufferTex", BufferTex);
            Mat.SetTexture("_RenderTex", RenderTex);

            BufferCheck.material.mainTexture = BufferTex;
            RenderCheck.material.mainTexture = RenderTex;

            var CmdBuffer = new CommandBuffer();
            var Identifier = new RenderTargetIdentifier(BuiltinRenderTextureType.CurrentActive);
            CmdBuffer.SetRenderTarget(-1);
            CmdBuffer.Blit(RenderTex, Identifier);
            Camera.main.AddCommandBuffer(CameraEvent.AfterEverything, CmdBuffer);
        }
    }
}
