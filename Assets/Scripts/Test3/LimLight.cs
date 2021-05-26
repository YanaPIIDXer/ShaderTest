using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

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
            var Renderer = GetComponent<MeshRenderer>();
            var Mat = Renderer.material;

            Mat.mainTexture = WebCameraTexture;
        }
    }
}
