using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorActionable : Actionable
{
    public Animator doorAnimator;

    public override void act()
    {
        doorAnimator.SetBool("isOpening", true);
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
