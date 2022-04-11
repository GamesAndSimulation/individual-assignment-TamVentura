using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOverlay : MonoBehaviour
{

    public CamerasController CamerasController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Canvas>().enabled = (CamerasController.curCamController != null);
    }
}
