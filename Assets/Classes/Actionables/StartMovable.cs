using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovable : Actionable
{

    public MovableGround movable;
    public override void act()
    {
        movable.isMoving = !movable.isMoving;
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
