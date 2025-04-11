using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // Componentes
    private Rigidbody rb;
    private Animator animator;

    // Configuración de movimiento
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float jumpForce = 5f;

    // Estado
    private bool canJump = false;
    private bool isRunning = false;
    private bool isDashing = false;
    private bool canDash = true;
    private float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleDash();
    }

    private void HandleMovement()
    {
        if (isDashing) return; // No moverse normalmente si estás en dash

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        bool isMoving = vertical != 0 || horizontal != 0;

        // Correr
        if (isMoving)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift);
            currentSpeed = isRunning ? runSpeed : walkSpeed;
        }
        else
        {
            currentSpeed = 0f;
            isRunning = false;
        }

        // Movimiento
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 movement = direction * currentSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Rotación con el mouse
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0f, mouseX * rotationSpeed * Time.deltaTime, 0f);

        // Animaciones
        animator.SetFloat("Speed", direction.magnitude * currentSpeed);
        animator.SetBool("isRunning", isRunning);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
        }
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector3 dashDirection = transform.forward;
        float timer = 0f;

        animator.SetTrigger("Dash"); // Necesitarás un trigger llamado "Dash" en tu Animator

        while (timer < dashDuration)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            timer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        rb.linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
