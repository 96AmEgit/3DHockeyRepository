using UnityEngine;

public class Player2controller : MonoBehaviour
{
    public float minX = -5.0f;
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
        
        // カメラ距離の計算（絶対値にしておく）
        cameraDistance = Mathf.Abs(mainCamera.transform.position.y - transform.position.y);
    }

    void FixedUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            // Player2は「画面の上半分 (y >= Screen.height / 2)」のタッチだけを見る
            if (touch.position.y >= Screen.height / 2)
            {
                MoveToFinger(touch);
                break;
            }
        }
    }

    void MoveToFinger(Touch touch)
    {
        Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, cameraDistance);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        worldPosition.y = transform.position.y;

        // 移動制限
        worldPosition.x = Mathf.Clamp(worldPosition.x, minX, maxX);
        worldPosition.z = Mathf.Clamp(worldPosition.z, minZ, maxZ);

        rb.MovePosition(worldPosition);
    }
}
