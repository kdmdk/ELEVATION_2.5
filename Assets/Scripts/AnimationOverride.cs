using UnityEngine;
using System.Collections;

// "https:/qiita.com/sango/items/3f7b9b19435fe7d58e28" 引用

public class AnimationOverride : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer m_renderer = null;     //!< コントローラーの操作対象

    [SerializeField]
    string m_path = "";       //!< バリエーションテクスチャのパス

    SpriteLoader m_loader = null;     //!< Sprite読み込むやーつ

    void Awake()
    {
        if (m_renderer == null)
            m_renderer = GetComponent<SpriteRenderer>();

        // 任意のバリエーションテクスチャを読み込む
        m_loader = new SpriteLoader();
        m_loader.Load(m_path);
    }

    /**
     * ここでSpriteを挿げ替える
     */
    void LateUpdate()
    {
        if (m_renderer == null) return;
        if (m_renderer.sprite == null) return;

        if (string.IsNullOrEmpty(m_path)) return;

        // SpriteLoaderから今AnimationClipが表示しているスプライトと同じ名前のスプライトを取得して、割り当て直す
        m_renderer.sprite = m_loader.GetSprite(m_renderer.sprite.name);
    }
}