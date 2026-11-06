using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;

    private Rigidbody2D rb;

    public void SetDirection(float direction)
    {
        rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = new Vector2(direction * speed, 0f);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}