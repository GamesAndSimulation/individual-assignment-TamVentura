using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCamera : Actionable
{

    public CameraController cameraController;
    public CamerasController controller;
    public override void act()
    {
        controller.openCamera(cameraController);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
