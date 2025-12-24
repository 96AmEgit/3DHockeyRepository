using UnityEngine;

public class Player2controller : MonoBehaviour
{
    public float speed = 20.0f; 
    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float minZ = -5.0f;
    public float maxZ = 5.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetKey(KeyCode.RightArrow) ? 1.0f : Input.GetKey(KeyCode.LeftArrow) ? -1.0f : 0.0f;
        float verticalInput = Input.GetKey(KeyCode.UpArrow) ? 1.0f : Input.GetKey(KeyCode.DownArrow) ? -1.0f : 0.0f;

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * speed * Time.fixedDeltaTime;

        Vector3 newPosition = rb.position + movement;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        rb.MovePosition(newPosition);
    }
}
