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
        this.UpdateAsObservable()
            .Where((_) => Input.GetKeyDown(KeyCode.Keypad1))
            .Subscribe((_) => SceneManager.LoadScene("Test1"))
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where((_) => Input.GetKeyDown(KeyCode.Keypad2))
            .Subscribe((_) => SceneManager.LoadScene("Test2"))
            .AddTo(gameObject);
    }
}
