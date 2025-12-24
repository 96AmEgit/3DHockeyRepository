using UnityEngine;

public class Player2controller : MonoBehaviour
{
    public float minX = 0.0f; // 中央より左に行かない制限
    public float maxX = 5.0f;
    public float minZ = -5.0f;
    public float maxZ = 5.0f;

    private Rigidbody rb;
    private Camera mainCamera;
    private float cameraDistance;

    // 自分の指ID
    private int myFingerId = -1;
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        cameraDistance = Mathf.Abs(mainCamera.transform.position.y - transform.position.y);
        targetPosition = transform.position;
    }

    void Update()
    {
        if (myFingerId == -1)
        {
            foreach (Touch touch in Input.touches)
            {
                // P2は「タッチ開始」かつ「画面の右側」なら自分の指と認定
                if (touch.phase == TouchPhase.Began && touch.position.x >= Screen.width / 2)
                {
                    myFingerId = touch.fingerId;
                }
            }
        }
        else
        {
            bool fingerFound = false;
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == myFingerId)
                {
                    fingerFound = true;
                    CalculateTargetPosition(touch);

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        myFingerId = -1;
                    }
                }
            }
            if (!fingerFound) myFingerId = -1;
        }
    }

    void FixedUpdate()
    {
        if (myFingerId != -1)
        {
            rb.MovePosition(targetPosition);
        }
    }

    void CalculateTargetPosition(Touch touch)
    {
        Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, cameraDistance);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        worldPosition.y = transform.position.y;

        // 移動制限
        worldPosition.x = Mathf.Clamp(worldPosition.x, minX, maxX);
        worldPosition.z = Mathf.Clamp(worldPosition.z, minZ, maxZ);

        targetPosition = worldPosition;
    }
}
