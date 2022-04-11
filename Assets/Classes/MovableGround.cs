using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableGround : MonoBehaviour
{


    [HideInInspector] // Hides var below
    public Vector3 move;

    public AudioSource moveSource;


    public Transform[] positions;
    public float speed = 1;
    public bool isMoving = true;

    private int positionId = 1;
    private float distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        positionId = 1;
        Vector3 position = positions[positionId].localPosition - transform.localPosition;

        distance = position.magnitude;
        move = position.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            moveSource.volume = 1;
            if (distance <= 0)
            {
                positionId = (positionId + 1) % positions.Length;
                Vector3 position = positions[positionId].localPosition - transform.localPosition;
                distance = position.magnitude;
                move = position.normalized * speed;
            }

            transform.localPosition += move * Time.deltaTime;
            distance -= move.magnitude * Time.deltaTime;
        }
        else
        {
            moveSource.volume = 0;

        }

    }
}
