using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public string nameItem;   // Nombre del ítem (ej: Manzana, Banana)
    public int itemValue = 1; // Valor en puntos

    public AudioClip itemSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("El jugador recogió: " + nameItem + " (+" + itemValue + ")");

            if (nameItem == "Coin")
            {
                GameManager.Instance.TotalCoin(itemValue);
            }
            else if (nameItem == "Coin2")
            {
                GameManager.Instance.TotalCoin2(itemValue);
            }
            else if (nameItem == "Coin3")
            {
                GameManager.Instance.TotalCoin3(itemValue);
            }
            if (itemSound != null)
            {
                AudioSource.PlayClipAtPoint(itemSound, transform.position);

            }

            Destroy(gameObject);
        }
    }
}