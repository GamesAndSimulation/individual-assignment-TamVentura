using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamerasController : MonoBehaviour
{
    public Camera playerCam;
    public Camera[] cameras;
    public LayerMask cameraMask;
    public bool canOpenCamera = true;

    [HideInInspector]
    public Camera curCam;
    [HideInInspector]
    public CameraController curCamController;
    private PlayerInputAction inputActions;
    private LayerMask actionableMask;

    // Start is called before the first frame update
    void Start()
    {
        actionableMask = LayerMask.GetMask("Actionable");

        curCam = playerCam;
        inputActions = new PlayerInputAction();
        inputActions.Enable();
        inputActions.World.CameraEnter.performed += cameraEnter;
        inputActions.World.CameraExit.performed += cameraExit;
    }

    private void cameraEnter(InputAction.CallbackContext obj)
    {
        if (Time.deltaTime > 0 )
        {

            RaycastHit hit;

            if (Physics.Raycast(curCam.transform.position, curCam.transform.TransformDirection(Vector3.forward), out hit, 100))
            {
                if ( (canOpenCamera || curCamController != null )&& cameraMask == (cameraMask | (1 << hit.transform.gameObject.layer)))
                {
                    openCamera(hit.transform.gameObject.GetComponent<CameraController>());
                }
                else if(curCamController != null)
                {
                    if (actionableMask == (actionableMask | (1 << hit.transform.gameObject.layer)))
                    {
                        hit.transform.gameObject.GetComponent<Actionable>().act();
                    }
                }
            }

        }
    }

    public void openCamera(CameraController controller)
    {
        if (curCamController)
            curCamController.Deactivate();
        else
            curCam.enabled = false;

        curCamController = controller;
        curCamController.Activate();
        curCam = curCamController.camera;
    }

    private void cameraExit(InputAction.CallbackContext obj)
    {
        if (Time.deltaTime > 0 && curCamController != null)
        {
            curCamController.Deactivate();
            curCamController = null;
            curCam = playerCam;
            curCam.enabled = true;
        }
    }


    // Update is called once per frame
    void Update()
    {


    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }
}
