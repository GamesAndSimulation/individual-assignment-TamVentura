using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Global : MonoBehaviour
{
    public PlayerInputAction input;

    public Text title;
    public GameObject button;
    private bool isPaused = false;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        input = new PlayerInputAction();
        input.Enable();
        input.Global.Pause.performed += pause;
        button.SetActive(false);
    }

    private void pause(InputAction.CallbackContext obj)
    {
        isPaused = !isPaused;

        

        Time.timeScale = isPaused?0:1;
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            button.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            button.SetActive(false);

        }

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        title.text = "TEMPO: " + time;
    }

    public void clickButton()
    {
        title.text = "TESTE2";
    }
}
