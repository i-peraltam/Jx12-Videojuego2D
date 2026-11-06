using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float fireRate = 0.25f;
    private float nextFireTime = 0f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        Transform visual = transform.Find("Visual");

        if (visual != null)
        {
            spriteRenderer = visual.GetComponent<SpriteRenderer>();
            animator = visual.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.isPressed &&
            Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        GameObject nuevaBala = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Bullet bullet = nuevaBala.GetComponent<Bullet>();

        float direction = 1f;

        if (spriteRenderer != null && spriteRenderer.flipX)
        {
            direction = -1f;
        }

        bullet.SetDirection(direction);
    }
}