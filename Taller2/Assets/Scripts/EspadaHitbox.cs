using UnityEngine;

public class EspadaHitbox : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Scorpion"))
        {
            Debug.Log("Golpeaste al escorpi�n!");
            Destroy(other.gameObject, 0.2f); // destruye con peque�o delay (animaci�n de golpe)
            // O tambi�n: other.GetComponent<Scorpion>().TakeDamage(damage);
        }
    }
}
