using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    public Animator doorAnimator;
    public LayerMask playerMask;

    bool isTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger && playerMask == (playerMask | (1 << other.gameObject.layer)))
        {
            doorAnimator.SetBool("isOpening", true);
            isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (playerMask == (playerMask | (1 << other.gameObject.layer)))
        {
            doorAnimator.SetBool("isOpening", false);
            isTrigger = false;
        }
    }
}
