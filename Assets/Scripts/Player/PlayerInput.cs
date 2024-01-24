using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }
    public float mouseXInput { get; private set; }
    public float mouseYInput { get; private set; }
    
    public bool sprintHeld { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool interactPressed { get; private set; }
    public bool primaryShootPressed { get; private set; }
    public bool secondaryShootPressed { get; private set; }
    private bool clear;

    #region Singleton
    private static PlayerInput instance;
    void InitializeSingleton()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    public static PlayerInput GetInstance()
    {
        return instance;
    }
    #endregion

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Update()
    {
        ClearInput();
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        clear = true;        
    }

    void ProcessInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseXInput = Input.GetAxis("Mouse X");
        mouseYInput = Input.GetAxis("Mouse Y");

        sprintHeld = sprintHeld || Input.GetButton("Sprint");
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        interactPressed = interactPressed || Input.GetKeyDown(KeyCode.E);
        primaryShootPressed = primaryShootPressed || Input.GetButtonDown("Fire1");
        secondaryShootPressed = secondaryShootPressed || Input.GetButtonDown("Fire2");
    }

    void ClearInput()
    {
        if(!clear)
            return;

        horizontalInput = 0f;
        verticalInput = 0f;
        mouseXInput = 0f;
        mouseYInput = 0f;

        sprintHeld = false;
        jumpPressed = false;
        interactPressed = false;
        primaryShootPressed = false;
        secondaryShootPressed = false;
    }
}