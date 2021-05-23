using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Test1
{
    /// <summary>
    /// テスト用エフェクトその１
    /// </summary>
    public class TestEffect : MonoBehaviour
    {
        /// <summary>
        /// バッファ描画
        /// </summary>
        [SerializeField]
        private MeshRenderer BufferRenderer = null;

        /// <summary>
        /// 本番描画先
        /// </summary>
        [SerializeField]
        private MeshRenderer MainRenderer = null;

        /// <summary>
        /// Webカメラ
        /// </summary>
        [SerializeField]
        private WebCameraRenderer WebCamera = null;

        void Awake()
        {
            WebCamera.RenderTarget
                .Subscribe(Initialize).AddTo(gameObject);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="WebCameraTexture">Webカメラからの出力テクスチャ</param>
        private void Initialize(RenderTexture WebCameraTexture)
        {
            var BufferTex = new RenderTexture(1024, 768, 0);
            var RenderTex = new RenderTexture(1024, 768, 24);
            Camera.main.SetTargetBuffers(new RenderBuffer[] { BufferTex.colorBuffer, RenderTex.colorBuffer }, RenderTex.depthBuffer);

            var Mat = MainRenderer.material;
            Mat.SetTexture("_MainTex", WebCameraTexture);

            BufferRenderer.material.mainTexture = BufferTex;
        }
    }
}
