using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private bool invertMouse;

    [Header("Player Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckDistance;

    [Header("Shoot")]
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Rigidbody rocketPrefab;

    [Header("Interact")]
    [SerializeField] private float interactDistance;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactLayerMask;

    [Header("Hold And Release")]
    [SerializeField] private float holdDistance;
    [SerializeField] private Transform attachPointTransform;
    [SerializeField] private LayerMask holdableLayerMask;

    private CharacterController characterController;
    private float horizontalInput, verticalInput, mouseXInput, mouseYInput, camXRotation;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float moveMultiplier = 1f;

    //Ray cast
    private RaycastHit raycastHitInfo;
    private ISelectable selectable;

    //Hold and release
    private bool isHolding = false;
    private IHoldable holdable;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        PlayerGrounded();
        MovePlayer();
        RotatePlayer();
        PlayerJump();

        ShootProjectile();
        ShootRocket();

        Interact();
        HoldAndRelease();
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseXInput = Input.GetAxis("Mouse X");
        mouseYInput = Input.GetAxis("Mouse Y");

        moveMultiplier = Input.GetButton("Sprint") ? sprintMultiplier : 1f;
    }

    void MovePlayer()
    {
        characterController.Move((transform.forward * verticalInput + transform.right * horizontalInput) * moveSpeed * moveMultiplier * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void PlayerGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayerMask);
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * turnSpeed * mouseXInput * Time.deltaTime);

        camXRotation += Time.deltaTime * mouseYInput * turnSpeed * (invertMouse ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -60f, 60f);

        cameraTransform.localRotation = Quaternion.Euler(camXRotation, 0f, 0f);
    }

    void PlayerJump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = jumpVelocity;
        }
    }

    void ShootProjectile()
    {
        if(Input.GetButtonDown("Fire1")){
            Rigidbody projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            projectileInstance.AddForce(projectileSpawnPoint.forward * shootForce);
            Destroy(projectileInstance.gameObject, 5f);
        }
    }

    void ShootRocket()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Rigidbody rocketInstance = Instantiate(rocketPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            rocketInstance.AddForce(projectileSpawnPoint.forward * shootForce);
            Destroy(rocketInstance.gameObject, 5f);
        }
    }

    void Interact()
    {
        // Three different ways to generate a ray from the center of the screen
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Debug.DrawRay(cam.transform.position, cam.transform.forward * interactDistance, Color.red);
        if(Physics.Raycast(ray, out raycastHitInfo, interactDistance, interactLayerMask))
        {
            selectable = raycastHitInfo.transform.GetComponent<ISelectable>();
            if(selectable != null)
            {
                Debug.Log($"Looking at interactable {raycastHitInfo.transform.name}");
                selectable.OnHoverEnter();

                if(Input.GetKeyDown(KeyCode.E))
                {
                    selectable.OnSelect();
                }
            }
        }

        if(raycastHitInfo.transform == null && selectable != null)
        {
            selectable.OnHoverExit();
            selectable = null;
        }
    }

    void HoldAndRelease()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if(Physics.Raycast(ray, out raycastHitInfo, holdDistance, holdableLayerMask))
        {
            Debug.Log($"Looking at holdable");
            if(raycastHitInfo.transform.TryGetComponent<IHoldable>(out holdable))
            {
                Debug.Log($"Looking at holdable {raycastHitInfo.transform.name}");

                if(Input.GetKeyDown(KeyCode.E) && !isHolding)
                {
                    holdable.OnHold(attachPointTransform);
                    isHolding = true;
                    return;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && isHolding)
        {
            holdable.OnRelease();
            isHolding = false;
        }
    }
}
