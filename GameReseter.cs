using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReseter : MonoBehaviour
{
    // ゲームをリセットするキー（Rキー）を設定
    public KeyCode restartKey = KeyCode.R;

    // ゲームの開始シーン名を設定
    public string startSceneName = "SampleScene";

    void Update()
    {
        // キーが押されたときの処理
        if (Input.GetKeyDown(restartKey))
        {
            // ゲームを最初からやり直すために指定したシーンに遷移
            SceneManager.LoadScene(startSceneName);
        }
    }
}
