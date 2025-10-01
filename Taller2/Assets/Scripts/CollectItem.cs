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
            else if (nameItem == "llave")
            {
                GameManager.Instance.TotalLlave(itemValue);
            }
            else if (nameItem == "vida")
            {
                GameManager.Instance.TotalVida(itemValue);
            }
            if (itemSound != null)
            {
                AudioSource.PlayClipAtPoint(itemSound, transform.position);

            }

            Destroy(gameObject);
        }
    }
}