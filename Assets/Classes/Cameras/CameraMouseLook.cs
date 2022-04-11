using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseLook : MonoBehaviour
{
    public float mouseSense = 0.1f;
    public PlayerInputAction input;
    private Camera camera;
    public float[] yClamp = { -90f, 90f };
    public float[] xClamp = { -360f, 360f };

    float xRotation = 0f;
    float yRotation = 0f;

    Vector3 startRotation;

    // Start is called before the first frame update
    void Start()
    {
        input = new PlayerInputAction();
        input.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        startRotation = transform.parent.eulerAngles;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime > 0 && camera.enabled)
        {
            Vector2 mouseRotation = input.Ground.Camera.ReadValue<Vector2>();
            float mouseX = mouseRotation.x * mouseSense;// * Time.deltaTime;
            float mouseY = mouseRotation.y * mouseSense;// * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, yClamp[0], yClamp[1]);


            yRotation += (Vector3.up * mouseX).y;
            yRotation = Mathf.Clamp(yRotation, xClamp[0], xClamp[1]);

            transform.parent.localEulerAngles = startRotation + (new Vector3(xRotation, yRotation, 0));
        }


    }
}
