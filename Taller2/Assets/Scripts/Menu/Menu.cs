using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [Header("Panel de Instrucciones")]
    public GameObject panelInstrucciones;   // Panel que contendrá imágenes y textos
    public Image[] imagenes;                // Opcional: asigna imágenes en el inspector
    public TextMeshProUGUI[] textos;        // Opcional: asigna textos en el inspector

    private bool panelActivo = false;
    public void inicio()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

     public void salir()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit(); 
    }

    // Método para mostrar/ocultar instrucciones
    public void instrucciones()
    {
        panelActivo = !panelActivo;

        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(panelActivo);

        if (panelActivo)
        {
            // Ejemplo: actualizar textos dinámicamente
            if (textos.Length > 0) textos[0].text = "Usa las flechas para moverte";
            if (textos.Length > 1) textos[1].text = "Presiona ESPACIO para saltar";
            if (textos.Length > 2) textos[2].text = "Recolecta objetos para sumar puntos";
        }
    }
        public void CerrarInstrucciones()
    {
        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(false);
    }
}

