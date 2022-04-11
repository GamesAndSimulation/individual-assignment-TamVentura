using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private LayerMask movableMask;
    // Start is called before the first frame update
    void Start()
    {
        movableMask = LayerMask.GetMask("MovableGround");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 7))
        {
            GameObject hitObject = hit.transform.gameObject;
            if(hitObject.GetComponent<MovableGround>() != null)
            {
                MovableGround movableGround = hitObject.GetComponent<MovableGround>();
                if (movableGround.isMoving)
                    transform.position += (movableGround.move * Time.deltaTime);
            }
            
        }

         
    }
}
