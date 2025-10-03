using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public AudioClip AttackSound;   // Sonido de ataque (desde el inspector)
    private AudioSource audioSource;

    [Header("Ataque")]
    public KeyCode attackKey = KeyCode.K;   // Tecla para atacar
    public GameObject espadaHitbox;         // Empty con collider2D como Trigger
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        espadaHitbox.SetActive(false); // Al inicio desactivado

        // Si el jugador no tiene AudioSource, se crea uno
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            animator.SetTrigger("attack");
            isAttacking = true;

            // 🔊 Reproducir sonido de ataque
            if (AttackSound != null)
                audioSource.PlayOneShot(AttackSound);

            StartCoroutine(ActivarHitbox());
        }
    }

    private System.Collections.IEnumerator ActivarHitbox()
    {
        yield return new WaitForSeconds(0.2f);
        espadaHitbox.SetActive(true); // Activa el collider
        yield return new WaitForSeconds(0.3f);
        espadaHitbox.SetActive(false); // Desactiva después
        isAttacking = false;
    }
}