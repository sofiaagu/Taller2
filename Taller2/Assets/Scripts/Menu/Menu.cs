using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;   // Necesario para VideoPlayer

public class Menu : MonoBehaviour
{
    [Header("Panel de Instrucciones")]
    public GameObject panelInstrucciones;
    public Image[] imagenes;
    public TextMeshProUGUI[] textos;

    private bool panelActivo = false;

    [Header("Video de inicio")]
    public GameObject panelVideo;      // Canvas con el RawImage del video
    public VideoPlayer videoPlayer;    // Asigna el VideoPlayer en el inspector

    public void inicio()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void salir()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public void instrucciones()
    {
        panelActivo = !panelActivo;

        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(panelActivo);

        if (panelActivo)
        {
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
