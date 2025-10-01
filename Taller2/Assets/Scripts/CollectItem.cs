using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public enum CoinType { Coin1, Coin2, Coin3 }  // Tipos de moneda
    public CoinType coinType;
    public int coinValue = 1;   // Cada moneda puede valer diferente

    public AudioClip itemSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"El jugador recogió: {coinType} (+{coinValue})");

            switch (coinType)
            {
                case CoinType.Coin1:
                    GameManager.Instance.TotalCoin(coinValue);
                    break;

                case CoinType.Coin2:
                    GameManager.Instance.TotalCoin2(coinValue);
                    break;

                case CoinType.Coin3:
                    GameManager.Instance.TotalCoin3(coinValue);
                    break;
            }

            if (itemSound != null)
                AudioSource.PlayClipAtPoint(itemSound, transform.position);

            Destroy(gameObject);
        }
    }
}