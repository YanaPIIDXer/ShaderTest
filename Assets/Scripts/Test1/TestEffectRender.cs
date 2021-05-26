using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Operators;
using System;

namespace Test1
{
    /// <summary>
    /// テストエフェクトの描画
    /// </summary>
    public class TestEffectRender : MonoBehaviour
    {
        /// <summary>
        /// エフェクト描画用
        /// </summary>
        [SerializeField]
        private MeshRenderer MainRenderer = null;

        /// <summary>
        /// バッファ（確認）描画用
        /// </summary>
        [SerializeField]
        private MeshRenderer BufferRenderer = null;

        /// <summary>
        /// エフェクト
        /// </summary>
        [SerializeField]
        private TestEffect Effect = null;

        void Awake()
        {
            Effect.TargetTexture
                  .Where((Tex) => Tex != null)
                  .Subscribe((Tex) => MainRenderer.material.mainTexture = Tex)
                  .AddTo(gameObject);

            Effect.BufferTarget
                  .Where((Tex) => Tex != null)
                  .Subscribe((Tex) => BufferRenderer.material.mainTexture = Tex)
                  .AddTo(gameObject);
        }
    }
}
