using System.Collections;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public MonoBehaviour EnemyScase;
    private Collider2D enemyCollider;
    public float stunDuration = 1f; // Duration to be stunned
    public float knockBackForce = 10f; // Force of the knock-back
    public float knockBackDuration = 0.2f; // Duration of the knock-back effect
    public float blinkDuration = 0.1f; // Duration of each blink
    public int blinkTimes = 3; // Number of blinks

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer component missing on enemy.");
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(BlinkRed());
            StartCoroutine(KnockBackAndStun());
        }
    }

    void Die()
    {
        this.enabled = false;
        if (EnemyScase != null)
        {
            EnemyScase.enabled = false;
        }
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false; // Disabling the collider
        }
        StartCoroutine(TurnRedAndDisableSprite(1f));
    }

    IEnumerator KnockBackAndStun()
    {
        if (rb != null)
        {
            Vector2 knockBackDirection = (transform.position - GameObject.FindWithTag("Player").transform.position).normalized;
            rb.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(knockBackDuration); // Wait for knock-back effect to complete

            rb.velocity = Vector2.zero; // Stop the enemy's movement
        }

        if (EnemyScase != null)
        {
            EnemyScase.GetComponent<EnemyScase>().isStunned = true;
            yield return new WaitForSeconds(stunDuration);
            EnemyScase.GetComponent<EnemyScase>().isStunned = false;
        }
    }

    IEnumerator BlinkRed()
    {
        for (int i = 0; i < blinkTimes; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    IEnumerator TurnRedAndDisableSprite(float delay)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(delay);
        spriteRenderer.enabled = false; // Disabling the sprite
    }

    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}