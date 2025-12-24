using UnityEngine;

public class Player1controller : MonoBehaviour
{
    public float minX = -5.0f;
    public float maxX = 0.0f; // 中央より右に行かない制限
    public float minZ = -5.0f;
    public float maxZ = 5.0f;

    private Rigidbody rb;
    private Camera mainCamera;
    private float cameraDistance;

    // 「自分の指」のIDを記憶する変数（-1は「指がない」という意味）
    private int myFingerId = -1;
    private Vector3 targetPosition; // 移動先の目標座標

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        cameraDistance = Mathf.Abs(mainCamera.transform.position.y - transform.position.y);
        
        // 初期位置を目標にしておく（変な飛び出し防止）
        targetPosition = transform.position;
    }

    void Update()
    {
        // ■ 指を探す処理（毎フレーム実行）
        if (myFingerId == -1)
        {
            // まだ指を捕まえていない時：新しくタッチされた指を探す
            foreach (Touch touch in Input.touches)
            {
                // 「タッチ開始（Began）」かつ「画面の左側」なら自分の指と認定！
                if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2)
                {
                    myFingerId = touch.fingerId; // IDを記憶（ロックオン！）
                }
            }
        }
        else
        {
            // すでに指を捕まえている時：記憶したIDの指だけを追う
            bool fingerFound = false;
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == myFingerId)
                {
                    fingerFound = true;
                    
                    // 指の位置を計算
                    CalculateTargetPosition(touch);

                    // 指が離されたら追跡終了
                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        myFingerId = -1;
                    }
                }
            }
            // もし何らかの理由で指が見失われたらリセット
            if (!fingerFound) myFingerId = -1;
        }
    }

    void FixedUpdate()
    {
        // ■ 実際に動かす処理
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

        // パドル自体の移動制限（指がはみ出てもパドルはここで止まる）
        worldPosition.x = Mathf.Clamp(worldPosition.x, minX, maxX);
        worldPosition.z = Mathf.Clamp(worldPosition.z, minZ, maxZ);

        targetPosition = worldPosition;
    }
}
