using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    [Header("Ataque")]
    public KeyCode attackKey = KeyCode.K;   // Tecla para atacar
    public GameObject espadaHitbox;         // Asigna tu Empty con collider en el Inspector
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        espadaHitbox.SetActive(false); // Al inicio desactivado
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            animator.SetTrigger("attack");
            isAttacking = true;
            StartCoroutine(ActivarHitbox());
        }
    }

    private System.Collections.IEnumerator ActivarHitbox()
    {
        yield return new WaitForSeconds(0.2f);
        espadaHitbox.SetActive(true); // Activa el collider
        yield return new WaitForSeconds(0.3f); // tiempo de golpe (aj�stalo al largo de la animaci�n)
        espadaHitbox.SetActive(false); // Desactiva despu�s
        isAttacking = false;
    }
}
