using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleFloor : MonoBehaviour
{
    public List<Camera> cameras;
    public CamerasController camerasController;

    private MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (cameras.Contains(camerasController.curCam))
        {
            renderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;

        }
    }
}
