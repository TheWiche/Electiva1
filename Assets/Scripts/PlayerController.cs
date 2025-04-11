using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SimplePlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    
    // Variables de configuración del movimiento, rotación y salto
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 120f;
    public float jumpForce = 7f;
    public float dashForce = 40f;
    public float dashCooldown = 3f;

    private bool isGrounded;
    private bool canDash;
    private float lastDash;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        MoveAndRotate();
        Jump();
        dash();
    }
    
    // Mover y rotar el jugador
    void MoveAndRotate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);
        Vector3 direction = new Vector3(0f, 0f, vertical).normalized;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;   
        Vector3 move = transform.TransformDirection(direction) * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
        // Animaciones de caminar y correr
        if (vertical != 0f)
        {
            if (currentSpeed == runSpeed)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }
    
    // Hacemos el jump si se presiona la tecla espacio
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
            animator.SetBool("isGround", false);
        }
    }

    //Hacemos el dash si se presiona la tecla control
    void dash()
    {
        // Hacemos el dash si se cumple la condición
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash)
        {
            rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
            canDash = false;
            lastDash = Time.time;
        } 
        // Verificamos si el jugador puede hacer el dash
        if (canDash == false){
            if (Time.time - lastDash > dashCooldown)
            {
                canDash = true;
                lastDash = Time.time;
            }
        }
    }
    
    // Detectamos si el jugador está colisionado
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            // La animación de jump se desactiva cuando el jugador está en el suelo
            animator.SetBool("isJumping", false);
            animator.SetBool("isGround", true);
        }
    }
}