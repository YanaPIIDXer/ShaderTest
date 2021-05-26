using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン切り替え
/// </summary>
public class SceneChanger : MonoBehaviour
{
    void Awake()
    {
        // 本来なら１つのシーン上に全部のエフェクトを置いて管理するべきなのかも知れないが、
        // 追加するたびに至る所にトグル処理を書かなければならないっぽい
        // それならこっちの方がマシ
        this.UpdateAsObservable()
            .Where((_) => Input.GetKeyDown(KeyCode.Keypad0))
            .Subscribe((_) => SceneManager.LoadScene("Nothing"))
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where((_) => Input.GetKeyDown(KeyCode.Keypad1))
            .Subscribe((_) => SceneManager.LoadScene("Test1"))
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where((_) => Input.GetKeyDown(KeyCode.Keypad2))
            .Subscribe((_) => SceneManager.LoadScene("Test2"))
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where((_) => Input.GetKeyDown(KeyCode.Keypad3))
            .Subscribe((_) => SceneManager.LoadScene("Test3"))
            .AddTo(gameObject);
    }
}
