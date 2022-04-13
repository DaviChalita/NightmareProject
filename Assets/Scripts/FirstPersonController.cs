using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;

    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);

    private bool ShouldJump =>
        Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField]
    private bool canSprint = true;

    [SerializeField]
    private bool canJump = false;

    [SerializeField]
    private bool canUseHeadbob = true;

    [SerializeField]
    private bool canUseFootsteps = true;

    [SerializeField]
    private bool canInteract = true;

    [Header("Controls")]
    [SerializeField]
    private KeyCode sprintKey = KeyCode.LeftShift;

    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;

    [SerializeField]
    private KeyCode interactKey = KeyCode.E;

    [Header("Movement Parameters")]
    [SerializeField]
    private float walkSpeed = 3.0f;

    [SerializeField]
    private float sprintSpeed = 6.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)]
    private float lookSpeedX = 2.0f;

    [SerializeField, Range(1, 10)]
    private float lookSpeedY = 2.0f;

    [SerializeField, Range(1, 180)]
    private float upperLookLimit = 80.0f;

    [SerializeField, Range(1, 180)]
    private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField]
    private float jumpForce = 8.0f;

    [SerializeField]
    private float gravity = 30.0f;

    [Header("Headbob Parameters")]
    [SerializeField]
    private float walkBobSpeed = 14f;

    [SerializeField]
    private float walkBobAmount = 0.005f;

    [SerializeField]
    private float sprintBobSpeed = 18f;

    [SerializeField]
    private float sprintBobAmount = 0.01f;

    [Header("Footsteps Parameters")]
    [SerializeField]
    private float baseStepSpeed = 0.5f;

    [SerializeField]
    private float sprintStepMultiplier = 1.5f;

    [SerializeField]
    private AudioSource footstepAudioSource = default;

    [SerializeField]
    private AudioClip[] grassClips = default;

    [SerializeField]
    private AudioClip[] concreteClips = default;

    [Header("Interaction")]
    [SerializeField]
    private Vector3 interactionRayPoint = default;

    [SerializeField]
    private float interactionDistance = default;

    [SerializeField]
    private LayerMask interactionLayer = default;

    private Interactable currentInteractable;

    private float footstepTimer = 0;

    private float GetCurrentOffset =>
        IsSprinting ? baseStepSpeed / sprintStepMultiplier : baseStepSpeed;

    private float defaultYPos = 0;

    private float timer;

    private Camera playerCamera;

    private CharacterController characterController;

    private Vector3 moveDirection;

    private Vector2 currentInput;

    private float rotationX = 0;

    // Start is called before the first frame update
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        footstepAudioSource = GetComponent<AudioSource>();
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (canJump) HandleJump();

            if (canUseHeadbob) HandleHeadbob();

            if (canUseFootsteps) HandleFootsteps();

            if (canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        currentInput =
            new Vector2((IsSprinting ? sprintSpeed : walkSpeed) *
                Input.GetAxis("Vertical"),
                (IsSprinting ? sprintSpeed : walkSpeed) *
                Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection =
            (transform.TransformDirection(Vector3.forward) * currentInput.x) +
            (transform.TransformDirection(Vector3.right) * currentInput.y);
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation =
            Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *=
            Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if (ShouldJump) moveDirection.y = jumpForce;
    }

    private void HandleHeadbob()
    {
        if (!characterController.isGrounded) return;

        if (
            Mathf.Abs(moveDirection.x) > 0.1f ||
            Mathf.Abs(moveDirection.z) > 0.1f
        )
        {
            timer +=
                Time.deltaTime * (IsSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition =
                new Vector3(playerCamera.transform.localPosition.x,
                    defaultYPos +
                    Mathf.Sin(timer) *
                    (IsSprinting ? sprintBobAmount : walkBobAmount),
                    playerCamera.transform.localPosition.z);
        }
    }

    private void HandleFootsteps()
    {
        if (!characterController.isGrounded || currentInput == Vector2.zero)
            return;

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            if (
                Physics
                    .Raycast(playerCamera.transform.position,
                    Vector3.down,
                    out RaycastHit hit,
                    3)
            )
            {
                // Debug.Log("Collider: "+hit.collider.tag);
                switch (hit.collider.tag)
                {
                    case "Footsteps/GRASS":
                        footstepAudioSource
                            .PlayOneShot(grassClips[Random
                                .Range(0, grassClips.Length - 1)]);
                        break;
                    case "Footsteps/CONCRETE":
                        footstepAudioSource
                            .PlayOneShot(concreteClips[Random
                                .Range(0, concreteClips.Length - 1)],
                            0.2f);
                        break;
                    default:
                        break;
                }
            }
            footstepTimer = GetCurrentOffset;
        }
    }

    private void HandleInteractionCheck()
    {
        if (
            Physics
                .Raycast(playerCamera.ViewportPointToRay(interactionRayPoint),
                out RaycastHit hit,
                interactionDistance)
        )
        {
            if (
                hit.collider.gameObject.layer == 6 &&
                (
                currentInteractable == null ||
                hit.collider.gameObject.GetInstanceID() !=
                currentInteractable.GetInstanceID()
                )
            )
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable) currentInteractable.OnFocus();
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractionInput()
    {
        if (
            Input.GetKeyDown(interactKey) &&
            currentInteractable != null &&
            Physics
                .Raycast(playerCamera.ViewportPointToRay(interactionRayPoint),
                out RaycastHit hit,
                interactionDistance,
                interactionLayer)
        )
        {
            currentInteractable.OnInteract();
        }
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        if (
            characterController.velocity.y < -1 &&
            characterController.isGrounded
        ) moveDirection.y = 0;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
