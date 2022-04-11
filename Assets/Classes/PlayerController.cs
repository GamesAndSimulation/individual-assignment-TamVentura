using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Camera camera;
    public AudioSource walkSource;
    public AudioSource jumpSource;

    public Animator animator;

    public GameObject prefab;

    [Header("Fisica")]
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float acceleration = 30f;
    public float drift = 0.05f;
    public float maxWalkSpeed = 5f;


    [Header("GroundChecks")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;

    public LayerMask movableGroundMask;

    public LayerMask grabbableMask;
    public Transform grabPosition;



    [Header("WallChecks")]
    public LayerMask WallMask;

    private GameObject grabbing;





    private PlayerInputAction input;

    private bool isGrounded = false;
    private bool isMovableGrounded = false; 
    private Vector3 velocity;

    private LayerMask actionableMask;





    void Start()
    {
        actionableMask = LayerMask.GetMask("Actionable");
        input = new PlayerInputAction();
        input.Ground.Enable();
        input.Ground.Grab.performed += grabPressed;
        input.Ground.Throw.performed += throwPressed;
        input.Ground.Act.performed += actPressed;


    }

    private void grabPressed(InputAction.CallbackContext obj)
    {
        if (Time.deltaTime > 0 && camera.enabled)
        {

            RaycastHit hit;

            if (grabbing != null)
            {
                grabbing.GetComponent<Rigidbody>().isKinematic = false;
                grabbing.GetComponent<Rigidbody>().velocity += (transform.TransformDirection(velocity) * 2);
                grabbing.GetComponent<Collider>().isTrigger = false;
                grabbing = null;

            }
            else
            {
                if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 5, grabbableMask))
                {
                    grabbing = hit.transform.gameObject;
                    grabbing.GetComponent<Rigidbody>().isKinematic = true;
                    grabbing.GetComponent<Collider>().isTrigger = true;
                }
            }





            // Does the ray intersect any objects excluding the player layer
            /*if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 100, WallMask))
            {
                Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 100);
                Debug.Log("Did Hit");
                GameObject clone = Instantiate(prefab, hit.point, new Quaternion(0, 0, 0, 0));
                clone.transform.position += hit.point - clone.GetComponent<Renderer>().bounds.center;


                GameObject text = new GameObject();
                TextMesh t = text.AddComponent<TextMesh>();
                t.text = "new text set";
                t.fontSize = 50;
                t.transform.localScale = Vector3.one / 30;
                t.alignment = TextAlignment.Center;
                t.anchor = TextAnchor.MiddleCenter;
                var height = hit.point - clone.GetComponent<Renderer>().bounds.center;
                height.x = 0;
                height.z = 0;


                Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 100, WallMask);

                t.transform.position = hit.point - height;
                t.transform.rotation = camera.transform.rotation;
            }
            else
            {
                Debug.DrawRay(transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }*/

        }
    }

   

    private void throwPressed(InputAction.CallbackContext obj)
    {
        if (Time.deltaTime > 0 && camera.enabled)
        {

            if (grabbing != null)
            {
                grabbing.GetComponent<Rigidbody>().isKinematic = false;
                grabbing.GetComponent<Rigidbody>().velocity += (transform.TransformDirection(velocity) * 2) + (camera.transform.forward * 30);
                grabbing.GetComponent<Collider>().isTrigger = false;
                grabbing = null;

            }

        }
    }

    private void actPressed(InputAction.CallbackContext obj)
    {
        if (Time.deltaTime > 0 && camera.enabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 5))
            {
                
                if (actionableMask == (actionableMask | (1 << hit.transform.gameObject.layer)))
                {
                    hit.transform.gameObject.GetComponent<Actionable>().act();
                }
            }


        }
    }




    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, camera.transform.TransformDirection(Vector3.forward) * 100, Color.yellow);

        bool isJumping = input.Ground.Jump.ReadValue<float>() > 0;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask, QueryTriggerInteraction.Ignore);

        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
        float currentSpeed = maxWalkSpeed;
        float currentAcceleration = acceleration;

        //Read Movement XZ
        if (Time.deltaTime > 0)
        {
            Vector2 vector = input.Ground.Walk.ReadValue<Vector2>();
            vector.Normalize();
            vector *= currentAcceleration;

            float y = velocity.y;
            velocity.y = 0;
            //Move and smooth movement XZ
            velocity += new Vector3(vector.x, 0, vector.y);
            velocity.x -= velocity.x * drift;
            velocity.z -= velocity.z * drift;

            velocity.x = Mathf.Clamp(velocity.x, -currentSpeed, currentSpeed);
            velocity.z = Mathf.Clamp(velocity.z, -currentSpeed, currentSpeed);
            if (velocity.magnitude > currentSpeed)
            {
                velocity.Normalize();
                velocity *= currentSpeed;
            }
            velocity.y = y;
            
        }


        //Apply gravity things
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        if (isJumping && isGrounded)
        {
            animator.SetBool("IsJumping", true);

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpSource.Play();
            walkSource.volume = 0;
        }

        if (isGrounded && velocity.y < 0)
        {
            walkSource.volume = new Vector3(velocity.x, 0, velocity.z).magnitude;
        }
        else
        {
            walkSource.volume = 0;
        }

        if (new Vector3(velocity.x,0,velocity.z).magnitude > 0.5)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        velocity.y += gravity * Time.deltaTime;



        //Move
        characterController.Move((transform.TransformDirection(velocity)) * Time.deltaTime);


        if (Time.deltaTime > 0)
        {
            if (grabbing)
            {
                grabbing.transform.rotation = transform.rotation;
                grabbing.transform.position = grabPosition.position;
            }
        }

    }

    void LateUpdate()
    {
        characterController.enabled = false;

        isMovableGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, movableGroundMask);
        if (isMovableGrounded)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100, movableGroundMask))
            {
                MovableGround movableGround = hit.transform.gameObject.GetComponent<MovableGround>();
                if(movableGround.isMoving)
                    transform.position += movableGround.move * Time.deltaTime;
            }
        }

        characterController.enabled = true;

    }


    float pushPower = 2.0f;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (grabbableMask == (grabbableMask | (1 << hit.gameObject.layer)))
        {
            Rigidbody body = hit.collider.attachedRigidbody;



            // no rigidbody
            if (body == null || body.isKinematic)
            {
                return;
            }

            // We dont want to push objects below us
            if (hit.moveDirection.y < -0.3)
            {
                return;
            }

            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            Vector3 pushDir = body.position - transform.position;//new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            pushDir = new Vector3(pushDir.x, 0, pushDir.z);

            // If you know how fast your character is trying to move,
            // then you can also multiply the push velocity by that.

            // Apply the push

            float localPushPower = pushPower;
            body.velocity += pushDir * localPushPower;
        }
    }

    private void OnDestroy()
    {
        input.Dispose();
    }

}
