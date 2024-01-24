using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertMouse;

    private PlayerInput playerInput;

    public float camXRotation { get; private set; }
    void Start()
    {
        playerInput = PlayerInput.GetInstance();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        camXRotation += playerInput.mouseYInput * turnSpeed * Time.deltaTime * (invertMouse ? 1f : -1f);
        camXRotation = Mathf.Clamp(camXRotation, -45f, 45f);

        transform.localRotation = Quaternion.Euler(camXRotation, 0f, 0f);
    }
}
