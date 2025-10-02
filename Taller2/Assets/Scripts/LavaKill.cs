using UnityEngine;

public class LavaKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Lava normal → quita 1 vida
        if (this.CompareTag("Lava"))
        {
            Debug.Log("Player tocó Lava normal → pierde 1 vida");
            GameManager.Instance.QuitarVida(1);
        }

        // PisoLava → pierde toda la vida
        if (this.CompareTag("PisoLava"))
        {
            Debug.Log("Player cayó en PisoLava → pierde todas las vidas");
            GameManager.Instance.QuitarVida(GameManager.Instance.currentHealth);
        }
    }
}
