using UnityEngine;

public class Player1controller : MonoBehaviour
{
    public float speed = 20.0f; // Rigidbodyを使うときは少し大きめの値にするのがコツ
    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float minZ = -5.0f;
    public float maxZ = 5.0f;

    private Rigidbody rb; // Rigidbodyを入れる変数

    void Start()
    {
        // ゲーム開始時に自分のRigidbodyを取得する
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() // 物理演算を使うときはUpdateではなくFixedUpdateを使う
    {
        float horizontalInput = Input.GetKey(KeyCode.D) ? 1.0f : Input.GetKey(KeyCode.A) ? -1.0f : 0.0f;
        float verticalInput = Input.GetKey(KeyCode.W) ? 1.0f : Input.GetKey(KeyCode.S) ? -1.0f : 0.0f;

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * speed * Time.fixedDeltaTime;

        // 現在の場所から「次の場所」を計算
        Vector3 newPosition = rb.position + movement;

        // 移動制限（Clamp）
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // 物理法則に従って移動させる（これが重要！）
        rb.MovePosition(newPosition);
    }
}
