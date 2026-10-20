using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 2f;

    public float horizontalRange = 60f;
    public float verticalRange = 40f;

    private float rotationX;
    private float rotationY;

    private Quaternion cameraRotate;

    private void Awake()
    {
         GetComponent<Camera>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraRotate = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;

        rotationX = Mathf.Clamp(rotationX, -horizontalRange, horizontalRange);
        rotationY = Mathf.Clamp(rotationY, -verticalRange, verticalRange);

        Quaternion targetRotation = Quaternion.Euler(rotationY, rotationX, 0f);
        transform.localRotation = cameraRotate * targetRotation;
    }
    public Ray GetLookRay()
    {
        return new Ray (transform.position, transform.forward);
    }
}
