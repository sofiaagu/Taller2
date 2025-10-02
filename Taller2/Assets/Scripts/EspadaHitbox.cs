using UnityEngine;

public class EspadaHitbox : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Scorpion"))
        {
            Debug.Log("Golpeaste al escorpión!");
            Destroy(other.gameObject, 0.2f); // destruye con pequeño delay (animación de golpe)
            // O también: other.GetComponent<Scorpion>().TakeDamage(damage);
        }
    }
}
