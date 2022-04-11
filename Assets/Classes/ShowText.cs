using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public bool isCamera;
    private CamerasController camerasController;
    private MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        camerasController = GameObject.Find("Player").GetComponent<CamerasController>();
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(camerasController.curCamController != null)
        {
            mr.enabled = true;
        }
        else
        {
            if (isCamera)
            {
                mr.enabled = camerasController.canOpenCamera;
            }
            else
            {
                mr.enabled = (camerasController.curCam.transform.position - transform.position).magnitude < 5;
            }
        }

        if (mr.enabled)
        {
            transform.LookAt(transform.position - (camerasController.curCam.transform.position - transform.position), Vector3.up);

        }

    }
}
