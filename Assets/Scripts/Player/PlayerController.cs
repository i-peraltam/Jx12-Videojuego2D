using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform visual;
    private Transform firePoint;
    private Rigidbody2D rb;

    private float firePointXOriginal;

    [SerializeField] private float jumpForce = 7f;

    private bool isGrounded = false;

    void Awake()
    {
        visual = transform.Find("Visual");
        firePoint = transform.Find("FirePoint");
        rb = GetComponent<Rigidbody2D>();

        if (visual != null)
        {
            animator = visual.GetComponent<Animator>();
            spriteRenderer = visual.GetComponent<SpriteRenderer>();
        }

        if (firePoint != null)
        {
            firePointXOriginal = Mathf.Abs(firePoint.localPosition.x);
        }
    }

    void Update()
    {
        float movimientoHorizontal = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
            {
                movimientoHorizontal = -1f;
            }

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
            {
                movimientoHorizontal = 1f;
            }

            if ((Keyboard.current.wKey.wasPressedThisFrame ||
     Keyboard.current.upArrowKey.wasPressedThisFrame) && isGrounded)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    jumpForce
                );

                isGrounded = false;
            }
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(movimientoHorizontal));
        }

        if (movimientoHorizontal < 0)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = true;
            }

            if (firePoint != null)
            {
                Vector3 posicion = firePoint.localPosition;
                posicion.x = -firePointXOriginal;
                firePoint.localPosition = posicion;
            }
        }
        else if (movimientoHorizontal > 0)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }

            if (firePoint != null)
            {
                Vector3 posicion = firePoint.localPosition;
                posicion.x = firePointXOriginal;
                firePoint.localPosition = posicion;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}