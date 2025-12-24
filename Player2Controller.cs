using UnityEngine;

public class Player2controller : MonoBehaviour
{
    // 移動制限の設定
    public float minX = 0.0f; // 右側担当なので、中央(0)より左に行かないように制限
    public float maxX = 5.0f;
    public float minZ = -5.0f;
    public float maxZ = 5.0f;

    private Rigidbody rb;
    private Camera mainCamera;
    private float cameraDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        
        cameraDistance = Mathf.Abs(mainCamera.transform.position.y - transform.position.y);
    }

    void FixedUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            // 【ここが変更点】
            // タッチ位置のX座標が「画面幅の半分以上」＝「画面の右側」
            if (touch.position.x >= Screen.width / 2)
            {
                MoveToFinger(touch);
                // Player2用の指が見つかったので抜ける
                break;
            }
        }
    }

    void MoveToFinger(Touch touch)
    {
        Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, cameraDistance);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        worldPosition.y = transform.position.y;

        worldPosition.x = Mathf.Clamp(worldPosition.x, minX, maxX);
        worldPosition.z = Mathf.Clamp(worldPosition.z, minZ, maxZ);

        rb.MovePosition(worldPosition);
    }
}
