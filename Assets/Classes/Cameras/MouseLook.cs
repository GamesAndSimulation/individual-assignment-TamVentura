using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSense = 0.1f;
    public Transform playerBody;
    public PlayerInputAction input;
    public Camera camera;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        input = new PlayerInputAction();
        input.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.deltaTime > 0 && camera.enabled)
        {
            Vector2 mouseRotation = input.Ground.Camera.ReadValue<Vector2>();
            float mouseX = mouseRotation.x * mouseSense;// * Time.deltaTime;
            float mouseY = mouseRotation.y * mouseSense;// * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);


            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.localEulerAngles += (Vector3.up * mouseX);
        }
        

    }

    private void OnDestroy()
    {
        input.Dispose();
    }
}
