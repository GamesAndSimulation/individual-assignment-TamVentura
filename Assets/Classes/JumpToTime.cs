using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class JumpToTime : MonoBehaviour
{
    private PlayerInputAction input;
    private static Vector3 targetPosition = new Vector3(23.55f, 2f, -9.81f); //here you store the position you want to teleport your player to
    private bool firstUpdate = true;

    private static bool cameraUpgrade = false;


    private void SceneLoaded()
    {
        if (targetPosition != null)
            transform.position = targetPosition;
        Debug.Log(targetPosition);
        GetComponent<CamerasController>().canOpenCamera = cameraUpgrade;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.sceneLoaded += SceneLoaded; //You add your method to the delegate
        input = new PlayerInputAction();
        input.Global.Enable();
        input.Global.JumpToLevel1.performed += JumpToLevel1;
        input.Global.JumpToLevel2.performed += JumpToLevel2;
        input.Global.JumpToLevel3.performed += JumpToLevel3;

    }



    private void JumpToLevel1(InputAction.CallbackContext obj)
    {
        cameraUpgrade = false;

        targetPosition = new Vector3(23.55f, 2f, -9.81f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

    }

    private void JumpToLevel2(InputAction.CallbackContext obj)
    {
        cameraUpgrade = false;

        targetPosition = new Vector3(18.431f, 2f, -81.735f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    

    private void JumpToLevel3(InputAction.CallbackContext obj)
    {
        cameraUpgrade = true;
        targetPosition = new Vector3(28f, 18f, -100f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            SceneLoaded();
        }
    }

    
    private void OnDestroy()
    {
        input.Dispose();
    }


    
}
