using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Test1
{
    /// <summary>
    /// テストエフェクトの描画
    /// </summary>
    public class TestEffectRender : MonoBehaviour
    {
        /// <summary>
        /// エフェクト
        /// </summary>
        [SerializeField]
        private TestEffect Effect = null;

        /// <summary>
        /// レンダラ
        /// </summary>
        private MeshRenderer Renderer = null;

        void Awake()
        {
            float Height = Camera.main.orthographicSize * 2;
            float Width = Height * Camera.main.aspect;
            transform.localScale = new Vector3(Width, Height, 1);

            Renderer = GetComponent<MeshRenderer>();

            Effect.TargetTexture
                  .Where((t) => t != null)
                  .Subscribe((t) => Renderer.material.mainTexture = t)
                  .AddTo(gameObject);
        }
    }
}
