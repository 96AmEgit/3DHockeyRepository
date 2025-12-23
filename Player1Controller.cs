using UnityEngine;

public class Player1controller : MonoBehaviour
{
    public float speed = 5.0f; // 移動速度を設定します。
    public float minX = -5.0f; // X軸の最小移動制限
    public float maxX = 5.0f;  // X軸の最大移動制限
    public float minZ = -5.0f; // Z軸の最小移動制限
    public float maxZ = 5.0f;  // Z軸の最大移動制限

    void Update()
    {
        float horizontalInput = Input.GetKey(KeyCode.D) ? 1.0f : Input.GetKey(KeyCode.A) ? -1.0f : 0.0f; // AとDキーの入力を検出
        float verticalInput = Input.GetKey(KeyCode.W) ? 1.0f : Input.GetKey(KeyCode.S) ? -1.0f : 0.0f; // WとSキーの入力を検出

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * speed * Time.deltaTime; // 移動ベクトルを計算

        // 新しい座標を計算
        Vector3 newPosition = transform.position + movement;

        // 移動制限を適用
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // オブジェクトを新しい座標に移動させる
        transform.position = newPosition;
    }
}
