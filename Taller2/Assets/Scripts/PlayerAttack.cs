using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    [Header("Ataque")]
    public KeyCode attackKey = KeyCode.K;   // Tecla para atacar
    private bool isAttacking = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Siempre puedes atacar aunque no haya escorpi�n
        if (Input.GetKeyDown(attackKey))
        {
            animator.SetTrigger("attack");
            isAttacking = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el jugador est� tocando un escorpi�n y presiona la tecla
        if (collision.gameObject.CompareTag("Scorpion") && Input.GetKeyDown(attackKey))
        {
            Destroy(collision.gameObject, 0.5f); // Destruye el escorpi�n despu�s de la animaci�n
            isAttacking = false;
        }
    }
}
