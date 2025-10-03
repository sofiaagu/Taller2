using UnityEngine;

public class EspadaHitbox : MonoBehaviour
{
    public int damage = 1; // Daño que hace la espada

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Scorpion")) 
        {
            ScorpionAI scorpion = other.GetComponent<ScorpionAI>();
            if (scorpion != null)
            {
                scorpion.TakeDamage(damage); // Llama al método de ScorpionAI
            }
        }
    }
}
