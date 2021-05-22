using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRenderTest : MonoBehaviour
{
    /// <summary>
    /// バッファテクスチャ表示用
    /// </summary>
    [SerializeField]
    private MeshRenderer BufferTextureRenderer = null;

    /// <summary>
    /// レンダーテクスチャ表示用
    /// </summary>
    [SerializeField]
    private MeshRenderer RenderTextureRenderer = null;

    void Awake()
    {

    }
}
