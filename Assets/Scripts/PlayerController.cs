using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.3f;
    [SerializeField] float leftBoundPadding = 0f;
    [SerializeField] float rightBoundPadding = 0f;
    [SerializeField] float bottomBoundPadding = 0f;
    [SerializeField] float topBoundPadding = 0f;

    InputAction moveAction;
    Vector3 moveVector;
    Vector2 minBound;
    Vector2 maxBound;

    Shooter playerShooter;
    InputAction fireAction;
    Animator playerAnimator;
    AudioManager audioManager;
    ChargeBarManager chargeBarManager;

    void Awake()
    {
        playerShooter = GetComponent<Shooter>();
        fireAction = InputSystem.actions.FindAction("Fire");
        moveAction = InputSystem.actions.FindAction("Move");
        playerAnimator = FindFirstObjectByType<Animator>();
        audioManager = FindFirstObjectByType<AudioManager>();
        chargeBarManager = FindFirstObjectByType<ChargeBarManager>();
    }

    void Start()
    {
        InitializeBound();
    }

    void InitializeBound()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        MovePlayer();
        FireShooter();
    }

    void MovePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        Vector3 newPos = transform.position + (moveVector * moveSpeed * Time.deltaTime);

        newPos.x = Math.Clamp(newPos.x, minBound.x + leftBoundPadding, maxBound.x - rightBoundPadding);
        newPos.y = Math.Clamp(newPos.y, minBound.y + bottomBoundPadding, maxBound.y - topBoundPadding);

        transform.position = newPos;
    }

    void FireShooter()
    {
        // playerShooter.isFiring = fireAction.IsPressed();

        if (playerShooter.isCharging)
        {
            playerShooter.isFiring = false;
            audioManager.PlayChargeUpSFX();

            if (playerShooter.chargeFiring)
            {   
                audioManager.PlayChargingShotSFX();
                playerShooter.ChargeFire();
                playerShooter.chargeFiring = false;
                playerShooter.isCharging = false;
                playerAnimator.SetBool("isCharging", false);
                chargeBarManager.StopChargeAnimation();
            }
        }
        else
        {   
            playerShooter.isFiring = fireAction.IsPressed();
        }
    }

    void OnEnable()
    {
        fireAction.Enable();

        fireAction.performed += OnHoldPerformed;
        fireAction.canceled += OnHoldCanceled;
    }

    void OnDisable()
    {
        fireAction.performed -= OnHoldPerformed;
        fireAction.canceled -= OnHoldCanceled;

        fireAction.Disable();
    }

    void OnHoldPerformed(InputAction.CallbackContext ctx)
    {
        print("Charge attack!");
        playerShooter.isCharging = true;
        playerAnimator.SetBool("isCharging", true);
        chargeBarManager.PlayChargeAnimation();
    }

    void OnHoldCanceled(InputAction.CallbackContext ctx)
    {
        print("Normal attack");

        if (playerShooter.isCharging)
        {
            playerShooter.chargeFiring = true;
        }
    }
}
