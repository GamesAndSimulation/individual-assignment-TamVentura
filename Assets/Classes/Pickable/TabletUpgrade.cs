using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUpgrade : Pickable
{
    public CamerasController camerasController;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }

    public override void picked()
    {
        camerasController.canOpenCamera = true;

    }

}
