using UnityEngine;

public class Player1controller : MonoBehaviour
{
    // 移動制限の設定（必要に応じて調整してください）
    public float minX = -5.0f;
    public float maxX = 0.0f; // 左側担当なので、中央(0)より右に行かないように制限すると良いかも
    public float minZ = -5.0f;
    public float maxZ = 5.0f;

    private Rigidbody rb;
    private Camera mainCamera;
    private float cameraDistance; // カメラとプレイヤーの距離

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        // カメラからプレイヤーまでの「奥行き」距離を計算
        // ※カメラが真上(Y軸方向)から見下ろしている前提の計算です
        cameraDistance = Mathf.Abs(mainCamera.transform.position.y - transform.position.y);
    }

    void FixedUpdate()
    {
        // 画面に触れているすべての指をチェック
        foreach (Touch touch in Input.touches)
        {
            // 【ここが変更点】
            // タッチ位置のX座標が「画面幅の半分より小さい」＝「画面の左側」
            if (touch.position.x < Screen.width / 2)
            {
                MoveToFinger(touch);
                // Player1用の指が見つかったので、他の指は無視してループを抜ける
                break; 
            }
        }
    }

    // 指の位置に移動させる処理（ここは前回と同じ）
    void MoveToFinger(Touch touch)
    {
        // タッチ座標(2D)をゲーム世界座標(3D)に変換
        Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, cameraDistance);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        // 高さは固定
        worldPosition.y = transform.position.y;

        // 移動制限（Clamp）を適用
        worldPosition.x = Mathf.Clamp(worldPosition.x, minX, maxX);
        worldPosition.z = Mathf.Clamp(worldPosition.z, minZ, maxZ);

        // 物理演算を使って、その場所に移動
        rb.MovePosition(worldPosition);
    }
}
