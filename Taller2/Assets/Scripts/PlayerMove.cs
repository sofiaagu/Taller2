using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _Rigidbody2D;
    private Animator animator;

    public float horizontal;
    public float vertical;   // por ahora no se usa, puede servir luego para salto
    public float speed = 3f;
    public float jumpForce = 3f;

    private bool isJumping = false;

    public int maxHealth = 5;
    private int currentHealth;

    public float invulnerableTime = 1f; // segundos de invulnerabilidad tras recibir daño
    private bool invulnerable = false;

    // Opcional: fuerza de retroceso cuando recibes daño
    public float defaultKnockbackForce = 3f;

    // Referencia al SpriteRenderer para parpadeo visual
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimiento lateral
        horizontal = Input.GetAxisRaw("Horizontal");

        // Animación de correr (Idle ↔ Run)
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (horizontal > 0) transform.localScale = new Vector3(1, 1, 1);
        if (horizontal < 0) transform.localScale = new Vector3(-1, 1, 1);

        // Salto
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            _Rigidbody2D.linearVelocity = new Vector2(_Rigidbody2D.linearVelocity.x, jumpForce);
            animator.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        _Rigidbody2D.linearVelocity = new Vector2(horizontal * speed, _Rigidbody2D.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }
    public void TakeDamage(int amount, Vector2 knockbackDirection, float knockbackForce = -1f)
    {
        if (invulnerable) return;

        if (knockbackForce <= 0f) knockbackForce = defaultKnockbackForce;

        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        // Animación/feedback
        if (animator != null)
        {
            animator.SetBool("isHurt", true);
            Invoke("ResetHurt", 0.3f); // 0.3 segundos = duración de la animación Hurt
        }


        // Aplicar knockback
        if (_Rigidbody2D != null)
        {
            // asegurar que el vector no sea cero
            if (knockbackDirection.sqrMagnitude == 0f) knockbackDirection = Vector2.up;
            _Rigidbody2D.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Impulse);
        }

        // Iniciar invulnerabilidad temporal y parpadeo visual
        StartCoroutine(InvulnerableRoutine());

        // Revisar muerte
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator InvulnerableRoutine()
    {
        invulnerable = true;
        float t = 0f;
        float flashInterval = 0.12f;

        while (t < invulnerableTime)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(flashInterval);
            t += flashInterval;
        }

        // asegurar visible
        if (spriteRenderer != null) spriteRenderer.enabled = true;
        invulnerable = false;
    }

    private void Die()
    {
        // Aquí defines qué pasa al morir (respawn, reiniciar escena, anim de muerte...)
        Debug.Log("Player muerto");
        if (animator != null) animator.SetTrigger("die");
        // ejemplo: desactivar controles
        this.enabled = false;
        // podrías añadir SceneManager.LoadScene(...) si quieres reiniciar
    }

    // Método opcional público para curar
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
    private void ResetHurt()
    {
        if (animator != null)
            animator.SetBool("isHurt", false);
    }
    // Método para consultar vida (útil para UI)
    public int GetCurrentHealth() { return currentHealth; }
}
