//using System;
//using UnityEngine;

//public class ScorpionAI : MonoBehaviour
//{
//    private Vector3 startPos;
//    private bool movingRight = true;

//    [Header("Movimiento")]
//    public float speed = 1f;
//    public float moveDistance = 0.5f;

//    [Header("Daño al jugador")]
//    public int damage = 1;
//    public float damageCooldown = 1f;
//    private float lastDamageTime = -999f;

//    [Header("Knockback")]
//    public float knockbackForce = 3f;

//    [Header("Vida del escorpión")]
//    public int maxHealth = 3;
//    private int currentHealth;

//    [Header("Audio")]
//    public AudioClip hurtSound;   // Sonido cuando recibe daño
//    private AudioSource audioSource;

//    void Start()
//    {
//        startPos = transform.position;
//        currentHealth = maxHealth;

//        // Si el escorpión no tiene AudioSource, se agrega
//        audioSource = GetComponent<AudioSource>();
//        if (audioSource == null)
//            audioSource = gameObject.AddComponent<AudioSource>();
//    }

//    void Update()
//    {
//        // Movimiento simple de patrulla
//        float step = speed * Time.deltaTime * (movingRight ? 1 : -1);
//        transform.Translate(step, 0, 0);

//        if (movingRight && transform.position.x >= startPos.x + moveDistance)
//        {
//            Flip();
//        }
//        else if (!movingRight && transform.position.x <= startPos.x - moveDistance)
//        {
//            Flip();
//        }
//    }

//    void Flip()
//    {
//        movingRight = !movingRight;
//        Vector3 scale = transform.localScale;
//        scale.x *= -1;
//        transform.localScale = scale;
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        // Cuando choca con el jugador, hace daño
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            if (Time.time - lastDamageTime < damageCooldown) return;

//            PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
//            if (player != null)
//            {
//                Vector2 knockDir = (collision.transform.position - transform.position).normalized;
//                player.TakeDamage(damage, knockDir, knockbackForce);
//                lastDamageTime = Time.time;
//            }
//        }
//    }

//    // 🔹 Método público para recibir daño (lo llamará el PlayerAttack)
//    public void TakeDamage(int amount)
//    {
//        currentHealth -= amount;

//        // 🔊 Reproduce sonido de dolor
//        if (hurtSound != null)
//            audioSource.PlayOneShot(hurtSound);

//        if (currentHealth <= 0)
//        {
//            Die();
//        }
//    }

//    void Die()
//    {
//        Debug.Log("El escorpión ha muerto");
//        Destroy(gameObject);
//    }
//}

////public class SwordHitbox : MonoBehaviour
////{
////    public int damage = 1; // Daño que causa el golpe

////    private void OnTriggerEnter2D(Collider2D collision)
////    {
////        if (collision.CompareTag("Enemy")) // Asegúrate que el escorpión tenga este tag
////        {
////            ScorpionAI scorpion = collision.GetComponent<ScorpionAI>();
////            if (scorpion != null)
////            {
////                scorpion.TakeDamage(damage);
////            }
////        }
////    }
////}