using UnityEngine;

public class Player1controller : MonoBehaviour
{
    // 移動制限の設定（前の設定を引き継ぎ）
    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float minZ = -5.0f;
    public float maxZ = 5.0f;

    private Rigidbody rb;
    private Camera mainCamera;
    private float cameraDistance; // カメラとプレイヤーの距離

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        // カメラからプレイヤーまでの奥行き距離を計算（これをしないと座標がズレます）
        // ※カメラがY軸方向にある前提です
        cameraDistance = mainCamera.transform.position.y - transform.position.y;
    }

    void FixedUpdate()
    {
        // 画面に触れている指の数だけループ処理
        foreach (Touch touch in Input.touches)
        {
            // Player1は「画面の下半分 (y < Screen.height / 2)」のタッチだけを見る
            if (touch.position.y < Screen.height / 2)
            {
                MoveToFinger(touch);
                // 1本の指に反応すれば十分なので、処理を抜ける（マルチタッチ誤作動防止）
                break; 
            }
        }
    }

    void MoveToFinger(Touch touch)
    {
        // タッチした画面上の位置(X, Y)を、3D空間の座標(X, Y, Z)に変換
        // Zには「カメラとの距離」を入れるのがコツです
        Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, cameraDistance);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        // 高さは固定（変な高さに行かないように）
        worldPosition.y = transform.position.y;

        // 移動制限（Clamp）を適用
        worldPosition.x = Mathf.Clamp(worldPosition.x, minX, maxX);
        worldPosition.z = Mathf.Clamp(worldPosition.z, minZ, maxZ);

        // 物理演算を使って、その場所に瞬間移動
        rb.MovePosition(worldPosition);
    }
}
