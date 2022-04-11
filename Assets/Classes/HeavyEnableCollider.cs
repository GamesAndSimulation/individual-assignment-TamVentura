using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnableCollider : MonoBehaviour
{
    public Animator doorAnimator;
    private LayerMask grabbableLayer;
    // Start is called before the first frame update
    void Start()
    {
        grabbableLayer = LayerMask.GetMask("Grabbable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (grabbableLayer == (grabbableLayer | (1 << other.gameObject.layer)))
        {
            doorAnimator.SetBool("isOpening", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("HERE");
        if (grabbableLayer == (grabbableLayer | (1 << other.gameObject.layer)))
        {
            doorAnimator.SetBool("isOpening", false);
        }
    }
}
