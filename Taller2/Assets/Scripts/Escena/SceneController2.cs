using UnityEngine;
using System.Collections.Generic;

public class SceneController2 : MonoBehaviour
{
    [Header("Prefab de la moneda")]
    public GameObject coinPrefab;

    [Header("Puntos fijos para las monedas")]
    public List<GameObject> puntosFijos = new List<GameObject>();

    private void Start()
    {
        GenerarMonedas();
    }

    private void GenerarMonedas()
    {
        foreach (GameObject punto in puntosFijos)
        {
            if (punto != null && coinPrefab != null)
            {
                Instantiate(coinPrefab, punto.transform.position, Quaternion.identity);
            }
        }
    }
}
