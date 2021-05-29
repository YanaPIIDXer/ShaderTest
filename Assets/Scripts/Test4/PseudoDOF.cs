using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Test4
{
    /// <summary>
    /// 擬似的な被写界深度
    /// </summary>
    [RequireComponent(typeof(FillQuad))]
    public class PseudoDOF : MonoBehaviour
    {
        /// <summary>
        /// Webカメラ
        /// </summary>
        private WebCamera WebCam = new WebCamera();

        void Awake()
        {
            WebCam.Initialize();

            var Renderer = GetComponent<MeshRenderer>();
            var Mat = Renderer.material;

            RenderTexture BufferTex = new RenderTexture(1024, 768, 0);
            Graphics.Blit(WebCam.CamTex, BufferTex);

            Mat.SetTexture("_MainTex", WebCam.CamTex);
            Mat.SetTexture("_BufferTex", BufferTex);
            Mat.SetFloat("_TexelX", 1.0f / 1024);
            Mat.SetFloat("_TexelY", 1.0f / 768);

            Observable.IntervalFrame(40)
                      .Subscribe((_) => Graphics.Blit(WebCam.CamTex, BufferTex))
                      .AddTo(gameObject);
        }
    }
}
