using UnityEngine;

public class ScorpionAI : MonoBehaviour
{
    private Vector3 startPos;
    private bool movingRight = true;

    [Header("Movimiento")]
    public float speed = 1f;          // Velocidad del escorpión
    public float moveDistance = 0.5f; // Distancia máxima desde el punto inicial

    [Header("Daño")]
    public int damage = 1;
    public float damageCooldown = 1f; // tiempo entre daños consecutivos
    private float lastDamageTime = -999f;

    [Header("Knockback")]
    public float knockbackForce = 3f;

    void Start()
    {
        startPos = transform.position; // Guardar posición inicial
    }

    void Update()
    {
        // Mover en X
        float step = speed * Time.deltaTime * (movingRight ? 1 : -1);
        transform.Translate(step, 0, 0);

        // Si se pasa de la distancia, voltear
        if (movingRight && transform.position.x >= startPos.x + moveDistance)
        {
            Flip();
        }
        else if (!movingRight && transform.position.x <= startPos.x - moveDistance)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // voltear sprite
        transform.localScale = scale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime < damageCooldown) return;

            PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
            if (player != null)
            {
                // Calcula dirección de knockback (desde enemigo hacia jugador)
                Vector2 knockDir = (collision.transform.position - transform.position).normalized;
                player.TakeDamage(damage, knockDir, knockbackForce);
                lastDamageTime = Time.time;
            }
        }
    }
}
