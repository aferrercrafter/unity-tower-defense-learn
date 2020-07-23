using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 30f;
    public float panBorderThickness = 5f;

    public float scrollSpeed = 5f;
    public float minScrollY = 10f;
    public float maxScrollX = 80f;

    private bool doMovement = true;

    Vector3 forward, right;

    private void Start()
    {
        forward = transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;        

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);

        Vector3 pos = transform.position;
        pos.y -= Input.GetAxis("Mouse ScrollWheel") * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minScrollY, maxScrollX);
        transform.position = pos;

    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;

        transform.position += rightMovement;
        transform.position += upMovement;
    }
}
