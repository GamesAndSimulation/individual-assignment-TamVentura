using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public bool isActive = false;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera.enabled = isActive;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        isActive = true;
        camera.enabled = isActive;
    }

    public void Deactivate()
    {
        isActive = false;
        camera.enabled = isActive;
    }
}
