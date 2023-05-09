using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    public float baseMoveSpeed;
    public float playerHeight;
    public float moveSpeed;
    public float groundDrag;
    public float sprintCost;
    public float jumpCost;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplyer;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Movement Vars")]
    bool isReadyToJump = true;
    public LayerMask whatIsGround;
    bool grounded;

    float maxStamina = 100.0f;
    float stamina = 100.0f;
    bool exhausted = false;
    float exhaustedCooldown = 2.5f;
    bool isMoving = false;
    bool isRunning = false;
    bool wasRunning = false;

    [Header("References")]
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // TEMP
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI StaminaText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MoveInput();
        SpeedControl();

        Vector3 temp = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (temp.magnitude <= 0.0f)
            isMoving = false;

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        HealthText.text = string.Format("Health: {0:n1}", PlayerData.Instance.GetHealth);
        StaminaText.text = string.Format("Stamina: {0:n1}", stamina);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MoveInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) 
            && isReadyToJump 
            && grounded
            && stamina >= jumpCost)
        {
            isReadyToJump = false;
            
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (grounded
            && !exhausted
            && Input.GetKey(sprintKey))
        {
            moveSpeed = baseMoveSpeed * 1.5f;
            isRunning = true;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
            isRunning = false;
        }

        CheckStatus();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            isMoving = true;
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplyer, ForceMode.Force);
            isMoving = true;
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limited = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limited.x, rb.velocity.y, limited.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        stamina -= jumpCost;
    }

    private void ResetJump()
    {
        isReadyToJump = true;
    }

    public void CheckStatus()
    {
        if (isRunning)
        {
            stamina -= sprintCost;
            wasRunning = true;
            if (stamina <= 0.0f)
            {
                exhausted = true;
            }
        }
        else if (wasRunning)
        {
            Invoke(nameof(ResetExhausted), exhaustedCooldown);
            wasRunning = false;
        }
        
        if (!isMoving && !exhausted && stamina < maxStamina && grounded)
        {
            stamina += sprintCost / 5.0f;
        }
        else if (!isRunning && !exhausted && stamina < maxStamina && grounded)
        {
            stamina += sprintCost / 10.0f;
        }
    }

    public void ResetExhausted()
    {
        exhausted = false;
    }
}

