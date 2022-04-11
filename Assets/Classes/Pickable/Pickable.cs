using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
    // Start is called before the first frame update
    LayerMask playerMask;
    void Start()
    {
        playerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerMask == (playerMask | (1 << other.gameObject.layer)))
        {
            picked();
            Destroy(gameObject);
        }
    }

    public abstract void picked();
}
