using UnityEngine;

public class LavaKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar que el que toca sea Player Y que este objeto sea Lava
        if (collision.CompareTag("Player") && this.CompareTag("Lava"))
        {
            Debug.Log("Player murió en la lava!");
            Destroy(collision.gameObject);
        }
    }
}
