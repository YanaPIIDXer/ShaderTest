using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Renderer.material.mainTexture = WebCam.CamTex;
        }
    }
}
